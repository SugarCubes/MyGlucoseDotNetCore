using System;

namespace MyGlucoseDotNetCore.Models
{
    public class ExerciseEntry
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public ApplicationUser User { get; set; }
        public string ExerciseName { get; set; }
        public int Minutes { get; set; }
        public DateTime Date { get; set; }
        public long Timestamp { get; set; }

    }
}