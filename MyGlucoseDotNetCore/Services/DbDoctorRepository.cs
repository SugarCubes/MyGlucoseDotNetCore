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
    public class DbDoctorRepository : IDoctorRepository
    {
        private readonly ApplicationDbContext _db;

        public DbDoctorRepository( ApplicationDbContext db )
        {
            _db = db;

        } // Injection Constructor


        public async Task<Doctor> ReadAsync( string username )
        {
            return await ReadAll()
                .SingleOrDefaultAsync( o => o.UserName == username );

        } // ReadAsync


        public IQueryable<Doctor> ReadAll()
        {
            return _db.Doctors
			.Include( p => p.Patients );

        } // ReadAll


        public async Task<Doctor> CreateAsync( Doctor doctor )
        {
            _db.Doctors.Add( doctor );
            await _db.SaveChangesAsync();
            return doctor;

        } // Create


        public async Task UpdateAsync( string usernameid, DoctorViewModel doctorVM )
        {
            var oldDoctor = await ReadAsync( usernameid );
            if( oldDoctor != null )
            {
    			oldDoctor.DegreeAbbreviation = doctorVM.DegreeAbbreviation;
    			oldDoctor.Patients = doctorVM.Patients;
                _db.Entry( oldDoctor ).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return;
            }

        } // UpdateAsync


        public async Task DeleteAsync( string usernameid )
        {
            var doctor = await ReadAsync( usernameid );
            if( doctor != null )
            {
                _db.Doctors.Remove( doctor );
                await _db.SaveChangesAsync();
            }
            return;

        } // DeleteAsync

        public ApplicationUser ReadDoctor(string email)
        {
            return _db.Users.FirstOrDefault(u => u.Email == email);
        }

        public bool Exists(string firstName)
        {
            return _db.Doctors.Any(fn => fn.FirstName == firstName);
        }

    } // Class

} // Namespace