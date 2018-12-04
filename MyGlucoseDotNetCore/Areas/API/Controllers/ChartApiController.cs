using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyGlucoseDotNetCore.Models;
using MyGlucoseDotNetCore.Models.ViewModels;
using MyGlucoseDotNetCore.Services.Interfaces;
using System;
using System.Linq;

namespace MyGlucoseDotNetCore.Areas.API.Controllers
{
    [Area( "API" )]
    public class ChartApiController : Controller
    {
        private IGlucoseEntryRepository _glucoseEntryRepo;
        private IExerciseEntryRepository _excerciseEntryRepo;
        private IMealEntryRepository _mealEntryRepo;
        private readonly ILogger _logger;


        public ChartApiController( IGlucoseEntryRepository glucoseEntriesRepository,
                                  IExerciseEntryRepository excerciseEntryRepo,
                                  IMealEntryRepository mealEntryRepoository,
                                  ILogger<ChartApiController> logger )
        {
            _glucoseEntryRepo = glucoseEntriesRepository;
            _excerciseEntryRepo = excerciseEntryRepo;
            _mealEntryRepo = mealEntryRepoository;
            _logger = logger;

        } // constructor


        public JsonResult GetUserExerciseChart( string UserName, DateTime? fromDate = null, DateTime? toDate = null )
        {
            IQueryable<ChartExerciseViewModel> data = GetExerciseEntries( UserName, fromDate, toDate );

            return new JsonResult( new { exerciseEntries = data } );

        } // GetUserExerciseChart


        public JsonResult GetUserStepChart( string UserName, DateTime? fromDate = null, DateTime? toDate = null )
        {
            IQueryable<ChartStepViewModel> data = GetStepEntries( UserName, fromDate, toDate );

            return new JsonResult( new { stepEntries = data } );

        } // GetUserExerciseChart


        public JsonResult GetGlucoseChart()
        {
            var data = _glucoseEntryRepo
                .ReadAll()
                .OrderBy(o => o.UpdatedAt);
            return new JsonResult( new { glucoseEntries = data } );

        } // GetGlucoseChart


        public JsonResult GetUserGlucoseChart( string UserName, DateTime? fromDate = null, DateTime? toDate = null )
        {
            IQueryable<ChartGlucoseViewModel> data = GetGlucoseEntries( UserName, fromDate, toDate );

            return new JsonResult( new { glucoseEntries = data } );

        } // GetGlucoseChart


        public JsonResult GetUserMealChart( string UserName, DateTime? fromDate = null, DateTime? toDate = null )
        {
            IQueryable<ChartMealViewModel> data = GetMealEntries( UserName, fromDate, toDate );

            return new JsonResult( new { mealEntries = data } );

        } // GetMealChart


        #region ------------------------------ HELPER METHODS ------------------------------

        private IQueryable<ChartExerciseViewModel> GetExerciseEntries( string UserName, DateTime? fromDate, DateTime? toDate )
        {
            var data = from e in _excerciseEntryRepo.ReadAll()
                       where e.UserName == UserName && e.Minutes > 0
                       orderby e.UpdatedAt
                       group e by e.UpdatedAt.ToString("d") into grp
                       select new ChartExerciseViewModel
                       {
                           Minutes = grp.Sum( s => s.Minutes ),
                           UpdatedAt = grp.Key,
                           Date = DateTime.Parse( grp.Key )
                           //Date = ((List<DateTime>)(from g in grp
                           //                         select g.UpdatedAt))[0]
                       };

            //_logger.LogDebug( "*******" + data.Count() + "*******" );

            if( fromDate != null )
                data = data.Where( d => d.Date >= fromDate );
            if( toDate != null )
            {
                var addADay = (DateTime) toDate;
                addADay = addADay.AddHours( 23 ).AddMinutes( 59 );
                data = data.Where( d => d.Date <= addADay );
            }

            return data;

        } // GetExerciseEntries


        private IQueryable<ChartMealViewModel> GetMealEntries( string UserName, DateTime? fromDate, DateTime? toDate )
        {
            var data = from e in _mealEntryRepo.ReadAll()
                       where e.UserName == UserName && e.TotalCarbs > 0
                       orderby e.UpdatedAt
                       group e by e.UpdatedAt.ToString("d") into grp
                       select new ChartMealViewModel
                       {
                           TotalCarbs = grp.Sum( s => s.TotalCarbs ),
                           UpdatedAt = grp.Key,
                           Date = DateTime.Parse( grp.Key )
                           //Date = ((List<DateTime>)(from g in grp
                           //                         select g.UpdatedAt))[0]
                       };

            //_logger.LogDebug( "*******" + data.Count() + "*******" );

            if( fromDate != null )
                data = data.Where( d => d.Date >= fromDate );
            if( toDate != null )
            {
                var addADay = (DateTime) toDate;
                addADay = addADay.AddHours( 23 ).AddMinutes( 59 );
                data = data.Where( d => d.Date <= addADay );
            }

            return data;

        } // GetMealEntries


