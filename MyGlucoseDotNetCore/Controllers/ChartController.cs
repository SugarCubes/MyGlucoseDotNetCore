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
        //private IGlucoseEntriesRepository _glucoseEntryRepo;

        public ChartController()// IGlucoseEntriesRepository glucoseEntryRepository )
        {
            //_glucoseEntryRepo = glucoseEntryRepository;

        } // constructor


        // Note: You don't have to pass a model. ALL of the data is handled by jQuery, so
        //      when you call /Chart/GlucoseIndex?UserName=example@example.com, jQuery does
        //      an Ajax call to /API/ChartApi/GetGlucoseChart?UserName=example@example.com, 
        //      simply getting the UserName parameter from the URL.
        public IActionResult GlucoseIndex()
        {
            //var entries = _glucoseEntryRepo.ReadAll();
            return View();
        }

    } // class

} // namespace