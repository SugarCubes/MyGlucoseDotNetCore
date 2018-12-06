using Microsoft.AspNetCore.Mvc;
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
            if( User.Identity.Name != UserName && !User.IsInRole( Roles.DOCTOR ) )
                return RedirectToAction( "Index", "Home" );

            ViewData[ "UserName" ] = await GetUserName( UserName );

            return View();

        } // GlucoseIndex

        public async Task<IActionResult> ExerciseIndex( string UserName = null )
        {
            if( User.Identity.Name != UserName && !User.IsInRole( Roles.DOCTOR ) )
                return RedirectToAction( "Index", "Home" );

            ViewData[ "UserName" ] = await GetUserName( UserName );
            return View();

        } // ExerciseIndex

        public async Task<IActionResult> MealIndex( string UserName = null )
        {
            if( User.Identity.Name != UserName && !User.IsInRole( Roles.DOCTOR ) )
                return RedirectToAction( "Index", "Home" );

            ViewData[ "UserName" ] = await GetUserName( UserName );
            return View();

        } // MealIndex

        public async Task<IActionResult> StepIndex( string UserName = null )
        {
            if( User.Identity.Name != UserName && !User.IsInRole( Roles.DOCTOR ) )
                return RedirectToAction( "Index", "Home" );

            ViewData[ "UserName" ] = await GetUserName( UserName );
            return View();

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

    } // class

} // namespace