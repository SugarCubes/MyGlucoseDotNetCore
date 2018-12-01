using System;
using System.Collections.Generic;

namespace MyGlucoseDotNetCore.Models
{
    public class Patient : ApplicationUser
    {
        public string DoctorUserName { get; set; }
        public string DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        //public ApplicatonRole Role { get; set; }
        public List<GlucoseEntry> GlucoseEntries { get; set; }
        public List<ExerciseEntry> ExerciseEntries { get; set; }
        public List<MealEntry> MealEntries { get; set; }

        public Patient()
        {
            Doctor = new Doctor();
            GlucoseEntries = new List<GlucoseEntry>();
            ExerciseEntries = new List<ExerciseEntry>();
            MealEntries = new List<MealEntry>();

        } // constructor


        /// <summary>Returns a <see cref="System.String" /> that represents this instance.</summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            string glucoseString = "";
            foreach ( GlucoseEntry entry in GlucoseEntries )
                glucoseString += "\n" + entry.ToString();

            string exerciseString = "";
            foreach ( ExerciseEntry entry in ExerciseEntries )
                exerciseString += "\n" + entry.ToString();

            string mealEntryString = "";
            foreach ( MealEntry entry in MealEntries )
                mealEntryString += "\n" + entry.ToString();

            return "First Name: " + FirstName
                + "\nLast Name: " + LastName
                + "\nEmail: " + Email
                + "\nPhone Number: " + PhoneNumber
                + "\nAddress 1: " + Address1
                + "\nAddress 2: " + Address2
                + "\nCity: " + City
                + "\nState: " + State
                + "\nZip1: " + Zip1
                + "\nZip2: " + Zip2
                + "\nUserName: " + UserName
                + "\nGlucoseEntries: " + glucoseString
                + "\nExerciseEntries: " + exerciseString
                + "\nMealEntries: " + mealEntryString
                + "\nDoctorUserName: " + DoctorUserName;

        } // ToString

    } // class

} // namespace