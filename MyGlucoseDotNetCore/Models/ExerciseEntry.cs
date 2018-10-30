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
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public long Timestamp { get; set; }

        public override string ToString()
        {
            return "EXERCISE ENTRY:"
                + "\nId: " + Id
                + "\nUserName: " + UserName
                + "\nExercise name: " + ExerciseName
                + "\nMinutes: " + Minutes
                + "\nDate: " + CreatedAt
                + "\nTimestamp: " + Timestamp;

        } // ToString

    } // class

} // namespace