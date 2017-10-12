using Hm.Core.Infrastructure;
using Hm.Web.Framework.Themes;
using Hm.Web.Framework.Mvc.Routes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Threading;
using System.Globalization;
using Hm.Admin.Controllers;
using Hm.Core;

namespace Hm.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("favicon.ico");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //register custom routes (plugins, etc)
            var routePublisher = EngineContext.Current.Resolve<IRoutePublisher>();
            routePublisher.RegisterRoutes(routes);

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Account", action = "Index", id = UrlParameter.Optional },
                new[] { "Hm.Web.Controllers" }
            );
        }

        protected void Application_Start()
        {

            //initialize engine context
            EngineContext.Initialize(false);

            //Registering some regular mvc stuff
            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new ThemeableRazorViewEngine());

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            BundleTable.EnableOptimizations = false;

            //Model Datetime format override http://stackoverflow.com/questions/11272851/format-datetime-in-asp-net-mvc-4
            ModelBinders.Binders.Add(typeof(DateTime?), new MyDateTimeModelBinder());

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            setTimeFormat();
        }


        protected void Application_Error(Object sender, EventArgs e)
        {
            var exception = Server.GetLastError();

            try
            {
                //process 404 HTTP errors
                var httpException = exception as HttpException;
                if (httpException != null && httpException.GetHttpCode() == 404)
                {
                    var webHelper = EngineContext.Current.Resolve<IWebHelper>();
                    if (!webHelper.IsStaticResource(this.Request))
                    {

                        Response.Clear();
                        Server.ClearError();
                        Response.TrySkipIisCustomErrors = true;
                        // Call target Controller and pass the routeData.
                        IController errorController = EngineContext.Current.Resolve<Hm.Admin.Controllers.CommonController>();
                        var routeData = new RouteData();
                        routeData.Values.Add("controller", "Shared");
                        routeData.Values.Add("action", "UnderConstruction");

                        errorController.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }



        public void setTimeFormat()
        {
            CultureInfo culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            culture.DateTimeFormat.ShortDatePattern = HmGlobal.StandardDateTimeFormat;
            culture.DateTimeFormat.LongTimePattern = "";
            Thread.CurrentThread.CurrentCulture = culture;
        }


    }

    public class MyDateTimeModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var displayFormat = bindingContext.ModelMetadata.DisplayFormatString;
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (!string.IsNullOrEmpty(displayFormat) && value != null)
            {
                DateTime date;
                displayFormat = displayFormat.Replace("{0:", string.Empty).Replace("}", string.Empty);
                // use the format specified in the DisplayFormat attribute to parse the date
                if (DateTime.TryParseExact(value.AttemptedValue, displayFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                {
                    return date;
                }
                else
                {
                    bindingContext.ModelState.AddModelError(
                        bindingContext.ModelName,
                        string.Format("{0} is an invalid date format", value.AttemptedValue)
                    );
                }
            }

            return base.BindModel(controllerContext, bindingContext);
        }
    }
}
