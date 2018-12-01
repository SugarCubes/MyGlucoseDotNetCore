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
                await _roleManager.CreateAsync(new ApplicationRole { Name = "Doctor", Description = "A role allowing doctors to view their patients' statistics.", CreatedDate = DateTime.Now } );
            }

            if (!_context.Roles.Any(r => r.Name == "Patient"))
            {
                await _roleManager.CreateAsync(new ApplicationRole { Name = "Patient", Description = "A patient, registered to a doctor", CreatedDate = DateTime.Now });
            }

            return;

        } // SeedAsync

    } // class

} // namespace
