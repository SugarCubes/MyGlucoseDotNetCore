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
        Task<ExerciseEntry> CreateAsync( ExerciseEntry project );
        Task UpdateAsync( Guid id, ExerciseEntryViewModel project );
        Task DeleteAsync( Guid id );
        ExerciseEntry Create(ExerciseEntry project);

    } // Interface

} // Namespace