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
        private IApplicationUserRepository _applicationUserRepository;

        public ChartController( IApplicationUserRepository applicationUserRepository )
        {
            _applicationUserRepository = applicationUserRepository;

        }

        public async Task<IActionResult> GlucoseIndex( string UserName = null )
        {
            ViewData[ "UserName" ] = await GetUserName( UserName );

            return View();

        } // GlucoseIndex

        public async Task<IActionResult> ExerciseIndex( string UserName = null )
        {
            ViewData[ "UserName" ] = await GetUserName( UserName );
            return View();

        } // ExerciseIndex

        public async Task<IActionResult> MealIndex( string UserName = null )
        {
            ViewData[ "UserName" ] = await GetUserName( UserName );
            return View();

        } // MealIndex

        public async Task<IActionResult> StepIndex( string UserName = null )
        {
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