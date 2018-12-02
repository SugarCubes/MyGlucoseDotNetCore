using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyGlucoseDotNetCore.Models
{
    public class ApplicationRole : IdentityRole
    {
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<ApplicationUserRole> Users { get; set; }

        public ApplicationRole()
        {
            Users = new List<ApplicationUserRole>();

        } // constructor

    }
}
