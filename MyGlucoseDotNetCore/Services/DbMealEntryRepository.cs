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
        private IMealItemRepository _mealItemRepository;

        public DbMealEntryRepository( ApplicationDbContext db,
            IMealItemRepository mealItemRepository )
        {
            _db = db;
            _mealItemRepository = mealItemRepository;

        } // Injection Constructor


        public async Task<MealEntry> ReadAsync( Guid id )
        {
            return await ReadAll()
                .Include( o => o.MealItems )
                .SingleOrDefaultAsync( o => o.Id == id );

        } // ReadAsync


        public IQueryable<MealEntry> ReadAll()
        {
            return _db.MealEntries
                .Include( o => o.MealItems );

        } // ReadAll


        public async Task<MealEntry> CreateAsync( MealEntry mealentry )
        {
            _db.MealEntries.Add( mealentry );
            await _db.SaveChangesAsync();
            return mealentry;

        } // Create


        public async Task UpdateAsync( Guid id, MealEntry mealEntry )
        {
            var oldMealEntry = await ReadAsync( id );
            if( oldMealEntry != null )
            {
                oldMealEntry.UserName = mealEntry.UserName;
                oldMealEntry.Patient = mealEntry.Patient;
                oldMealEntry.TotalCarbs = mealEntry.TotalCarbs;
                oldMealEntry.CreatedAt = mealEntry.CreatedAt;
                oldMealEntry.UpdatedAt = mealEntry.UpdatedAt;
                oldMealEntry.Timestamp = mealEntry.Timestamp;
                //oldMealEntry.MealItems = mealEntry.MealItems;

                _db.Entry( mealEntry.MealItems ).State = EntityState.Unchanged;
                _db.Entry( oldMealEntry.MealItems ).State = EntityState.Unchanged;

                foreach( var mealItem in mealEntry.MealItems )
                    _db.Entry( mealItem ).State = EntityState.Unchanged;

                foreach ( var mealItem in oldMealEntry.MealItems )
                    _db.Entry( mealItem ).State = EntityState.Unchanged;

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


        public async Task CreateOrUpdateEntries( ICollection<MealEntry> mealEntries )
        {
            foreach ( MealEntry mealEntry in mealEntries )
            {
                MealEntry dbMealEntry = await ReadAsync( mealEntry.Id );
                if ( dbMealEntry == null )                  // If meal entry doesn't exist
                {
                    // Create in the database
                    await CreateAsync( mealEntry );

                }
                else if ( dbMealEntry.UpdatedAt < mealEntry.UpdatedAt )
                {
                    // Update in the database
                    await UpdateAsync( mealEntry.Id, mealEntry );

                }

                // Check whether meal items need to be created/updated:
                await _mealItemRepository.CreateOrUpdateEntries( mealEntry.MealItems );

            } // foreach MealEntry

            return;

        } // CreateOrUpdateEntries

    } // Class

} // Namespace