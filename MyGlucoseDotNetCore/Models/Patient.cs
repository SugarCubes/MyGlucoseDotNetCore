using System.Collections.Generic;

namespace MyGlucoseDotNetCore.Models
{
    public class Patient : ApplicationUser
    {
        public List<GlucoseEntry> GlucoseEntries { get; set; }
        public List<ExcerciseEntry> ExcerciseEntries { get; set; }
        public List<MealEntry> MealEntries { get; set; }

        public Patient()
        {
            GlucoseEntries = new List<GlucoseEntry>();
            ExcerciseEntries = new List<ExcerciseEntry>();
            MealEntries = new List<MealEntry>();
        }
    }
}