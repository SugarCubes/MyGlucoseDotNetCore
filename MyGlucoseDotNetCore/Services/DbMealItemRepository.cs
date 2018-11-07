using Microsoft.EntityFrameworkCore;
using MyGlucoseDotNetCore.Data;
using MyGlucoseDotNetCore.Models;
using MyGlucoseDotNetCore.Models.ViewModels;
using MyGlucoseDotNetCore.Services.Interfaces;
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


        public async Task<MealItem> ReadAsync( Guid mealid )
        {
            return await ReadAll()
                .Include( o => o.Meal )
                .SingleOrDefaultAsync( o => o.Id == mealid );

        } // ReadAsync


        public IQueryable<MealItem> ReadAll()
        {
            return _db.MealItems
                .Include( o => o.Meal );

        } // ReadAll


        public async Task<MealItem> CreateAsync( Guid mealEntryId, MealItem mealItem )
        {
            var mealEntry = await _db.MealEntries
                .Include( o => o.MealItems )
                .SingleOrDefaultAsync( o => o.Id == mealEntryId );
            if ( mealEntry != null )
            {
                mealEntry.MealItems.Add( mealItem );    // Associate item with the entry
                mealItem.Meal = mealEntry;              // Associate the entry with the item
                await _db.SaveChangesAsync();
            }// End if mealEntry not null statement.

            return mealItem;

        } // CreateAsync


        public async Task UpdateAsync( Guid id, MealItem mealItem )
        {
            var oldMealItem = await ReadAsync( id );
            if ( oldMealItem != null )
            {
                oldMealItem.Meal = mealItem.Meal;
                oldMealItem.Name = mealItem.Name;
                oldMealItem.Carbs = mealItem.Carbs;
                oldMealItem.Servings = mealItem.Servings;
                oldMealItem.Meal = mealItem.Meal;
                oldMealItem.MealId = mealItem.MealId;
                oldMealItem.UpdatedAt = DateTime.Now;
                //oldMealItem.UpdatedAt = mealItem.UpdatedAt;
                _db.Entry( oldMealItem.Meal ).State = EntityState.Unchanged;
                _db.Entry( oldMealItem ).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return;
            }

        } // UpdateAsync


        public async Task DeleteAsync( Guid id )
        {
            var mealitem = await ReadAsync( id );
            if ( mealitem != null )
            {
                _db.MealItems.Remove( mealitem );
                await _db.SaveChangesAsync();
            }
            return;

        } // DeleteAsync
        

        public async Task CreateOrUpdateEntries( ICollection<MealItem> mealItems )
        {
            foreach ( MealItem mealItem in mealItems )
            {
                MealItem dbMealItem = await ReadAsync( mealItem.Id );
                if ( dbMealItem == null )                  // If meal entry doesn't exist
                {
                    // Create in the database
                    await CreateAsync( mealItem.Id, mealItem );

                }
                else if ( dbMealItem.UpdatedAt < mealItem.UpdatedAt )
                {
                    // Update in the database
                    await UpdateAsync( mealItem.Id, mealItem );

                }

            } // foreach MealEntry

            return;

        } // CreateOrUpdateEntries

    } // Class

} // Namespace