using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyGlucoseDotNetCore.Models;
using MyGlucoseDotNetCore.Models.ViewModels;
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

        public IActionResult CreatePatient()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePatient( PatientViewModel patientVM )
        {
            if (ModelState.IsValid)
            {
                await _repo.CreatePatientAsync( patientVM.GetNewPatient() );
                //return RedirectToAction( "Index" );
            }
            return View( patientVM );
        }

        public IActionResult PatientIndex()
        {
            var model = _repo.ReadAll()
            .Select(p => new PatientViewModel
            {
                UserName = p.UserName,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Address1 = p.Address1,
                Address2 = p.Address2,
                City = p.City,
                State = p.State,
                Zip1 = p.Zip1,
                Zip2 = p.Zip2,
                PhoneNumber = p.PhoneNumber,
                Email = p.Email
            });
            return View(model);
        }
    }
}
