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
        Task<Patient> ReadAsync( string username );
        IQueryable<Patient> ReadAll();
        Task<Patient> CreateAsync(Patient patient);
        Task UpdateAsync( string username, Patient project );
        Task DeleteAsync( string username );

        

    } // Interface

} // Namespace