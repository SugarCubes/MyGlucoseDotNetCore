using MyGlucoseDotNetCore.Models;
using MyGlucoseDotNetCore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyGlucoseDotNetCore.Services.Interfaces
{
    public interface IDoctorRepository
    {
        Task<Doctor> ReadAsync( string username );
        IQueryable<Doctor> ReadAll();
        Task<Doctor> CreateAsync( Doctor doctor );
        Task UpdateAsync( string username, Doctor doctor );
        Task DeleteAsync( string username );
        

    } // Interface

} // Namespace