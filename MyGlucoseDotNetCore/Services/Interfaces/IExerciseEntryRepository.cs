using MyGlucoseDotNetCore.Models;
using MyGlucoseDotNetCore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyGlucoseDotNetCore.Services.Interfaces
{
    public interface IExerciseEntryRepository
    {
        Task<ExerciseEntry> ReadAsync( Guid id );
        IQueryable<ExerciseEntry> ReadAll();
        Task<ExerciseEntry> CreateAsync( ExerciseEntry project );
        Task UpdateAsync( Guid id, ExcerciseEntryViewModel project );
        Task DeleteAsync( Guid id );

    } // Interface

} // Namespace