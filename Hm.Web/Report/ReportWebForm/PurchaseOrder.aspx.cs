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
    public partial class PurchaseOrder : System.Web.UI.Page
    {
        private IWorkContext _currentContext;     
        private HotelManagementEntities _hmContext;

        protected void Page_Load(object sender, EventArgs e)
        {
            _currentContext = EngineContext.Current.Resolve<IWorkContext>();           
            _hmContext = EngineContext.Current.Resolve<HotelManagementEntities>();

            var que = _hmContext.trn_PurchaseOrderDetails.AsQueryable().AsNoTracking();

            if (!Page.IsPostBack)
            {
                PopulateGrid(que);
            }

        }

        public void PopulateGrid(IQueryable<trn_PurchaseOrderDetails> quotation)
        {
            var companyName = _currentContext.GetCompany.companyName;
            var agentList = from p in
                                quotation.ToList()
                            select new
                            {
                                CompanyName = _currentContext.GetCompany.companyName,
                                PurchaseCD = p.trn_PurchaseOrder.PurchaseCD,
                                TrnCD = p.trn_PurchaseOrder.TrnCD,
                                SupplierCD = p.trn_PurchaseOrder.SupplierCD,
                                DocumentDate = p.trn_PurchaseOrder.DocumentDate.ToString(HmGlobal.StandardDateFormat),
                                Subject = p.trn_PurchaseOrder.Subject,
                                PartNo = p.mst_Product.PartNo,
                                Quantity = p.OrderQuantity,
                                UnitPrice = p.UnitPrice,
                                Amount = p.Amount
                            };

            ReportViewer1.Reset();
            ReportViewer1.LocalReport.ReportPath = "Report/RDLC/PurchaseOrder.rdlc";
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("PurchaseOrderDataset", agentList));
            ReportViewer1.LocalReport.Refresh();
        }
    }
}