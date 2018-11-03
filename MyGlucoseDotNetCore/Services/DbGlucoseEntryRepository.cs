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


        public async Task<GlucoseEntry> ReadAsync( Guid id )
        {
            return await ReadAll()
                .SingleOrDefaultAsync( o => o.Id == id );

        } // ReadAsync


        public IQueryable<GlucoseEntry> ReadAll()
        {
            return _db.GlucoseEntries;

        } // ReadAll


        public async Task<GlucoseEntry> CreateAsync( GlucoseEntry GlucoseEntries )
        {
            _db.GlucoseEntries.Add( GlucoseEntries );
            await _db.SaveChangesAsync();
            return GlucoseEntries;

        } // Create


        public async Task UpdateAsync( Guid id, GlucoseEntry GlucoseEntry )
        {
            var oldGlucoseEntries = await ReadAsync( id );
            if( oldGlucoseEntries != null )
            {
    			oldGlucoseEntries.PatientUsername = GlucoseEntry.PatientUsername;
    			oldGlucoseEntries.Patient = GlucoseEntry.Patient;
    			oldGlucoseEntries.Measurement = GlucoseEntry.Measurement;
    			oldGlucoseEntries.BeforeAfter = GlucoseEntry.BeforeAfter;
    			oldGlucoseEntries.WhichMeal = GlucoseEntry.WhichMeal;
    			oldGlucoseEntries.CreatedAt = GlucoseEntry.CreatedAt;
                oldGlucoseEntries.UpdatedAt = GlucoseEntry.UpdatedAt;
                oldGlucoseEntries.Timestamp = GlucoseEntry.Timestamp;
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


        public async Task CreateOrUpdateEntries( ICollection<GlucoseEntry> glucoseEntries )
        {
            foreach ( GlucoseEntry glucoseEntry in glucoseEntries )
            {
                GlucoseEntry dbGlucoseEntry = await ReadAsync( glucoseEntry.Id );
                if ( dbGlucoseEntry == null )                  // If meal entry doesn't exist
                {
                    // Create in the database
                    await CreateAsync( glucoseEntry );

                }
                else if ( dbGlucoseEntry.UpdatedAt < glucoseEntry.UpdatedAt )
                {
                    // Update in the database
                    await UpdateAsync( glucoseEntry.Id, glucoseEntry );

                }

            } // foreach MealEntry

            return;

        } // CreateOrUpdateEntries

    } // Class

} // Namespace