using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyGlucoseDotNetCore.Services.Interfaces;
using MyGlucoseDotNetCore.Models;
using MyGlucoseDotNetCore.Models.ViewModels;

namespace MyGlucoseDotNetCore.Areas.API.Controllers
{
    public class GlucoseController : Controller
    {
        private IGlucoseEntryRepository _glucose;
        private IApplicationUserRepository _users;

        public GlucoseController(IGlucoseEntryRepository glucose, IApplicationUserRepository users)
        {
            _glucose = glucose;
            _users = users;

        } // injection constructor

        [HttpPost]
        public async Task<IActionResult> CreateGlucoseEntry(string userName, Guid loginToken, GlucoseEntry glucoseEntry)
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

            if (!_glucose.ReadAll().Any(o => o.Id == glucoseEntry.Id))// Ensure glucose doesn't exist first
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
                var newGlucoseEntry = await _glucose.CreateAsync(glucoseEntry);

                return new JsonResult(                              // Return success
                    new
                    {
                        success = true,
                        errorCode = ErrorCode.NO_ERROR,
                        newGlucoseEntry.Id
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

        } // CreateGlucoseEntry


        public async Task<IActionResult> GlucoseEntryUpdate(string userName, Guid loginToken, Guid glucoseId)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _users.ReadAsync(userName);// Read user from the repository
                user.RemoteLoginToken = Guid.NewGuid();                 // Create a login token, similar to a "session id"
                GlucoseEntry glucose = await _glucose.ReadAsync(glucoseId);

                var glucoseModel = new GlucoseEntriesViewModel
                {
                    PatientUsername = glucose.UserName,
                    Patient = glucose.Patient,
                    Measurement = glucose.Measurement,
                    BeforeAfter = glucose.BeforeAfter,
                    WhichMeal = glucose.WhichMeal,
                    Date = glucose.CreatedAt,
                    Timestamp = glucose.Timestamp,
                };


                await _glucose.UpdateAsync(glucose.Id, glucoseModel.GetNewGlucoseEntries());

                return new JsonResult(                                  // This implements IActionResult. If you were 
                        new                                             //      to inspect the output, you would see a 
                        {                                               //      Json-formatted string.
                            success = true,
                            errorCode = ErrorCode.NO_ERROR,
                            remoteGlucoseToken = _glucose.ToString(),
                            glucose.UserName,
                            glucose.Patient,
                            glucose.Measurement,
                            glucose.BeforeAfter,
                            glucose.WhichMeal,
                            glucose.CreatedAt,
                            glucose.Timestamp
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

        }//end GlucoseEntryUpdate



        public IActionResult Index()
        {
            return View();
        }

        // GET: GlucoseEntry/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: GlucoseEntry/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GlucoseEntry/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GlucoseEntry/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: GlucoseEntry/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GlucoseEntry/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GlucoseEntry/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}