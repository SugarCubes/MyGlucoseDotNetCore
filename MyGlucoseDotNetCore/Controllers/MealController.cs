using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyGlucoseDotNetCore.Models;
using MyGlucoseDotNetCore.Models.ViewModels;
using MyGlucoseDotNetCore.Services.Interfaces;

namespace MyGlucoseDotNetCore.Controllers
{
    public class MealController : Controller
    {
        /************Meal Data variables ***************/
        //private readonly MealManager<MealEntry> _mealManager;
        private IMealEntryRepository _meal;

        /**********************************************/

        public MealController(IMealEntryRepository meal)
        {
            _meal = meal;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> MealSync(Guid mealId)
        {
            if (ModelState.IsValid)
            {
                MealEntry meal = await _meal.ReadAsync(mealId);

                var mealModel = new MealEntryViewModel
                {
                    UserName = meal.UserName,
                    User = meal.User,
                    TotalCarbs = meal.TotalCarbs,
                    Date = meal.Date,
                    Timestamp = meal.Timestamp,
                    MealItems = meal.MealItems,
                };

                
                await _meal.UpdateAsync(meal.Id, mealModel);

                return new JsonResult(                                  // This implements IActionResult. If you were 
                        new                                                 //      to inspect the output, you would see a 
                        {                                                   //      Json-formatted string.
                            success = true,
                            errorCode = ErrorCode.NO_ERROR,
                            remoteMealToken = _meal.ToString(),
                            meal.UserName,
                            meal.User,
                            meal.TotalCarbs,
                            meal.Date,
                            meal.Timestamp,
                            meal.MealItems
                        }
                        );

            }//end if(ModelState.IsValid)

            return new JsonResult(
                    new
                    {
                        success = false,
                        errorCode = ErrorCode.UNKNOWN
                    }
                );
        }//end MealSync
    }//end MealController
}//end MyGlucose namespace