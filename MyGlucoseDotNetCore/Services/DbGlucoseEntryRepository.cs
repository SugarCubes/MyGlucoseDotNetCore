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
    public class DbGlucoseEntryRepository : IGlucoseEntryRepository
    {
        private readonly ApplicationDbContext _db;

        public DbGlucoseEntryRepository( ApplicationDbContext db )
        {
            _db = db;

        } // Injection Constructor


        public async Task<GlucoseEntry> ReadAsync( Guid id )
        {
            return await ReadAll()
                .SingleOrDefaultAsync( o => o.Id == id );

        } // ReadAsync


        public IQueryable<GlucoseEntry> ReadAll()
        {
            return _db.GlucoseEntries;

        } // ReadAll


        public async Task<GlucoseEntry> CreateAsync( GlucoseEntry glucoseentry )
        {
            _db.GlucoseEntries.Add( glucoseentry );
            await _db.SaveChangesAsync();
            return glucoseentry;

        } // Create


        public async Task UpdateAsync( Guid id, GlucoseEntryViewModel glucoseentryVM )
        {
            var oldGlucoseEntry = await ReadAsync( id );
            if( oldGlucoseEntry != null )
            {
    			oldGlucoseEntry.PatientUsername = glucoseentryVM.PatientUsername;
    			oldGlucoseEntry.Patient = glucoseentryVM.Patient;
    			oldGlucoseEntry.Measurement = glucoseentryVM.Measurement;
    			oldGlucoseEntry.BeforeAfter = glucoseentryVM.BeforeAfter;
    			oldGlucoseEntry.WhichMeal = glucoseentryVM.WhichMeal;
    			oldGlucoseEntry.Date = glucoseentryVM.Date;
    			oldGlucoseEntry.Timestamp = glucoseentryVM.Timestamp;
                _db.Entry( oldGlucoseEntry ).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return;
            }

        } // UpdateAsync


        public async Task DeleteAsync( Guid id )
        {
            var glucoseentry = await ReadAsync( id );
            if( glucoseentry != null )
            {
                _db.GlucoseEntries.Remove( glucoseentry );
                await _db.SaveChangesAsync();
            }
            return;

        } // DeleteAsync

    } // Class

} // Namespace