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
    public partial class Product : System.Web.UI.Page
    {
        private IWorkContext _currentContext;
        private HotelManagementEntities _hmContext;

        protected void Page_Load(object sender, EventArgs e)
        {
            _currentContext = EngineContext.Current.Resolve<IWorkContext>();
            _hmContext = EngineContext.Current.Resolve<HotelManagementEntities>();

            var que = _hmContext.mst_Product.AsQueryable().AsNoTracking();

            var partNo = Request.QueryString["ProductName"];

            if (!string.IsNullOrWhiteSpace(partNo))
            {
                que = que.Where(a => a.PartNo.Contains(partNo));
            }

           

            if (!Page.IsPostBack)
            {
                PopulateGrid(que);
            }

        }

        public void PopulateGrid(IQueryable<mst_Product> quotation)
        {
            var companyName = _currentContext.GetCompany.companyName;
            var agentList = from p in
                                quotation.ToList()
                            select new 
                            {
                                CompanyName = _currentContext.GetCompany.companyName,
                                PartNo = p.PartNo,
                                PurchasePrice = p.PurchasePrice,
                                PurchaseCurrency = p.PurchaseCurrency,
                                SalesPrice = p.SalesPrice,
                                PackingQty = p.PackingQty,
                                Ratio = p.Ratio     
                               
                            };

            ReportViewer1.Reset();
            ReportViewer1.LocalReport.ReportPath = "Report/RDLC/Product.rdlc";
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("ProductDataSet", agentList));
            ReportViewer1.LocalReport.Refresh();
        }
    }
}