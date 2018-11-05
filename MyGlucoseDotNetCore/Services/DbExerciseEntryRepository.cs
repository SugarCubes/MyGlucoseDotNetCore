using MyGlucoseDotNetCore.Data;
using MyGlucoseDotNetCore.Models;
using MyGlucoseDotNetCore.Models.ViewModels;
using MyGlucoseDotNetCore.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
/*********************************************/
//  Created by J.T. Blevins
//  Modified by Heather Harvey with advice from Natash Ince and J.T. Blevins
/*********************************************/
namespace MyGlucoseDotNetCore.Services
{
    public class DbExerciseEntryRepository : IExerciseEntryRepository
    {
        private readonly ApplicationDbContext _db;

        public DbExerciseEntryRepository( ApplicationDbContext db )
        {
            _db = db;

        } // Injection Constructor


        public async Task<ExerciseEntry> ReadAsync( Guid id )
        {
            return await ReadAll()
                .SingleOrDefaultAsync( o => o.Id == id );

        } // ReadAsync


        public IQueryable<ExerciseEntry> ReadAll()
        {
            return _db.ExerciseEntries;

        } // ReadAll


        public async Task<ExerciseEntry> CreateAsync( ExerciseEntry exerciseentry )
        {
            _db.ExerciseEntries.Add( exerciseentry );
            await _db.SaveChangesAsync();
            return exerciseentry;

        } // Create


        public async Task UpdateAsync( Guid id, ExerciseEntry exerciseEntry )
        {
            var oldExerciseEntry = await ReadAsync( id );
            if( oldExerciseEntry != null )
            {
    			oldExerciseEntry.UserName = exerciseEntry.UserName;
    			oldExerciseEntry.User = exerciseEntry.User;
    			oldExerciseEntry.Name = exerciseEntry.Name;
    			oldExerciseEntry.Minutes = exerciseEntry.Minutes;
    			oldExerciseEntry.CreatedAt = exerciseEntry.CreatedAt;
                oldExerciseEntry.UpdatedAt = exerciseEntry.UpdatedAt;
                oldExerciseEntry.Timestamp = exerciseEntry.Timestamp;
                _db.Entry( oldExerciseEntry ).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return;
            }

        } // UpdateAsync


        public async Task DeleteAsync( Guid id )
        {
            var exerciseentry = await ReadAsync( id );
            if( exerciseentry != null )
            {
                _db.ExerciseEntries.Remove( exerciseentry );
                await _db.SaveChangesAsync();
            }
            return;

        } // DeleteAsync

        public ExerciseEntry Create(ExerciseEntry exerciseEntry)
        {
            _db.ExerciseEntries.Add(exerciseEntry);
            _db.SaveChanges();
            return exerciseEntry;
        }// ExerciseEntry Create

        public ExerciseEntry Read(Guid exerciseEntryId)
        {
            return _db.ExerciseEntries.FirstOrDefault(e => e.Id == exerciseEntryId);
        }// ExerciseEntry Read
        

        public async Task CreateOrUpdateEntries( ICollection<ExerciseEntry> exerciseEntries )
        {
            foreach ( ExerciseEntry exerciseEntry in exerciseEntries )
            {
                ExerciseEntry dbExerciseEntry = await ReadAsync( exerciseEntry.Id );
                if ( dbExerciseEntry == null )                  // If meal entry doesn't exist
                {
                    // Create in the database
                    await CreateAsync( exerciseEntry );

                }
                else if ( dbExerciseEntry.UpdatedAt < exerciseEntry.UpdatedAt )
                {
                    // Update in the database
                    await UpdateAsync( exerciseEntry.Id, exerciseEntry );

                }

            } // foreach MealEntry

            return;

        } // CreateOrUpdateEntries

    } // Class

} // Namespace