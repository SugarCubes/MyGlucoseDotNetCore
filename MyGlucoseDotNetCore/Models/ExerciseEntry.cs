﻿using System;

namespace MyGlucoseDotNetCore.Models
{
    public class ExerciseEntry
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public Patient Patient { get; set; }
        public string Name { get; set; }
        public int Minutes { get; set; }
        public int Steps { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public long Timestamp { get; set; }

        public override string ToString()
        {
            return "EXERCISE ENTRY:"
                + "\nId: " + Id
                + "\nUserName: " + UserName
                + "\nExercise name: " + Name
                + "\nMinutes: " + Minutes
                + "\nSteps" + Steps
                + "\nDate: " + CreatedAt
                + "\nTimestamp: " + Timestamp;

        } // ToString

    } // class

} // namespace