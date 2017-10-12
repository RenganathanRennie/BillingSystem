using Hm.Core;
using Hm.Core.Infrastructure;
using Hm.Services.Quotation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;
using Hm.Core.Domain;

namespace Hm.Web.Report.ReportWebForm
{
    public partial class Quotation : System.Web.UI.Page
    {

        private IWorkContext _currentContext;
        private IQuotationService _quotationService;
        private HotelManagementEntities _hmContext;

        protected void Page_Load(object sender, EventArgs e)
        {
            _currentContext = EngineContext.Current.Resolve<IWorkContext>();
            _quotationService = EngineContext.Current.Resolve<IQuotationService>();
            _hmContext = EngineContext.Current.Resolve<HotelManagementEntities>();

            var que = _hmContext.trn_QuotationDetails.AsQueryable().AsNoTracking();

            if (!Page.IsPostBack)
            {
                PopulateGrid(que);
            }

        }

        public void PopulateGrid(IQueryable<trn_QuotationDetails> quotation)
        {
            var companyName = _currentContext.GetCompany.companyName;
            var agentList = from p in
                                quotation.ToList()
                            select new
                            {
                                QuotationCD = p.QuotationCD,
                                Date = p.trn_Quotation.Date.ToString(HmGlobal.StandardDateFormat),
                                CustomerName = p.trn_Quotation.mst_Customer.CustomerName,
                                Address = p.trn_Quotation.AddressLine1,
                                PayTerm = p.trn_Quotation.PayTerm,
                                ProductName = p.mst_Product.PartNo,
                                UnitPrice = p.Unit,
                                Quantity = p.Quantity,
                                Amount = p.Amount
                            };

            ReportViewer1.Reset();
            ReportViewer1.LocalReport.ReportPath = "Report/RDLC/Quotation.rdlc";
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("QuotationDataSet", agentList));
            ReportViewer1.LocalReport.Refresh();
        }
    }
}