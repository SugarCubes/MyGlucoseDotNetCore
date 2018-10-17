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
        Task<MealItem> ReadAsync( Guid id, Guid mealid );
        IQueryable<MealItem> ReadAll();
        Task<MealItem> CreateAsync( MealItem project );
        Task UpdateAsync( Guid id, Guid mealid, MealItemViewModel project );
        Task DeleteAsync( Guid id, Guid mealid );
        MealItem Create(Guid mealEntryId, MealItem mealItem);
        MealEntry Read(Guid mealId);

    } // Interface

} // Namespace