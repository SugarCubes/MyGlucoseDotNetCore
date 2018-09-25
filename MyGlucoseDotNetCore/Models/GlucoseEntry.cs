using System;

namespace MyGlucoseDotNetCore.Models
{
    public class GlucoseEntry
    {
        public Guid Id { get; set; }
        public string PatientUsername { get; set; }
        public Patient Patient { get; set; }
        public float Measurement { get; set; }
        public BeforeAfter BeforeAfter { get; set; }
        public WhichMeal WhichMeal { get; set; }
        public DateTime Date { get; set; }
        public long Timestamp { get; set; }

    }
}