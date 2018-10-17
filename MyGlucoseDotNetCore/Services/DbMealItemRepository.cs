using MyGlucoseDotNetCore.Data;
using MyGlucoseDotNetCore.Models;
using MyGlucoseDotNetCore.Models.ViewModels;
using MyGlucoseDotNetCore.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyGlucoseDotNetCore.Services
{
    public class DbMealItemRepository : IMealItemRepository
    {
        private readonly ApplicationDbContext _db;

        public DbMealItemRepository( ApplicationDbContext db )
        {
            _db = db;

        } // Injection Constructor


        public async Task<MealItem> ReadAsync( Guid id, Guid mealid )
        {
            return await ReadAll()
                .SingleOrDefaultAsync( o => o.Id == id
                && o.MealId == mealid );

        } // ReadAsync


        public IQueryable<MealItem> ReadAll()
        {
            return _db.MealItems;

        } // ReadAll


        public async Task<MealItem> CreateAsync( MealItem mealitem )
        {
            _db.MealItems.Add( mealitem );
            await _db.SaveChangesAsync();
            return mealitem;

        } // CreateAsync


        public async Task UpdateAsync( Guid id, Guid mealid, MealItemViewModel mealitemVM )
        {
            var oldMealItem = await ReadAsync( id, mealid );
            if( oldMealItem != null )
            {
    			oldMealItem.Meal = mealitemVM.Meal;
    			oldMealItem.Name = mealitemVM.Name;
    			oldMealItem.Carbs = mealitemVM.Carbs;
    			oldMealItem.Servings = mealitemVM.Servings;
                _db.Entry( oldMealItem ).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return;
            }

        } // UpdateAsync


        public async Task DeleteAsync( Guid id, Guid mealid )
        {
            var mealitem = await ReadAsync( id, mealid );
            if( mealitem != null )
            {
                _db.MealItems.Remove( mealitem );
                await _db.SaveChangesAsync();
            }
            return;

        } // DeleteAsync
        

        public MealItem Create(Guid mealEntryId, MealItem mealItem)
        {
            var mealEntry = Read(mealEntryId);
            if (mealEntry != null)
            {
                mealEntry.MealItems.Add(mealItem);
                mealItem.Meal = mealEntry;
                _db.SaveChanges();
            }// End if mealEntry not null statement.
            return mealItem;
        } // End Create

        public MealEntry Read(Guid mealId)
        {
            return _db.MealEntries.Include(m => m.MealItems).FirstOrDefault(m => m.Id == mealId);
        }// End Read
    } // Class

} // Namespace