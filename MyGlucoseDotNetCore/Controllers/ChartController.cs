using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MyGlucoseDotNetCore.Controllers
{
    public class ChartController : Controller
    {
        public ChartController()// IGlucoseEntriesRepository glucoseEntryRepository )
        {
            //_glucoseEntryRepo = glucoseEntryRepository;

        }
        public IActionResult GlucoseIndex()
        {
            return View();
        }
    }
}