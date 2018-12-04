using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyGlucoseDotNetCore.Models.ViewModels;
using MyGlucoseDotNetCore.Services.Interfaces;

namespace MyGlucoseDotNetCore.Controllers
{
    public class DoctorController : Controller
    {
        private IPatientRepository _pat;

        public DoctorController(IPatientRepository pat)
        {
            _pat = pat;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PatientNames()
        {
            var model = _pat.ReadAll()
            .Select(p => new PatientViewModel
            {
                FirstName = p.FirstName,
                LastName = p.LastName,
                PhoneNumber = p.PhoneNumber
            });
            return View(model);
        }
 

        //[HttpPost]
        public IActionResult PatientDetails()
        {
            
            //var patient = _pat.ReadAsync(userName);

            var model = _pat.ReadAll()
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