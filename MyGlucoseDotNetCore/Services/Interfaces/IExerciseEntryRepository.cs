using MyGlucoseDotNetCore.Models;
using MyGlucoseDotNetCore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
/*********************************************/
//  Created by J.T. Blevins
//  Modified by Heather Harvey with advice from Natash Ince and J.T. Blevins
/*********************************************/
namespace MyGlucoseDotNetCore.Services.Interfaces
{
    public interface IExerciseEntryRepository
    {
        Task<ExerciseEntry> ReadAsync( Guid id );
        IQueryable<ExerciseEntry> ReadAll();
        Task<ExerciseEntry> CreateAsync( ExerciseEntry exerciseEntry );
        Task UpdateAsync( Guid id, ExerciseEntry exerciseEntry );
        Task DeleteAsync( Guid id );
        ExerciseEntry Create(ExerciseEntry exerciseEntry );
        Task CreateOrUpdateEntries( ICollection<ExerciseEntry> exerciseEntries );

    } // Interface

} // Namespace