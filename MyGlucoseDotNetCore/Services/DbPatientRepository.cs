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
    public class DbPatientRepository : IPatientRepository
    {
        private readonly ApplicationDbContext _db;

        public DbPatientRepository( ApplicationDbContext db )
        {
            _db = db;

        } // Injection Constructor


        public async Task<Patient> ReadAsync( string username )
        {
            return await ReadAll()
                .SingleOrDefaultAsync( o => o.UserName == username );

        } // ReadAsync


        public IQueryable<Patient> ReadAll()
        {
            return _db.Patients
			.Include( g => g.GlucoseEntries )
			.Include( e => e.ExerciseEntries )
			.Include( m => m.MealEntries )
				.ThenInclude( mi => mi.MealItems );

        } // ReadAll


        public async Task<Patient> CreateAsync( Patient patient )
        {
            _db.Patients.Add( patient );
            await _db.SaveChangesAsync();
            return patient;

        } // Create


        public async Task UpdateAsync( string username, PatientViewModel patientVM )
        {
            var oldPatient = await ReadAsync( username );
            if( oldPatient != null )
            {
    			oldPatient.GlucoseEntries = patientVM.GlucoseEntries;
    			oldPatient.ExerciseEntries = patientVM.ExerciseEntries;
    			oldPatient.MealEntries = patientVM.MealEntries;
                _db.Entry( oldPatient ).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return;
            }

        } // UpdateAsync


        public async Task DeleteAsync( string username )
        {
            var patient = await ReadAsync( username );
            if( patient != null )
            {
                _db.Patients.Remove( patient );
                await _db.SaveChangesAsync();
            }
            return;

        } // DeleteAsync

    } // Class

} // Namespace