using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyGlucoseDotNetCore.Data;
using MyGlucoseDotNetCore.Models;
using MyGlucoseDotNetCore.Services.Interfaces;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MyGlucoseDotNetCore.Services
{
    public class DbApplicationUserRepository : IApplicationUserRepository
    {
        private ApplicationDbContext _db;


        private UserManager<ApplicationUser> _userManager;

        public DbApplicationUserRepository( ApplicationDbContext db,
                                            UserManager<ApplicationUser> userManager )
        {
            _db = db;
            _userManager = userManager;

        } // constructor


        public bool UserExists( string userName )
        {
            return _db.Users.Any( o => o.UserName == userName );

        } // UserExists


        public async Task<bool> AssignRole( string email, string roleName )
        {
            var user = await ReadAsync(email);

            if( user != null )
            {
                if( !user.HasRole( roleName ) )
                {
                    Debug.WriteLine( "User doesn't have role '" + roleName + "'. Adding..." );
                    await _userManager.AddToRoleAsync( user, roleName );//.Wait();
                    return true;
                }
                else
                    Debug.WriteLine( "User has role '" + roleName + "'." );
            }
            return false;

        } // AssignRole


        public ApplicationUser ReadUser( string email )
        {
            ApplicationUser appUser = _db.Users
                .Include( r => r.Roles )
                .FirstOrDefault( u => u.Email == email );
            
            return appUser;

        }

        public async Task<ApplicationUser> ReadAsync( string username )
        {
            return await ReadAll()
                .Include( r => r.Roles )
                .SingleOrDefaultAsync( o => o.UserName == username );

        } // ReadAsync


        public IQueryable<ApplicationUser> ReadAll()
        {
            return _db.Users
                .Include( r => r.Roles );

        } // ReadAll


        public async Task<ApplicationUser> CreateAsync( ApplicationUser applicationUser )
        {
            _db.Users.Add( applicationUser );
            await _db.SaveChangesAsync();
            return applicationUser;

        } // CreateAsync


        public async Task UpdateAsync( string username, ApplicationUser applicationUser )
        {
            var oldUser = await ReadAsync( username );
            if( oldUser != null )
            {
                oldUser.UserName = applicationUser.UserName;
                oldUser.Address1 = applicationUser.Address1;
                oldUser.Address2 = applicationUser.Address2;
                oldUser.City = applicationUser.City;
                oldUser.Email = applicationUser.Email;
                oldUser.LastName = applicationUser.LastName;
                oldUser.PhoneNumber = applicationUser.PhoneNumber;
                oldUser.State = applicationUser.State;
                oldUser.Zip1 = applicationUser.Zip1;
                oldUser.Zip2 = applicationUser.Zip2;

                _db.Entry( oldUser ).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return;

            } // if

        } // UpdateAsync


        public async Task DeleteAsync( string username )
        {
            var user = await ReadAsync( username );
            if( user != null )
            {
                _db.Users.Remove( user );
                await _db.SaveChangesAsync();
            }
            return;

        } // DeleteAsync


        public List<ApplicationRole> ReadAllRoles()
        {
            return _db.Roles.ToList();

        } // ReadAllRoles


        //public bool HasRole(string roleName)
        //{
        //    string role = Read(roleName).ToString();
        //    if(roleName == role)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        //public ApplicationRole Read(string role)
        //{
        //    return _db.Roles.FirstOrDefault(r => r.Role == role);
        //}
    }

}
