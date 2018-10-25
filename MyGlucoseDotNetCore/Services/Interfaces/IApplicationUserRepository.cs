using MyGlucoseDotNetCore.Models;
using MyGlucoseDotNetCore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyGlucoseDotNetCore.Services.Interfaces
{
    public interface IApplicationUserRepository
    {
        Task<ApplicationUser> ReadAsync( string username );
        IQueryable<ApplicationUser> ReadAll();
        Task<ApplicationUser> CreateAsync( ApplicationUser applicationUser );
        Task UpdateAsync( string username, ApplicationUser applicationUser );
        Task DeleteAsync( string username );
        PatientViewModel CreatePatient(PatientViewModel patient);
    }
}
