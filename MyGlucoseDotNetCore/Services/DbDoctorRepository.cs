using Microsoft.EntityFrameworkCore;
using MyGlucoseDotNetCore.Data;
using MyGlucoseDotNetCore.Models;
using MyGlucoseDotNetCore.Services.Interfaces;
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


        public IQueryable<Doctor> ReadAll()
        {
            return _db.Doctors;

        } // ReadAll


        public async Task<Doctor> ReadAsync( string username )
        {
            return await ReadAll()
                .Include( p => p.Patients )
                .SingleOrDefaultAsync( o => o.UserName == username );

        } // ReadAsync


        public async Task<Doctor> CreateAsync( Doctor doctor )
        {
            _db.Doctors.Add( doctor );
            await _db.SaveChangesAsync();
            return doctor;

        } // Create


        public async Task UpdateAsync( string userName, Doctor doctor )
        {
            var oldDoctor = await ReadAsync( userName );
            if( oldDoctor != null )
            {
                oldDoctor.DegreeAbbreviation = doctor.DegreeAbbreviation;
                if( doctor.Patients != null )
                    oldDoctor.Patients = doctor.Patients;
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

        public ApplicationUser ReadDoctor( string email )
        {
            return _db.Users.FirstOrDefault( u => u.Email == email );
        }

        public bool Exists( string firstName )
        {
            return _db.Doctors.Any( fn => fn.FirstName == firstName );
        }

    } // Class

} // Namespace