        private IQueryable<ChartGlucoseViewModel> GetGlucoseEntries( string UserName, DateTime? fromDate, DateTime? toDate )
        {
            var data = from e in _glucoseEntryRepo.ReadAll()
                       where e.UserName == UserName && e.Measurement > 0
                       orderby e.UpdatedAt
                       group e by e.UpdatedAt.ToString("d") into grp
                       select new ChartGlucoseViewModel
                       {
                           Measurement = grp.Average( s => s.Measurement ),
                           UpdatedAt = grp.Key,
                           Date = DateTime.Parse( grp.Key )
                           //Date = ((List<DateTime>)(from g in grp
                           //                         select g.UpdatedAt))[0]
                       };

            //_logger.LogDebug( "*******" + data.Count() + "*******" );

            if( fromDate != null )
                data = data.Where( d => d.Date >= fromDate );
            if( toDate != null )
            {
                var addADay = (DateTime) toDate;
                addADay = addADay.AddHours( 23 ).AddMinutes( 59 );
                data = data.Where( d => d.Date <= addADay );
            }

            return data;

        } // GetGlucoseEntries


        private IQueryable<ChartStepViewModel> GetStepEntries( string UserName, DateTime? fromDate, DateTime? toDate )
        {
            var data = from e in _excerciseEntryRepo.ReadAll()
                       where e.UserName == UserName && e.Steps > 0
                       orderby e.UpdatedAt
                       group e by e.UpdatedAt.ToString("d") into grp
                       select new ChartStepViewModel
                       {
                           Steps = grp.Sum( s => s.Steps ),
                           UpdatedAt = grp.Key,
                           Date = DateTime.Parse( grp.Key )
                           //Date = ((List<DateTime>)(from g in grp
                           //                         select g.UpdatedAt))[0]
                       };

            //_logger.LogDebug( "*******" + data.Count() + "*******" );

            if( fromDate != null )
                data = data.Where( d => d.Date >= fromDate );
            if( toDate != null )
            {
                var addADay = (DateTime) toDate;
                addADay = addADay.AddHours( 23 ).AddMinutes( 59 );
                data = data.Where( d => d.Date <= addADay );
            }

            //_logger.LogDebug( "*******" + data1.Count() + "*******" );
            return data;

        } // GetStepEntries

        #endregion


        #region ------------------------------ OLD CODE ------------------------------

        // TODO: Not grouping by date
        //var data = _excerciseEntryRepo.ReadAll()
        //    .Where(s => s.UserName == UserName && s.Minutes > 0 )
        //    .OrderBy(o => o.UpdatedAt)
        //    .GroupBy( d => new { d.UpdatedAt, d.Minutes } )
        //    .Select( e => new ChartExerciseViewModel
        //    {
        //        UpdatedAt = e.Key.UpdatedAt.ToString("d"),
        //        Minutes = e.Key.Minutes,
        //        Date = DateTime.Parse( e.Key.UpdatedAt.ToString() )
        //    } );


        //if( fromDate != null )
        //    data = data.Where( d => d.Date >= fromDate );
        //if( toDate != null )
        //{
        //    var addADay = (DateTime) toDate;
        //    addADay = addADay.AddHours( 23 ).AddMinutes( 59 );
        //    data = data.Where( d => d.Date <= addADay );
        //}

        //var data = _glucoseEntryRepo.ReadAll()
        //    .Where(s => s.UserName == UserName && s.Measurement > 0 )
        //    .OrderBy(o => o.UpdatedAt)
        //    .GroupBy( d => new { d.UpdatedAt, d.Measurement } )
        //    .Select( e => new ChartGlucoseViewModel
        //    {
        //        UpdatedAt = e.Key.UpdatedAt.ToString("d"),
        //        Measurement = e.Key.Measurement,
        //        Date = DateTime.Parse( e.Key.UpdatedAt.ToString() )
        //    } );


        //if( fromDate != null )
        //    data = data.Where( d => d.Date >= fromDate );
        //if( toDate != null )
        //{
        //    var addADay = (DateTime) toDate;
        //    addADay = addADay.AddHours( 23 ).AddMinutes( 59 );
        //    data = data.Where( d => d.Date <= addADay );
        //}

        #endregion


    } // class

} // namespace