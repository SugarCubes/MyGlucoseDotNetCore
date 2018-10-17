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
        private IApplicationUserRepository _users;
        private IMealItemRepository _item;

        /**********************************************/

        public MealController(
            IApplicationUserRepository users, 
            IMealEntryRepository meal,
            IMealItemRepository item)
        {
            _meal = meal;
            _users = users;
            _item = item;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateMealEntry()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateMealEntry(MealEntry mealEntry)
        {
            if (ModelState.IsValid)
            {
                _meal.Create(mealEntry);
                return RedirectToAction("Index");
            }
            return View(mealEntry);
        }

        public IActionResult CreateMealItem([Bind(Prefix = "id")] Guid mealEntryId)
        {
            var mealEntry = _meal.Read(mealEntryId);
            if(mealEntry == null)
            {
                /*******************************/
                //This will need to change once we create the views
                /*************************/
                return RedirectToAction("Index");
            }
            // Need to remember to create a view named "MealEntry"
            ViewData["MealEntry"] = mealEntry;
            return View();
        }

        [HttpPost]
        public IActionResult CreateMealItem(Guid mealEntryId, MealItem mealItem)
        {
            if (ModelState.IsValid)
            {
                //mealItem.Id = 0;
                _item.Create(mealEntryId, mealItem);
                /*******************************/
                //This will need to change once we create the views
                /*************************/
                return RedirectToAction("Index");
            }// End if model state is valid statement.
            // Need to remember to create a view named "MealItem"
            ViewData["MealItem"] = _meal.Read(mealEntryId);
            return View(mealItem);
        }// End CreateMealItem[Post].

        public async Task<IActionResult> MealEntrySync(Guid mealId, string Email)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _users.ReadAsync(Email); // Read user from the repository
                user.RemoteLoginToken = Guid.NewGuid();                 // Create a login token, similar to a "session id"
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
        }//end MealEntrySync

        public async Task<IActionResult> MealItemSync(Guid itemId, Guid mealId, string Email)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _users.ReadAsync(Email); // Read user from the repository
                user.RemoteLoginToken = Guid.NewGuid();                 // Create a login token, similar to a "session id"

                MealItem item = await _item.ReadAsync(itemId, mealId);

                var itemModel = new MealItemViewModel
                {
                    Meal = item.Meal,
                    Name = item.Name,
                    Carbs = item.Carbs,
                    Servings = item.Servings,
                };

                await _item.UpdateAsync(item.Id, item.MealId, itemModel);

                return new JsonResult(                                  // This implements IActionResult. If you were 
                       new                                                 //      to inspect the output, you would see a 
                        {                                                   //      Json-formatted string.
                           success = true,
                           errorCode = ErrorCode.NO_ERROR,
                           remoteItemToken = _item.ToString(),
                           item.Id,
                           item.MealId,
                           item.Meal,
                           item.Name,
                           item.Carbs,
                           item.Servings
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

        }//end MealItemSync

            }//end MealController
}//end MyGlucose namespace