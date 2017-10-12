using Hm.Core;
using Hm.Core.Domain;
using Hm.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;

namespace Hm.Web.Report.ReportWebForm
{
    public partial class SalesOrder : System.Web.UI.Page
    {
        private IWorkContext _currentContext;
        private HotelManagementEntities _hmContext;


        protected void Page_Load(object sender, EventArgs e)
        {
            _currentContext = EngineContext.Current.Resolve<IWorkContext>();         
            _hmContext = EngineContext.Current.Resolve<HotelManagementEntities>();

            var que = _hmContext.trn_SalesOrderDetails.AsQueryable().AsNoTracking();

            if (!Page.IsPostBack)
            {
                PopulateGrid(que);
            }


        }

        public void PopulateGrid(IQueryable<trn_SalesOrderDetails> quotation)
        {
            var companyName = _currentContext.GetCompany.companyName;
            var agentList = from p in
                                quotation.ToList()
                            select new
                            {
                                CompanyName = _currentContext.GetCompany.companyName,
                                SalesOrderCD = p.SalesOrderNo,
                                TrnCD = p.trn_SalesOrder.TrnCD,
                                CustomerName = p.trn_SalesOrder.mst_Customer.CustomerName,
                                DocumentDate = p.trn_SalesOrder.DocumentDate.ToString(HmGlobal.StandardDateFormat),
                                ProductName = p.mst_Product.PartNo,                                
                                Quantity = p.Quantity,
                                UnitPrice = p.UnitPrice,                               
                                Amount = p.Amount
                            };

            ReportViewer1.Reset();
            ReportViewer1.LocalReport.ReportPath = "Report/RDLC/SalesOrder.rdlc";
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("SalesOrderDataSet", agentList));
            ReportViewer1.LocalReport.Refresh();
        }
    }
}