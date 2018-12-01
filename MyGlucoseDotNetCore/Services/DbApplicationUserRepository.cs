using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyGlucoseDotNetCore.Data;
using MyGlucoseDotNetCore.Models;
using MyGlucoseDotNetCore.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace MyGlucoseDotNetCore.Services
{
    public class DbApplicationUserRepository : IApplicationUserRepository
    {
        private ApplicationDbContext _db;


        private UserManager<ApplicationUser> _userManager;

        public DbApplicationUserRepository( ApplicationDbContext db,
                                            UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        } // constructor

        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = await ReadAsync(email);

            if (user != null)
            {
                if (!user.HasRole(roleName))
                {
                    _userManager.AddToRoleAsync(user, roleName).Wait();
                    return true;
                }
            }
            return false;
        }

        public ApplicationUser ReadRoleUser(string email)
        {
            ApplicationUser appUser = null;
            appUser = _db.Users.FirstOrDefault(u => u.Email == email);
            if (appUser != null)
            {
                AddRoles(appUser);
            }
            return appUser;

        }

        private void AddRoles(ApplicationUser user)
        {
            var roleIds = _db.UserRoles.Where(ur => ur.UserId == user.Id).Select(ur => ur.RoleId);
            foreach (var roleId in roleIds)
            {
                user.Roles.Add(_db.Roles.Find(roleId));
            }
        }

        public async Task<ApplicationUser> ReadAsync( string username )
        {
            return await ReadAll()
                .SingleOrDefaultAsync( o => o.UserName == username );
        }

        public IQueryable<ApplicationUser> ReadAll()
        {
            return _db.Users;

        }

        public async Task<ApplicationUser> CreateAsync( ApplicationUser applicationUser )
        {
            _db.Users.Add( applicationUser );
            await _db.SaveChangesAsync();
            return applicationUser;

        }

        public async Task UpdateAsync( string username, ApplicationUser applicationUser )
        {
            var oldUser = await ReadAsync( username );
            if ( oldUser != null )
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
            }

        }

        public async Task DeleteAsync( string username )
        {
            var user = await ReadAsync( username );
            if ( user != null )
            {
                _db.Users.Remove( user );
                await _db.SaveChangesAsync();
            }
            return;

        }
        

    }

}
