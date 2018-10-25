using System.Collections.Generic;

namespace MyGlucoseDotNetCore.Models
{
    public class Patient : ApplicationUser
    {
        public Doctor Doctor { get; set; }
        public List<GlucoseEntry> GlucoseEntries { get; set; }
        public List<ExerciseEntry> ExerciseEntries { get; set; }
        public List<MealEntry> MealEntries { get; set; }

        public Patient()
        {
            Doctor = new Doctor();
            GlucoseEntries = new List<GlucoseEntry>();
            ExerciseEntries = new List<ExerciseEntry>();
            MealEntries = new List<MealEntry>();
        }
    }
}