using Microsoft.AspNetCore.Mvc;
using MyGlucoseDotNetCore.Models;
using MyGlucoseDotNetCore.Services.Interfaces;
using System.Diagnostics;

namespace MyGlucoseDotNetCore.Controllers
{
    public class HomeController : Controller
    {
        private IApplicationUserRepository _user;


        public HomeController( IApplicationUserRepository user )
        {
            _user = user;

        } // constructor


        public IActionResult Index()
        {
            if( User.Identity.IsAuthenticated )
            {
                //var user = _user.ReadUser(User.);

                if( User.IsInRole( Roles.DOCTOR ) )
                {
                    return RedirectToAction( nameof( Index ), "Doctor" );
                }
                else if( User.IsInRole( Roles.PATIENT ) )
                {
                    return RedirectToAction( nameof( Index ), "Patient" );
                }
            }

            ViewData[ "Message" ] = "Welcome to My Glucose!";

            return View();

        } // Index


        public IActionResult About()
        {
            ViewData[ "Message" ] = "About My Glucose";

            return View();

        } // About


        public IActionResult Contact()
        {
            ViewData[ "Message" ] = "Your contact page.";

            return View();

        } // Contact


        public IActionResult Error()
        {
            return View( new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier } );

        } // Error

    } // class

} // namespace
