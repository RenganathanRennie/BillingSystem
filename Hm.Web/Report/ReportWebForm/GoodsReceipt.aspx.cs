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
    public partial class GoodsReceipt : System.Web.UI.Page
    {
        private IWorkContext _currentContext;
        private HotelManagementEntities _hmContext;

        protected void Page_Load(object sender, EventArgs e)
        {
            _currentContext = EngineContext.Current.Resolve<IWorkContext>();
            _hmContext = EngineContext.Current.Resolve<HotelManagementEntities>();

            var que = _hmContext.trn_Good_Details.AsQueryable().AsNoTracking().Where(a=>a.CompanyCD == _currentContext.GetCompany.CompanyCD);

            if (!Page.IsPostBack)
            {
                PopulateGrid(que);
            }

        }

        public void PopulateGrid(IQueryable<trn_Good_Details> quotation)
        {
            var companyName = _currentContext.GetCompany.companyName;
            var agentList = from p in
                                quotation.ToList()
                            select new
                            {
                                CompanyName = _currentContext.GetCompany.companyName,
                                GoodCD = p.trn_Good.GoodCD,
                                TrnCD = p.trn_Good.TrnCD,
                                SupplierName = p.trn_Good.mst_Supplier.SupplierName,
                                DocumentDate = p.trn_Good.DocumentDate.Value.ToString(HmGlobal.StandardDateFormat),
                                ProductName = p.mst_Product.PartNo,
                                UnitPrice = p.UnitPrice,
                                Quantity = p.Quantity,
                                Amount = p.Amount                               
                            };

            ReportViewer1.Reset();
            ReportViewer1.LocalReport.ReportPath = "Report/RDLC/GoodsReceipt.rdlc";
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("GoodsReceiptDataSet", agentList));
            ReportViewer1.LocalReport.Refresh();
        }
    }
}