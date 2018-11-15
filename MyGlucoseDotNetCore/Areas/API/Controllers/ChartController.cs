using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyGlucoseDotNetCore.Services.Interfaces;

namespace MyGlucoseDotNetCore.Controllers
{
    public class ChartController : Controller
    {
        // Note: You don't have to pass a model. ALL of the data is handled by jQuery, so
        //      when you call /Chart/GlucoseIndex?UserName=example@example.com, jQuery does
        //      an Ajax call to /API/ChartApi/GetGlucoseChart?UserName=example@example.com, 
        //      simply getting the UserName parameter from the URL.
        public IActionResult GlucoseIndex()
        {
            return View();
        }

        public IActionResult ExerciseIndex()
        {
            return View();
        }
    } // class

} // namespace