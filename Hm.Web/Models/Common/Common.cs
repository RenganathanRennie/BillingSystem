using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hm.Web.Models.Common
{
    public partial class HeaderLinksModel
    {
        public bool IsAuthenticated { get; set; }
        public string UserEmail { get; set; }
    }
}