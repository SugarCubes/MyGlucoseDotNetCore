using Microsoft.AspNetCore.Mvc;
using MyGlucoseDotNetCore.Models;
using MyGlucoseDotNetCore.Models.ViewModels;
using MyGlucoseDotNetCore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyGlucoseDotNetCore.Areas.API.Controllers
{
    [Area("API")]
    public class ChartApiController : Controller
    {
        private IGlucoseEntryRepository _glucoseEntryRepo;
        private IExerciseEntryRepository _excerciseEntryRepo;
        private IMealEntryRepository _mealEntryRepo;

        public ChartApiController(IGlucoseEntryRepository glucoseEntriesRepository,
                                  IExerciseEntryRepository excerciseEntryRepo, 
                                  IMealEntryRepository mealEntryRepoository )
        {
            _glucoseEntryRepo = glucoseEntriesRepository;
            _excerciseEntryRepo = excerciseEntryRepo;
            _mealEntryRepo = mealEntryRepoository;

        } // constructor

        public JsonResult GetUserExerciseChart(string UserName)
        {
            var data = from e in _excerciseEntryRepo.ReadAll()
                       where e.UserName == UserName && e.Minutes > 0
                       orderby e.UpdatedAt
                       group e by e.UpdatedAt.ToString("d") into grp
                       let minutes = grp.Sum( s => s.Minutes )
                       select new ChartExerciseViewModel
                       {
                           Minutes = minutes,
                           UpdatedAt = grp.Key
                       };
            //var data = _excerciseEntryRepo.ReadAll()
            //    .Where(e => e.UserName == UserName && e.Minutes > 0 )
            //    .OrderBy(e => e.UpdatedAt)
            //    .GroupBy( e => e.UpdatedAt )
            //    .Select( g => new
            //    {
            //        UpdatedAt = g.Key.ToString("d"),
            //        Minutes = e.Minutes
            //    } );
            return new JsonResult(new { exerciseEntries = data });

        } // GetUserExerciseChart

        public JsonResult GetUserStepChart( string UserName )
        {
            var data = from e in _excerciseEntryRepo.ReadAll()
                       where e.UserName == UserName && e.Steps > 0
                       orderby e.UpdatedAt
                       group e by e.UpdatedAt.ToString("d") into grp
                       let steps = grp.Sum( s => s.Steps )
                       select new ChartStepViewModel
                       {
                           Steps = steps,
                           UpdatedAt = grp.Key
                       };
            //var data = _excerciseEntryRepo
            //    .ReadAll()
            //    .Where(s => s.UserName == UserName && s.Steps > 0 )
            //    .OrderBy(o => o.UpdatedAt)
            //    .GroupBy( d => new { d.UpdatedAt, d.Steps } )
            //    .Select( e => new ChartStepViewModel
            //    {
            //        UpdatedAt = e.Key.UpdatedAt.ToString("d"),
            //        Steps = e.Key.Steps
            //    } );
            return new JsonResult( new { stepEntries = data } );

        } // GetUserExerciseChart


        public JsonResult GetGlucoseChart()
        {
            var data = _glucoseEntryRepo
                .ReadAll()
                .OrderBy(o => o.UpdatedAt);
            return new JsonResult(new { glucoseEntries = data });

        } // GetGlucoseChart


        public JsonResult GetUserGlucoseChart(string UserName)
        {
            var data = from e in _glucoseEntryRepo.ReadAll()
                       where e.UserName == UserName && e.Measurement > 0
                       orderby e.UpdatedAt
                       group e by e.UpdatedAt.ToString("d") into grp
                       let average = grp.Average( s => s.Measurement )
                       select new ChartGlucoseViewModel
                       {
                           Measurement = average,
                           UpdatedAt = grp.Key
                       };
            //var data = _glucoseEntryRepo
            //    .ReadAll()
            //    .Where(o => o.UserName == UserName)
            //    .OrderBy(o => o.UpdatedAt)
            //    .GroupBy( d => new { d.UpdatedAt, d.Measurement } )
            //    .Select( e => new ChartGlucoseViewModel
            //    {
            //        UpdatedAt = e.Key.UpdatedAt.ToString("d"),
            //        Measurement = e.Key.Measurement
            //    } );
            return new JsonResult(new { glucoseEntries = data });

        } // GetGlucoseChart


        public JsonResult GetUserMealChart( string UserName )
        {
            var data = from e in _mealEntryRepo.ReadAll()
                       where e.UserName == UserName && e.TotalCarbs > 0
                       orderby e.UpdatedAt
                       group e by e.UpdatedAt.ToString("d") into grp
                       let dailyCarbs = grp.Sum( s => s.TotalCarbs )
                       select new ChartMealViewModel
                       {
                           TotalCarbs = dailyCarbs,
                           UpdatedAt = grp.Key
                       };
            // TODO: Create ReadAll(Username)
            //var data = _mealEntryRepo
            //    .ReadAll()
            //    .Where(o => o.UserName == UserName)
            //    .OrderBy(o => o.UpdatedAt)
            //    .GroupBy( d => new { d.UpdatedAt, d.TotalCarbs } )
            //    .Select( e => new ChartMealViewModel
            //    {
            //        UpdatedAt = e.Key.UpdatedAt.ToString("d"),
            //        TotalCarbs = e.Key.TotalCarbs
            //    } );
            return new JsonResult( new { mealEntries = data } );

        } // GetMealChart

    } // class

} // namespace