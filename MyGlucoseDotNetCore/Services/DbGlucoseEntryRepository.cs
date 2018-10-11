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
    public class DbGlucoseEntriesRepository : IGlucoseEntriesRepository
    {
        private readonly ApplicationDbContext _db;

        public DbGlucoseEntriesRepository( ApplicationDbContext db )
        {
            _db = db;

        } // Injection Constructor


        public async Task<GlucoseEntries> ReadAsync( Guid id )
        {
            return await ReadAll()
                .SingleOrDefaultAsync( o => o.Id == id );

        } // ReadAsync


        public IQueryable<GlucoseEntries> ReadAll()
        {
            return _db.GlucoseEntries;

        } // ReadAll


        public async Task<GlucoseEntries> CreateAsync( GlucoseEntries GlucoseEntries )
        {
            _db.GlucoseEntries.Add( GlucoseEntries );
            await _db.SaveChangesAsync();
            return GlucoseEntries;

        } // Create


        public async Task UpdateAsync( Guid id, GlucoseEntriesViewModel GlucoseEntriesVM )
        {
            var oldGlucoseEntries = await ReadAsync( id );
            if( oldGlucoseEntries != null )
            {
    			oldGlucoseEntries.PatientUsername = GlucoseEntriesVM.PatientUsername;
    			oldGlucoseEntries.Patient = GlucoseEntriesVM.Patient;
    			oldGlucoseEntries.Measurement = GlucoseEntriesVM.Measurement;
    			oldGlucoseEntries.BeforeAfter = GlucoseEntriesVM.BeforeAfter;
    			oldGlucoseEntries.WhichMeal = GlucoseEntriesVM.WhichMeal;
    			oldGlucoseEntries.Date = GlucoseEntriesVM.Date;
    			oldGlucoseEntries.Timestamp = GlucoseEntriesVM.Timestamp;
                _db.Entry( oldGlucoseEntries ).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return;
            }

        } // UpdateAsync


        public async Task DeleteAsync( Guid id )
        {
            var GlucoseEntries = await ReadAsync( id );
            if( GlucoseEntries != null )
            {
                _db.GlucoseEntries.Remove( GlucoseEntries );
                await _db.SaveChangesAsync();
            }
            return;

        } // DeleteAsync

    } // Class

} // Namespace