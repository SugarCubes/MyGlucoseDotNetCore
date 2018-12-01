using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyGlucoseDotNetCore.Models
{
    public class RoleUser
    {
        public IdentityUser User { get; set; }
        public ICollection<IdentityRole> Roles { get; set; }

        public RoleUser()
        {
            Roles = new List<IdentityRole>();
        }

        public bool HasRole(string roleName)
        {
            return Roles.FirstOrDefault(r => r.Name == roleName) != null;
        }
    }
}
