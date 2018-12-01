using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyGlucoseDotNetCore.Models;

namespace MyGlucoseDotNetCore.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public DbSet<ExerciseEntry> ExerciseEntries { get; set; }
        public DbSet<GlucoseEntry> GlucoseEntries { get; set; }
        public DbSet<MealEntry> MealEntries { get; set; }
        public DbSet<MealItem> MealItems { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        //public DbSet<ApplicationUserRole> UserRoles { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<ApplicationUserRole>()
                .HasKey( k => new { k.UserId, k.RoleId } );

            builder.Entity<ApplicationUser>()
                .HasMany( o => o.Roles )
                .WithOne( o => o.User )
                .HasForeignKey( o => o.UserId );

            builder.Entity<ApplicationRole>()
                .HasMany( o => o.Users )
                .WithOne( o => o.Role )
                .HasForeignKey( o => o.RoleId );

            builder.Entity<Patient>()
                .HasMany( o => o.GlucoseEntries )
                .WithOne( o => o.Patient )
                .HasForeignKey( o => o.UserName );

            builder.Entity<Patient>()
                .HasMany( o => o.MealEntries )
                .WithOne( o => o.Patient )
                .HasForeignKey( o => o.UserName );

            //builder.Entity<ApplicationUser>()
            //    .HasMany(r => r.Roles)
            //    .WithOne(u => u.ApplicationRole)

            builder.Entity<Patient>()
                .HasMany( o => o.ExerciseEntries )
                .WithOne( o => o.Patient )
                .HasForeignKey( o => o.UserName );

            builder.Entity<Doctor>()
                .HasMany( o => o.Patients )
                .WithOne( o => o.Doctor );

            builder.Entity<MealEntry>()
                .HasMany( o => o.MealItems )
                .WithOne( o => o.Meal );
            
            


            //builder.Entity<MealItem>()
            //    .HasOne( o => o.Meal )
            //    .WithMany( o => o.MealItems );

        }
    }
}
