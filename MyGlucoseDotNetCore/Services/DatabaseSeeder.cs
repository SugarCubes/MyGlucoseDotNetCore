using Microsoft.AspNetCore.Identity;
using MyGlucoseDotNetCore.Data;
using MyGlucoseDotNetCore.Models;
using System;
using System.Diagnostics;
using System.Linq;

namespace MyGlucoseDotNetCore.Services
{
    public class DatabaseSeeder
    {
        private ApplicationDbContext _context;
        private RoleManager<ApplicationRole> _roleManager;
        //private UserManager<IdentityUser> _userManager;

        public DatabaseSeeder(
           ApplicationDbContext context,
           RoleManager<ApplicationRole> roleManager
           /*UserManager<IdentityUser> userManager*/)
        {
            _context = context;
            _roleManager = roleManager;
            //_userManager = userManager;
        }


        public void SeedRoles()
        {
            _context.Database.EnsureCreated();

            if( !_context.Roles.Any( r => r.Name == Roles.DOCTOR ) )
            {
                Debug.WriteLine( "Creating Doctor" );
                _roleManager.CreateAsync( new ApplicationRole
                {
                    Name = Roles.DOCTOR,
                    Description = "A role allowing doctors to view their patients' statistics.",
                    CreatedDate = DateTime.Now
                } );
            }

            if( !_context.Roles.Any( r => r.Name == Roles.PATIENT ) )
            {
                Debug.WriteLine( "Creating Patient" );
                _roleManager.CreateAsync( new ApplicationRole
                {
                    Name = Roles.PATIENT,
                    Description = "A patient, registered to a doctor",
                    CreatedDate = DateTime.Now
                } );
            }

        } // SeedRoles

    } // class

} // namespace
