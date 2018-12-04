using Microsoft.EntityFrameworkCore;
using MyGlucoseDotNetCore.Data;
using MyGlucoseDotNetCore.Models;
using MyGlucoseDotNetCore.Services.Interfaces;
using System;
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


        public async Task UpdateAsync( string username, Patient patient )
        {
            var oldPatient = await ReadAsync( username );
            if ( oldPatient != null )
            {
                oldPatient.FirstName = patient.FirstName;
                oldPatient.LastName = patient.LastName;
                oldPatient.Address1 = patient.Address1;
                oldPatient.Address2 = patient.Address2;
                oldPatient.City = patient.City;
                oldPatient.State = patient.State;
                oldPatient.Zip1 = patient.Zip1;
                oldPatient.Zip2 = patient.Zip2;
                oldPatient.PhoneNumber = patient.PhoneNumber;
                oldPatient.Email = patient.Email;
                oldPatient.CreatedAt = patient.CreatedAt;
                oldPatient.UpdatedAt = patient.UpdatedAt;
                oldPatient.RemoteLoginToken = patient.RemoteLoginToken; // In case it has changed
                oldPatient.Height = patient.Height;
                oldPatient.Weight = patient.Weight;
                oldPatient.DoctorUserName = patient.DoctorUserName;
                if ( oldPatient.Doctor != null && patient.Doctor != null
                    && oldPatient.Doctor.Id == patient.Doctor.Id )
                    _db.Entry( patient.Doctor ).State = EntityState.Unchanged;
                //oldPatient.Doctor = null;
                //oldPatient.DoctorId = patient.DoctorId;
                //var doctor = await _db.Doctors
                //    .SingleOrDefaultAsync( u => u.UserName == patient.DoctorUserName );
                //if ( doctor != null )
                //    oldPatient.Doctor = patient.Doctor;
                // Tries to insert duplicate entries:
                //if ( patient.GlucoseEntries != null )
                //    oldPatient.GlucoseEntries = patient.GlucoseEntries;
                //if ( patient.ExerciseEntries != null )
                //    oldPatient.ExerciseEntries = patient.ExerciseEntries;
                //if ( patient.MealEntries != null )
                //    oldPatient.MealEntries = patient.MealEntries;
                _db.Entry( oldPatient ).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return;
            }

        } // UpdateAsync


        public async Task DeleteAsync( string username )
        {
            var patient = await ReadAsync( username );
            if ( patient != null )
            {
                _db.Patients.Remove( patient );
                await _db.SaveChangesAsync();
            }
            return;

        } // DeleteAsync

        public bool Exists(string userName)
        {
            //var patient = userName.GetNewPatient();
            return _db.Patients.Any(u => u.UserName == userName);
			
        }
    } // Class

} // Namespace