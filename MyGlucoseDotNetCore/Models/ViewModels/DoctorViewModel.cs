using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyGlucoseDotNetCore.Models.ViewModels
{
    public class DoctorViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Patient> Patients { get; set; }
        public string IdAbbrev { get; set; }
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
        }
    }
}
