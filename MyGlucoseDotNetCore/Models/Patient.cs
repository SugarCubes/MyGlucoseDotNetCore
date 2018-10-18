using System.Collections.Generic;

namespace MyGlucoseDotNetCore.Models
{
    public class Patient : ApplicationUser
    {
        public List<GlucoseEntry> GlucoseEntries { get; set; }
        public List<ExerciseEntry> ExcerciseEntries { get; set; }
        public List<MealEntry> MealEntries { get; set; }

        public Patient()
        {
            GlucoseEntries = new List<GlucoseEntry>();
            ExcerciseEntries = new List<ExerciseEntry>();
            MealEntries = new List<MealEntry>();
        }
    }
}