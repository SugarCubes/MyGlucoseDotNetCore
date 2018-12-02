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
        private IApplicationUserRepository _user;
        

        public HomeController(IApplicationUserRepository user)
        {
            _user = user;
        }
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = _user.ReadUser(User.Identity.Name);
                if (user.HasRole("Doctor"))
                {
                    return RedirectToAction("Index", "Doctor");
                }
                else
                {
                    return RedirectToAction("Index", "Patient");
                }
            }
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

       
                //return RedirectToAction( "Index" );


    }
}
