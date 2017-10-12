using Hm.Core.Infrastructure;
using Hm.Web.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Hm.Web.Controllers
{
    public class BasePublicController : BaseController
    {
        protected virtual ActionResult InvokeHttp404()
        {
            return new EmptyResult();
        }
	}
}