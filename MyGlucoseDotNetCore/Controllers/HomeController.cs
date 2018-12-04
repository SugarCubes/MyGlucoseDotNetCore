using Microsoft.AspNetCore.Mvc;
using MyGlucoseDotNetCore.Models;
using MyGlucoseDotNetCore.Services.Interfaces;
using System.Diagnostics;

namespace MyGlucoseDotNetCore.Controllers
{
    public class HomeController : Controller
    {
        private IApplicationUserRepository _user;
        private IPatientRepository _pat;
        private IDoctorRepository _doc;


        public HomeController( IApplicationUserRepository user,
                               IPatientRepository pat,
                               IDoctorRepository doc)
        {
            _user = user;
            _pat = pat;
            _doc = doc;
        }


        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {

                if (User.IsInRole(Roles.DOCTOR))
                {
                    var doctor = _doc.ReadDoctor(User.Identity.Name);
                    if (!_doc.Exists(doctor.FirstName))
                    {
                        //return RedirectToAction("Create", "Doctor");
                        return RedirectToAction("Index", "Manage");

                    }
                    return RedirectToAction(nameof(Index), "Doctor");
                }
                else if (User.IsInRole(Roles.PATIENT))
                {
                    var patient = _pat.ReadPatient(User.Identity.Name);
                    if (!_pat.Exists(patient.FirstName))
                    {
                        //return RedirectToAction("Create", "Patient");
                        return RedirectToAction("Index", "Manage");

                    }
                    return RedirectToAction(nameof(Index), "Patient");
                }
                
            }
            ViewData[ "Message" ] = "Welcome to My Glucose!";

            return View();
        }

        public IActionResult About()
        {
            ViewData[ "Message" ] = "About My Glucose";

            return View();
        }


        public IActionResult Contact()
        {
            ViewData[ "Message" ] = "Your contact page.";

            return View();
        }
        public IActionResult Error()
        {
            return View( new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier } );
        }


        //return RedirectToAction( "Index" );


    }
}
