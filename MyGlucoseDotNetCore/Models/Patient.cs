using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyGlucoseDotNetCore.Models
{
    public class Patient : ApplicationUser
    {
        [Key]
        public string Username { get; set; }
        public List<GlucoseEntry> GlucoseEntries { get; set; }
        public List<ExerciseEntry> ExerciseEntries { get; set; }
        public List<MealEntry> MealEntries { get; set; }

        public Patient()
        {
            GlucoseEntries = new List<GlucoseEntry>();
            ExerciseEntries = new List<ExerciseEntry>();
            MealEntries = new List<MealEntry>();
        }
    }
}