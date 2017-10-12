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
    public partial class Customer : System.Web.UI.Page
    {
        private IWorkContext _currentContext;
        private HotelManagementEntities _hmContext;

        protected void Page_Load(object sender, EventArgs e)
        {
            _currentContext = EngineContext.Current.Resolve<IWorkContext>();
            _hmContext = EngineContext.Current.Resolve<HotelManagementEntities>();

            var que = _hmContext.mst_Customer.AsQueryable()
                .Include(a=>a.mst_Tax)
                .AsNoTracking()
                .Where(a=>a.CompanyCD ==  _currentContext.GetCompany.CompanyCD);

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

        public void PopulateGrid(IQueryable<mst_Customer> quotation)
        {
            var companyName = _currentContext.GetCompany.companyName;
            var agentList = from c in
                                quotation.ToList()
                            select new
                            {
                                CompanyName = _currentContext.GetCompany.companyName,
                                CustomerName = c.CustomerName,
                                Address = c.AddressLine1 +' ' +c.AddressLine2+' '+c.AddressLine3,
                                Email = c.Email,
                                TrnCurrency =c.TrnCurrency ,
                                DefaultShipping = c.DefaultShipping
                            };

            ReportViewer1.Reset();
            ReportViewer1.LocalReport.ReportPath = "Report/RDLC/Customer.rdlc";
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("CustomerDataset", agentList));
            ReportViewer1.LocalReport.Refresh();
        }
    }
}