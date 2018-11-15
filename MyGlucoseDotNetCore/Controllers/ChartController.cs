using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MyGlucoseDotNetCore.Controllers
{
    public class ChartController : Controller
    {
        public IActionResult GlucoseIndex()
        {
            return View();

        }

        public IActionResult ExerciseIndex()
        {
            return View();

        }

        public IActionResult MealIndex()
        {
            return View();

        }

    } // class
    
} // namespace