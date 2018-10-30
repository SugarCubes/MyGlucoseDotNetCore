using MyGlucoseDotNetCore.Models;
using MyGlucoseDotNetCore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyGlucoseDotNetCore.Services.Interfaces
{
    public interface IMealItemRepository
    {
        Task<MealItem> ReadAsync( Guid Id );
        IQueryable<MealItem> ReadAll();
        Task<MealItem> CreateAsync( Guid mealEntryId, MealItem mealItem );
        Task UpdateAsync( Guid id, MealItem mealItem );
        Task DeleteAsync( Guid id );
        Task CreateOrUpdateEntries( ICollection<MealItem> mealItems );

    } // Interface

} // Namespace