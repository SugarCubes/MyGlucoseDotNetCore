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
    public class DbMealEntryRepository : IMealEntryRepository
    {
        private readonly ApplicationDbContext _db;

        public DbMealEntryRepository( ApplicationDbContext db )
        {
            _db = db;

        } // Injection Constructor


        public async Task<MealEntry> ReadAsync( Guid id )
        {
            return await ReadAll()
                .SingleOrDefaultAsync( o => o.Id == id );

        } // ReadAsync


        public IQueryable<MealEntry> ReadAll()
        {
            return _db.MealEntries;

        } // ReadAll


        public async Task<MealEntry> CreateAsync( MealEntry mealentry )
        {
            _db.MealEntries.Add( mealentry );
            await _db.SaveChangesAsync();
            return mealentry;

        } // Create


        public async Task UpdateAsync( Guid id, MealEntryViewModel mealentryVM )
        {
            var oldMealEntry = await ReadAsync( id );
            if( oldMealEntry != null )
            {
    			oldMealEntry.UserName = mealentryVM.UserName;
    			oldMealEntry.User = mealentryVM.User;
    			oldMealEntry.TotalCarbs = mealentryVM.TotalCarbs;
    			oldMealEntry.Date = mealentryVM.Date;
    			oldMealEntry.Timestamp = mealentryVM.Timestamp;
    			oldMealEntry.MealItems = mealentryVM.MealItems;
                _db.Entry( oldMealEntry ).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return;
            }

        } // UpdateAsync


        public async Task DeleteAsync( Guid id )
        {
            var mealentry = await ReadAsync( id );
            if( mealentry != null )
            {
                _db.MealEntries.Remove( mealentry );
                await _db.SaveChangesAsync();
            }
            return;

        } // DeleteAsync

        public MealEntry Create(MealEntry mealEntry)
        {
            _db.MealEntries.Add(mealEntry);
            _db.SaveChanges();
            return mealEntry;
        }

        public MealEntry Read(Guid mealEntryId)
        {
            return _db.MealEntries.Include(m => m.MealItems).FirstOrDefault(m => m.Id == mealEntryId);
        }
    } // Class

} // Namespace