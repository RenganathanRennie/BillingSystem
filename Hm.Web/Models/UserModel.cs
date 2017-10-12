using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hm.Web.Models
{
    public partial class User
    {
        public User()
        {
            UserDetails = new List<User>();
        }
        public List<User> UserDetails;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Mobile { get; set; }
    }
}