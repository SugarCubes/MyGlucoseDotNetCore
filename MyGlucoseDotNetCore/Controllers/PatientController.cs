using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyGlucoseDotNetCore.Models.ViewModels;
using MyGlucoseDotNetCore.Services.Interfaces;

namespace MyGlucoseDotNetCore.Controllers
{
    public class PatientController : Controller
    {
        private IPatientRepository _pat;
        private IDoctorRepository _doc;

        public PatientController(IPatientRepository pat,
                                 IDoctorRepository doc)
        {
            _pat = pat;
            _doc = doc;
        }

        public IActionResult Index()
        {
            var doctors = _doc.ReadAll();
            return View(doctors);
        }

        public IActionResult PatientList()
        {
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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PatientViewModel patientVM)
        {
            if (ModelState.IsValid)
            {
                await _pat.CreateAsync(patientVM.GetNewPatient());
                return RedirectToAction("Index");
            }
            return View(patientVM);
        }


    }
}