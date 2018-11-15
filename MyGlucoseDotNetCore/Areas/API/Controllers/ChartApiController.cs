using Microsoft.AspNetCore.Mvc;
using MyGlucoseDotNetCore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyGlucoseDotNetCore.Areas.API.Controllers
{
    [Area("API")]
    public class ChartApiController : Controller
    {
        private IGlucoseEntriesRepository _glucoseEntryRepo;
        private IExerciseEntryRepository _excerciseEntryRepo;
        private IMealEntryRepository _mealEntryRepo;

        public ChartApiController(IGlucoseEntriesRepository glucoseEntriesRepository,
                                  IExerciseEntryRepository excerciseEntryRepo, 
                                  IMealEntryRepository mealEntryRepoository )
        {
            _glucoseEntryRepo = glucoseEntriesRepository;
            _excerciseEntryRepo = excerciseEntryRepo;
            _mealEntryRepo = mealEntryRepoository;

        } // constructor

        public JsonResult GetUserExerciseChart(string UserName)
        {
            var data = _excerciseEntryRepo
                .ReadAll()
                .Where(e => e.UserName == UserName && e.Minutes > 0 )
                .OrderBy(e => e.UpdatedAt);
            return new JsonResult(new { exerciseEntries = data });

        } // GetGlucoseChart


        public JsonResult GetGlucoseChart()
        {
            var data = _glucoseEntryRepo
                .ReadAll()
                .OrderBy(o => o.UpdatedAt);
            return new JsonResult(new { glucoseEntries = data });

        } // GetGlucoseChart


        public JsonResult GetUserGlucoseChart(string UserName)
        {
            var data = _glucoseEntryRepo
                .ReadAll()
                .Where(o => o.UserName == UserName)
                .OrderBy(o => o.UpdatedAt);
            return new JsonResult(new { glucoseEntries = data });

        } // GetGlucoseChart


        public JsonResult GetUserMealChart( string UserName )
        {
            // TODO: Create ReadAll(Username)
            var data = _mealEntryRepo
                .ReadAll()
                .Where(o => o.UserName == UserName)
                .OrderBy(o => o.UpdatedAt);
            return new JsonResult( new { mealEntries = data } );

        } // GetMealChart

    } // class

} // namespace