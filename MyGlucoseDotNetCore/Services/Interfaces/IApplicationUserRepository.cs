using MyGlucoseDotNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyGlucoseDotNetCore.Services.Interfaces
{
    public interface IApplicationUserRepository
    {
        bool UserExists( string userName );
        Task<ApplicationUser> ReadAsync( string username );
        IQueryable<ApplicationUser> ReadAll();
        Task<ApplicationUser> CreateAsync( ApplicationUser applicationUser );
        Task UpdateAsync( string username, ApplicationUser applicationUser );
        Task DeleteAsync( string username );
        ApplicationUser ReadUser(string email);
        //ApplicationUserRole Read(ApplicationRole role);
        //ApplicationRole Read(string role);

        Task<bool> AssignRole(string email, string roleName);
        List<ApplicationRole> ReadAllRoles();

        //bool HasRole(string roleName);


    }
}
