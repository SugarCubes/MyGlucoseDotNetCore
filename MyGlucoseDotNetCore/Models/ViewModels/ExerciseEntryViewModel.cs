using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyGlucoseDotNetCore.Models.ViewModels
{
    public class ExerciseEntryViewModel
    {
        public string UserName { get; set; }
        public ApplicationUser User { get; set; }
        public string ExerciseName { get; set; }
        public int Minutes { get; set; }
        public DateTime Date { get; set; }
        public long Timestamp { get; set; }

		public ExerciseEntry GetNewExcerciseEntry()
		{
			return new ExerciseEntry
			{
    			UserName = UserName,
    			User = User,
    			ExerciseName = ExerciseName,
    			Minutes = Minutes,
    			CreatedAt = Date,
    			Timestamp = Timestamp
			};

		} // GetNewExcerciseEntry

    } // Class

} // Namespace