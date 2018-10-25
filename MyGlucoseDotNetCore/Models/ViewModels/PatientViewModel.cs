using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyGlucoseDotNetCore.Models.ViewModels
{
    public class PatientViewModel //: IdentityUser
    {
        [Key]
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<GlucoseEntry> GlucoseEntries { get; set; }
        public List<ExerciseEntry> ExerciseEntries { get; set; }
        public List<MealEntry> MealEntries { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip1 { get; set; }
        public int Zip2 { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        /*
        public ApplicationUser GetNewPatient()
        {
            return new ApplicationUser 
            {
                UserName = UserName,
                FirstName = FirstName,
                LastName = LastName,
                Address1 = Address1,
                Address2 = Address2,
                City = City,
                State = State,
                Zip1 = Zip1,
                Zip2 = Zip2,
                PhoneNumber = PhoneNumber,
                Email = Email
                //GlucoseEntries = GlucoseEntries,
                //ExerciseEntries = ExerciseEntries,
                //MealEntries = MealEntries
            };

        } // GetNewPatient
        */
    } // class

} // namespace
