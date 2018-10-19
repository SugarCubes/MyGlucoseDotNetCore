using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyGlucoseDotNetCore.Models.ViewModels
{
    public class PatientViewModel //: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<GlucoseEntries> GlucoseEntries { get; set; }
        public List<ExerciseEntry> ExerciseEntries { get; set; }
        public List<MealEntry> MealEntries { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }


        public ApplicationUser GetNewUser()
        {
            return new ApplicationUser
            {
                FirstName = FirstName,
                LastName = LastName,
                Address1 = Address1,
                Address2 = Address2,
                City = City,
                State = State,
                Zip1 = Zip
            };

        } // GetNewUser
    }
}
