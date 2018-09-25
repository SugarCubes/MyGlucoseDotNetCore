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


        public async Task UpdateAsync( Guid id, ExcerciseEntryViewModel exerciseentryVM )
        {
            var oldExerciseEntry = await ReadAsync( id );
            if( oldExerciseEntry != null )
            {
    			oldExerciseEntry.UserName = exerciseentryVM.UserName;
    			oldExerciseEntry.User = exerciseentryVM.User;
    			oldExerciseEntry.ExerciseName = exerciseentryVM.ExerciseName;
    			oldExerciseEntry.Minutes = exerciseentryVM.Minutes;
    			oldExerciseEntry.Date = exerciseentryVM.Date;
    			oldExerciseEntry.Timestamp = exerciseentryVM.Timestamp;
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

    } // Class

} // Namespace