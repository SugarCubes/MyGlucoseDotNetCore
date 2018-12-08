using Microsoft.AspNetCore.Mvc;
using MyGlucoseDotNetCore.Models;
using MyGlucoseDotNetCore.Services.Interfaces;
using System.Threading.Tasks;

namespace MyGlucoseDotNetCore.Controllers
{
    //[Authorize( Roles = Roles.DOCTOR )]
    public class ChartController : Controller
    {
        private IApplicationUserRepository _applicationUserRepository;
        private IPatientRepository _patientRepository;

        public ChartController( IApplicationUserRepository applicationUserRepository,
            IPatientRepository patientRepository )
        {
            _applicationUserRepository = applicationUserRepository;
            _patientRepository = patientRepository;

        }

        public async Task<IActionResult> GlucoseIndex( string UserName = null )
        {
            // Redirect if unauthorized:
            if( User.Identity.Name != UserName && !User.IsInRole( Roles.DOCTOR ) )
                return RedirectToAction( "Index", "Home" );

            var patient = await _patientRepository.ReadAsync( UserName );
            ViewData[ "UserName" ] = GetUserName( patient );
            return View( patient );

        } // GlucoseIndex

        public async Task<IActionResult> ExerciseIndex( string UserName = null )
        {
            // Redirect if unauthorized:
            if( User.Identity.Name != UserName && !User.IsInRole( Roles.DOCTOR ) )
                return RedirectToAction( "Index", "Home" );

            var patient = await _patientRepository.ReadAsync( UserName );
            ViewData[ "UserName" ] = GetUserName( patient );
            return View( patient );

        } // ExerciseIndex

        public async Task<IActionResult> MealIndex( string UserName = null )
        {
            // Redirect if unauthorized:
            if( User.Identity.Name != UserName && !User.IsInRole( Roles.DOCTOR ) )
                return RedirectToAction( "Index", "Home" );

            var patient = await _patientRepository.ReadAsync( UserName );
            ViewData[ "UserName" ] = GetUserName( patient );
            return View( patient );

        } // MealIndex

        public async Task<IActionResult> StepIndex( string UserName = null )
        {
            // Redirect if unauthorized:
            if( User.Identity.Name != UserName && !User.IsInRole( Roles.DOCTOR ) )
                return RedirectToAction( "Index", "Home" );

            var patient = await _patientRepository.ReadAsync( UserName );
            ViewData[ "UserName" ] = GetUserName( patient );
            return View( patient );

        } // MealIndex


        private async Task<string> GetUserName( string UserName = null )
        {
            if( UserName != null && _applicationUserRepository.UserExists( UserName ) )
            {
                var user = await _applicationUserRepository.ReadAsync( UserName );
                return user.FirstName + " " + user.LastName;

            }
            else
                return "Patient";

        } // GetUserName


        private string GetUserName( Patient patient = null )
        {
            if( patient != null )
                return patient.FirstName + " " + patient.LastName;
            else
                return "Patient";

        } // GetUserName

    } // class

} // namespace