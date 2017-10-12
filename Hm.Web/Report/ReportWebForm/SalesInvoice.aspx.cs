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
using Hm.Web.Framework.Extention;

namespace Hm.Web.Report.ReportWebForm
{
    public partial class SalesInvoice : System.Web.UI.Page
    {
        private IWorkContext _currentContext;
        private HotelManagementEntities _hmContext;

        protected void Page_Load(object sender, EventArgs e)
        {
            _currentContext = EngineContext.Current.Resolve<IWorkContext>();
            _hmContext = EngineContext.Current.Resolve<HotelManagementEntities>();

            var que = _hmContext.trn_InvoiceDetails.AsQueryable().AsNoTracking();

            if (!Page.IsPostBack)
            {
                PopulateGrid(que);
            }

        }

        public void PopulateGrid(IQueryable<trn_InvoiceDetails> quotation)
        {
            var companyName = _currentContext.GetCompany.companyName;
            var agentList = from p in
                                quotation.ToList()
                            select new
                            {
                                CompanyName = _currentContext.GetCompany.companyName,
                                InvoiceCD = p.trn_Invoice.InvoiceCD,
                                TrnCD = p.trn_Invoice.TrnCD,
                                CustomerName = p.trn_Invoice.mst_Customer.CustomerName,
                                DocumentDate = p.trn_Invoice.DocumentDate.FormatDate(),
                                ProductName = p.mst_Product.PartNo,
                                UnitPrice = p.UnitPrice,
                                Quantity = p.DeliverQTY,
                                Amount = p.Amount
                            };

            ReportViewer1.Reset();
            ReportViewer1.LocalReport.ReportPath = "Report/RDLC/SalesInvoice.rdlc";
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("SalesInvoiceDataSet", agentList));
            ReportViewer1.LocalReport.Refresh();
        }
    }
}