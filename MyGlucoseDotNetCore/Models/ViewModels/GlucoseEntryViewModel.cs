using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyGlucoseDotNetCore.Models.ViewModels
{
    public class GlucoseEntriesViewModel
    {
        public string PatientUsername { get; set; }
        public Patient Patient { get; set; }
        public float Measurement { get; set; }
        public BeforeAfter BeforeAfter { get; set; }
        public WhichMeal WhichMeal { get; set; }
        public DateTime Date { get; set; }
        public long Timestamp { get; set; }

		public GlucoseEntries GetNewGlucoseEntries()
		{
			return new GlucoseEntries
			{
    			PatientUsername = PatientUsername,
    			Patient = Patient,
    			Measurement = Measurement,
    			BeforeAfter = BeforeAfter,
    			WhichMeal = WhichMeal,
    			Date = Date,
    			Timestamp = Timestamp
			};

		} // GetNewGlucoseEntries

    } // Class

} // Namespace