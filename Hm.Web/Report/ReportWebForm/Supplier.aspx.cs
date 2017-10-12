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
    public partial class Supplier : System.Web.UI.Page
    {
        private IWorkContext _currentContext;
        private HotelManagementEntities _hmContext;

        protected void Page_Load(object sender, EventArgs e)
        {
            _currentContext = EngineContext.Current.Resolve<IWorkContext>();
            _hmContext = EngineContext.Current.Resolve<HotelManagementEntities>();

            var que = _hmContext.mst_Supplier.AsQueryable().AsNoTracking();

            var partNo = Request.QueryString["email"];

            if (!string.IsNullOrWhiteSpace(partNo))
            {
                que = que.Where(a => a.Email.Contains(partNo));
            }
            if (!Page.IsPostBack)
            {
                PopulateGrid(que);
            }

        }

        public void PopulateGrid(IQueryable<mst_Supplier> quotation)
        {
            var companyName = _currentContext.GetCompany.companyName;
            var agentList = from c in
                                quotation.ToList()
                            select new
                            {
                                CompanyName = _currentContext.GetCompany.companyName,
                                SupplierName = c.SupplierName,
                                Address = c.AddressLine1 + ' ' + c.AddressLine2 + ' ' + c.AddressLine3,
                                Attention = c.Attention,
                                Designation = c.Designation,
                                Email = c.Email,
                                TranCurrency = c.TranCurrency,
                                GLCurrency = c.GLCurrency,
                                CreditLimit = c.CreditLimit
                            };

            ReportViewer1.Reset();
            ReportViewer1.LocalReport.ReportPath = "Report/RDLC/Supplier.rdlc";
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("SupplierDataSet", agentList));
            ReportViewer1.LocalReport.Refresh();
        }
    }
}