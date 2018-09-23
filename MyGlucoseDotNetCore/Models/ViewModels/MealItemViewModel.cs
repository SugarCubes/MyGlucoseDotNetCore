using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyGlucoseDotNetCore.Models.ViewModels
{
    public class MealItemViewModel
    {
        public MealEntry Meal { get; set; }
        public string Name { get; set; }
        public int Carbs { get; set; }
        public int Servings { get; set; }

		public MealItem GetNewMealItem()
		{
			return new MealItem
			{
    			Meal = Meal,
    			Name = Name,
    			Carbs = Carbs,
    			Servings = Servings
			};

		} // GetNewMealItem

    } // Class

} // Namespace