using Microsoft.AspNetCore.Mvc;
using MyGlucoseDotNetCore.Services.Interfaces;
using System.Linq;

namespace MyGlucoseDotNetCore.Areas.API.Controllers
{
    [Area("API")]
    public class ChartApiController : Controller
    {
        private IGlucoseEntriesRepository _glucoseEntryRepo;
        private IExerciseEntryRepository _excerciseEntryRepo;

        public ChartApiController(IGlucoseEntriesRepository glucoseEntriesRepository,
                                  IExerciseEntryRepository excerciseEntryRepo)
        {
            _glucoseEntryRepo = glucoseEntriesRepository;
            _excerciseEntryRepo = excerciseEntryRepo;

        } // constructor

        public JsonResult GetUserExerciseChart(string UserName)
        {
            // TODO: Create ReadAll(Username)
            var data = _excerciseEntryRepo
                .ReadAll()
                .Where(e => e.UserName == UserName)
                .OrderBy(e => e.UpdatedAt);
            return new JsonResult(new { exerciseEntries = data });

        } // GetGlucoseChart

        public JsonResult GetGlucoseChart()
        {
            // TODO: Create ReadAll(Username)
            var data = _glucoseEntryRepo
                .ReadAll()
                .OrderBy(o => o.UpdatedAt);
            return new JsonResult(new { glucoseEntries = data });

        } // GetGlucoseChart


        public JsonResult GetUserGlucoseChart(string UserName)
        {
            // TODO: Create ReadAll(Username)
            var data = _glucoseEntryRepo
                .ReadAll()
                .Where(o => o.UserName == UserName)
                .OrderBy(o => o.UpdatedAt);
            return new JsonResult(new { glucoseEntries = data });

        } // GetGlucoseChart

    } // class

} // namespace