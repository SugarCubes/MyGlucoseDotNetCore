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
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public long Timestamp { get; set; }

        public override string ToString()
        {
            return "GLUCOSE ENTRY:"
                + "\nId: " + Id
                + "\nPatient: " + PatientUsername
                + "\nMeasurement: " + Measurement
                + "\nBefore/After: " + BeforeAfter.ToString()
                + "\nWhich meal: " + WhichMeal.ToString()
                + "\nDate: " + CreatedAt
                + "\nTimestamp: " + Timestamp;

        } // ToString

    } // class

} // namespace