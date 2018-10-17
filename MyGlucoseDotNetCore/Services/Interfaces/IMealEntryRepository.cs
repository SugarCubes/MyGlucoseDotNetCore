using MyGlucoseDotNetCore.Models;
using MyGlucoseDotNetCore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyGlucoseDotNetCore.Services.Interfaces
{
    public interface IMealEntryRepository
    {
        Task<MealEntry> ReadAsync( Guid id );
        IQueryable<MealEntry> ReadAll();
        Task<MealEntry> CreateAsync( MealEntry project );
        Task UpdateAsync( Guid id, MealEntryViewModel project );
        Task DeleteAsync( Guid id );
        MealEntry Create(MealEntry mealEntry);
        MealEntry Read(Guid mealId);

    } // Interface

} // Namespace