using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyGlucoseDotNetCore.Services.Interfaces;

namespace MyGlucoseDotNetCore.Areas.API.Controllers
{
    [Area("API")]
    public class ChartApiController : Controller
    {
        private IGlucoseEntriesRepository _gEntry;

        public IActionResult Index()
        {
            return View();
        }

        public ChartApiController(IGlucoseEntriesRepository gEntry)
        {
            _gEntry = gEntry;

        } // constructor

        public JsonResult GetGlucoseChart()
        {
            // TODO: Create ReadAll(Username)
            var data = _gEntry
                .ReadAll()
                .OrderBy(o => o.UpdatedAt);
            return new JsonResult(new { glucoseEntries = data });

        } // GetGlucoseChart

        public JsonResult GetUserGlucoseChart(string UserName)
        {
            // TODO: Create ReadAll(Username)
            var data = _gEntry
                .ReadAll()
                .Where(o => o.UserName == UserName)
                .OrderBy(o => o.UpdatedAt);
            return new JsonResult(new { glucoseEntries = data });

        } // GetGlucoseChart
    } // end chart controller
}// end namespace