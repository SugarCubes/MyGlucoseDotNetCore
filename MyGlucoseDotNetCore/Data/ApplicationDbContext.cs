using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyGlucoseDotNetCore.Models;
using MyGlucoseDotNetCore.Models.ViewModels;

namespace MyGlucoseDotNetCore.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public DbSet<ExerciseEntry> ExerciseEntries { get; set; }
        public DbSet<GlucoseEntry> GlucoseEntries { get; set; }
        public DbSet<MealEntry> MealEntries { get; set; }
        public DbSet<MealItem> MealItems { get; set; }
        public DbSet<Patient> Patients { get; set; }

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

            builder.Entity<Patient>()
                .HasMany( o => o.GlucoseEntries )
                .WithOne( o => o.Patient );

        }

        public DbSet<MyGlucoseDotNetCore.Models.ViewModels.PatientViewModel> PatientViewModel { get; set; }
    }
}
