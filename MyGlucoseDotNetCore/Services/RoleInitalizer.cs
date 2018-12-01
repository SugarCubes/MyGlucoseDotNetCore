using MyGlucoseDotNetCore.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyGlucoseDotNetCore.Models;

namespace MyGlucoseDotNetCore.Services
{
    public class RoleInitalizer
    {
        private ApplicationDbContext _context;
        private RoleManager<IdentityRole> _roleManager;
        //private UserManager<IdentityUser> _userManager;

        public RoleInitalizer(
           ApplicationDbContext context,
           RoleManager<Microsoft.AspNetCore.Identity.IdentityRole> roleManager
           /*UserManager<IdentityUser> userManager*/)
        {
            _context = context;
            _roleManager = roleManager;
            //_userManager = userManager;
        }

        public async Task SeedAsync()
        {
            _context.Database.EnsureCreated();

            if(!_context.Roles.Any(r => r.Name == "Doctor"))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = "Doctor"});
            }

            if (!_context.Roles.Any(r => r.Name == "Patient"))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = "Patient" });
            }
        }
    }
}
