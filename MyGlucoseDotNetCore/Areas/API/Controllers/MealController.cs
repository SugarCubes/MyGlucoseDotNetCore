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
            IMealItemRepository item )
        {
            _meal = meal;
            _users = users;
            _item = item;

        } // injection constructor


        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateMealEntry( string userName, Guid loginToken, MealEntry mealEntry )
        {
            // Get user from username, verify login token
            var user = await _users.ReadAsync( userName );
            if( user.RemoteLoginToken != loginToken )               // Check login token
            {
                return new JsonResult(                              // Return error
                    new
                    {
                        success = false,
                        errorCode = ErrorCode.INVALID_LOGIN_TOKEN
                    }
                    );

            } // if

            if ( !_meal.ReadAll().Any( o => o.Id == mealEntry.Id ) )// Ensure meal doesn't exist first
            {
                return new JsonResult(                              // If it does, return error
                    new
                    {
                        success = false,
                        errorCode = ErrorCode.ITEM_ALREADY_EXISTS
                    }
                    );
            }

            if ( ModelState.IsValid )
            {
                var newMealEntry = await _meal.CreateAsync( mealEntry );

                return new JsonResult(                              // Return success
                    new
                    {
                        success = true,
                        errorCode = ErrorCode.NO_ERROR,
                        newMealEntry.Id
                    }
                    );

            } // if

            return new JsonResult(                              // Return unknown error
                new
                {
                    success = false,
                    errorCode = ErrorCode.UNKNOWN
                }
                );

        } // CreateMealEntry


        [HttpPost]
        public async Task<IActionResult> CreateMealItem( string userName, Guid loginToken, Guid mealEntryId, MealItem mealItem )
        {
            // Get user from username, verify login token
            var user = await _users.ReadAsync( userName );
            if ( user.RemoteLoginToken != loginToken )               // Check login token
            {
                return new JsonResult(                              // Return error
                    new
                    {
                        success = false,
                        errorCode = ErrorCode.INVALID_LOGIN_TOKEN
                    }
                    );

            } // if

            if ( !_meal.ReadAll().Any( o => o.Id == mealEntryId ) ) // Ensure meal exists first
            {
                return new JsonResult(                              // If not, return invalid meal entry
                    new
                    {
                        success = false,
                        errorCode = ErrorCode.INVALID_MEAL_ENTRY
                    }
                    );
            }

            if ( ModelState.IsValid )                                 // Otherwise, check if model valid
            {
                var newMealItem = await _item.CreateAsync( mealEntryId, mealItem );    // Create item in the DB

                return new JsonResult(                              // Return success
                    new
                    {
                        success = true,
                        errorCode = ErrorCode.NO_ERROR,
                        newMealItem.Id
                    }
                    );
            }// End if model state is valid statement.

            return new JsonResult(                              // Return unknown error
                new
                {
                    success = false,
                    errorCode = ErrorCode.UNKNOWN
                }
                );

        }// End CreateMealItem[Post].


        public async Task<IActionResult> MealEntryUpdate( string userName, Guid loginToken, Guid mealId )
        {
            if ( ModelState.IsValid )
            {
                ApplicationUser user = await _users.ReadAsync(userName);// Read user from the repository
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


                await _meal.UpdateAsync( meal.Id, mealModel );

                return new JsonResult(                                  // This implements IActionResult. If you were 
                        new                                             //      to inspect the output, you would see a 
                        {                                               //      Json-formatted string.
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


        public async Task<IActionResult> MealItemUpdate( Guid itemId, Guid mealId, string Email )
        {
            if ( ModelState.IsValid )
            {
                ApplicationUser user = await _users.ReadAsync(Email);   // Read user from the repository
                user.RemoteLoginToken = Guid.NewGuid();                 // Create a login token, similar to a "session id"

                MealItem item = await _item.ReadAsync(itemId, mealId);

                var itemModel = new MealItemViewModel
                {
                    Meal = item.Meal,
                    Name = item.Name,
                    Carbs = item.Carbs,
                    Servings = item.Servings,
                };

                await _item.UpdateAsync( item.Id, item.MealId, itemModel );

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