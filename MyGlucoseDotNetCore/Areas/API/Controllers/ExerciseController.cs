using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyGlucoseDotNetCore.Models;
using MyGlucoseDotNetCore.Models.ViewModels;
using MyGlucoseDotNetCore.Services.Interfaces;
/*********************************************/
//  Created by Heather Harvey with advice from Natash Ince and J.T. Blevins
/*********************************************/
namespace MyGlucoseDotNetCore.Areas.API.Controllers
{
    public class ExerciseController : Controller
    {
        /************Exercise Data variables ***************/
        private IExerciseEntryRepository _exercise;
        private IApplicationUserRepository _users;
        /**********************************************/

        public ExerciseController(
            IApplicationUserRepository users,
            IExerciseEntryRepository exercise)
        {
            _exercise = exercise;
            _users = users;
        } // injection constructor

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateExerciseEntry(string userName, Guid loginToken, ExerciseEntry exerciseEntry)
        {
            // Get user from username, verify login token
            var user = await _users.ReadAsync(userName);
            if (user.RemoteLoginToken != loginToken)               // Check login token
            {
                return new JsonResult(                              // Return error
                    new
                    {
                        success = false,
                        errorCode = ErrorCode.INVALID_LOGIN_TOKEN
                    }
                    );

            } // if

            if (!_exercise.ReadAll().Any(o => o.Id == exerciseEntry.Id))// Ensure exercise doesn't exist first
            {
                return new JsonResult(                              // If it does, return error
                    new
                    {
                        success = false,
                        errorCode = ErrorCode.ITEM_ALREADY_EXISTS
                    }
                    );
            }

            if (ModelState.IsValid)
            {
                var newExerciseEntry = await _exercise.CreateAsync(exerciseEntry);

                return new JsonResult(                              // Return success
                    new
                    {
                        success = true,
                        errorCode = ErrorCode.NO_ERROR,
                        newExerciseEntry.Id
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

        } // CreateExerciseEntry

        public async Task<IActionResult> ExerciseEntryUpdate(string userName, Guid loginToken, Guid exerciseId)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _users.ReadAsync(userName);// Read user from the repository
                user.RemoteLoginToken = Guid.NewGuid();                 // Create a login token, similar to a "session id"
                ExerciseEntry exercise = await _exercise.ReadAsync(exerciseId);

                var exerciseModel = new ExerciseEntryViewModel
                {
                    UserName = exercise.UserName,
                    Patient = exercise.Patient,
                    ExerciseName = exercise.Name,
                    Minutes = exercise.Minutes,
                    Date = exercise.CreatedAt,
                    Timestamp = exercise.Timestamp,
                };


                await _exercise.UpdateAsync(exercise.Id, exerciseModel.GetNewExcerciseEntry());

                return new JsonResult(                                  // This implements IActionResult. If you were 
                        new                                             //      to inspect the output, you would see a 
                        {                                               //      Json-formatted string.
                            success = true,
                            errorCode = ErrorCode.NO_ERROR,
                            remoteMealToken = _exercise.ToString(),
                            exercise.UserName,
                            exercise.Patient,
                            exercise.Name,
                            exercise.Minutes,
                            exercise.CreatedAt,
                            exercise.Timestamp
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

        }//end ExerciseEntrySync
    }//end ExerciseController
}//end namespace