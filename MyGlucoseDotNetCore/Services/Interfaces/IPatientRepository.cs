using MyGlucoseDotNetCore.Models;
using MyGlucoseDotNetCore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyGlucoseDotNetCore.Services.Interfaces
{
    public interface IPatientRepository
    {
        Task<Patient> ReadAsync( string usernameid );
        IQueryable<Patient> ReadAll();
        Task<Patient> CreateAsync( Patient project );
        Task UpdateAsync( string usernameid, PatientViewModel project );
        Task DeleteAsync( string usernameid );

    } // Interface

} // Namespace