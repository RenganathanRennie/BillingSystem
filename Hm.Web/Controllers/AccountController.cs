using Hm.Services.User;
using Hm.Web.Helper;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Hm.Web.Controllers
{
    public class AccountController : BasePublicController
    {
        public ActionResult index()
        {
           
            return View();
        }
       
	}
}