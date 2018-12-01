using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyGlucoseDotNetCore.Models
{
    public class Doctor : ApplicationUser
    {
        public string DegreeAbbreviation { get; set; }
        public List<Patient> Patients { get; set; }

        public Doctor()
        {
            Patients = new List<Patient>();

        } // constructor

    } // class

} // namespace
