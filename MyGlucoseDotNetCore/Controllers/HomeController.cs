using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyGlucoseDotNetCore.Models;
using MyGlucoseDotNetCore.Services.Interfaces;

namespace MyGlucoseDotNetCore.Controllers
{
    public class HomeController : Controller
    {
        private IApplicationUserRepository _repo;

        public HomeController(IApplicationUserRepository repo)
        {
            _repo = repo;
        }
        public IActionResult Index()
        {
            ViewData["Message"] = "Welcome to My Glucose!";

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "About My Glucose";

            return View();
        }


        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult PatientIndex()
        {
            // TODO: Need to return patients, not ApplicationsUsers
            //      Also, need to use ToList().
            //      Needs to be tested as well
            var model = _repo.ReadAll();
            return View(model);
        }
    }
}
