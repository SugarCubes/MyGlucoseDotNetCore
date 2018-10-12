using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyGlucoseDotNetCore.Models;
using MyGlucoseDotNetCore.Models.AccountViewModels;
using MyGlucoseDotNetCore.Services;
using MyGlucoseDotNetCore.Services.Interfaces;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MyGlucoseDotNetCore.Areas.API.Controllers
{
    [Authorize]
    [Area( "API" )]
    public class AccountApiController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private IApplicationUserRepository _users;

        [TempData]
        public string ErrorMessage { get; set; }


        public AccountApiController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ILogger<AccountApiController> logger,
            IApplicationUserRepository users )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
            _users = users;
        }


        /// <summary>Allows a patient to login from a remote device.</summary>
        /// <param name="Email">The patient's email.</param>
        /// <param name="Password">The patient's password.</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Login( string Email, string Password )
        {
            if ( ModelState.IsValid )
            {
                // result is a boolean value: whether or not the signin was successful:
                var result = await _signInManager.PasswordSignInAsync( Email, Password, true, lockoutOnFailure: false);
                if ( result.Succeeded )                                     // Login was successful
                {
                    ApplicationUser user = await _users.ReadAsync( Email ); // Read user from the repository
                    _logger.LogInformation( "User logged in remotely." );   // Probably not needed

                    user.RemoteLoginToken = Guid.NewGuid();                 // Create a login token, similar to a "session id"
                    // Not tested: Probably won't be used to force user to login again until much later in development:
                    user.RemoteLoginExpiration = (long) DateTime.UtcNow.Subtract( new DateTime( 1970, 1, 1 ).AddDays( 30 ) ).TotalSeconds;
                    await _users.UpdateAsync( user.UserName, user );        // Update the user with the repo
                    var res = new JsonResult(                               // This implements IActionResult. If you were 
                        new                                                 //      to inspect the output, you would see a 
                        {                                                   //      Json-formatted string.
                            success = true,
                            errorCode = ErrorCode.NO_ERROR,
                            user.UserName,
                            remoteLoginToken = user.RemoteLoginToken.ToString(),
                            user.RemoteLoginExpiration,
                            user.Address1,
                            user.Address2,
                            user.City,
                            user.Email,
                            user.FirstName,
                            user.Id,
                            user.LastName,
                            user.PhoneNumber,
                            user.State,
                            user.Zip1,
                            user.Zip2
                        }
                        );
                    Debug.WriteLine( res.ToString() );
                    return res;

                }
                else                                                            // Login didn't work
                {
                    var resFail = new JsonResult(                                      // We still return a JsonResult
                            new
                            {
                                success = false,                                // Indicate the login was unsuccessful
                                errorCode = ErrorCode.INVALID_EMAIL_PASSWORD    //  ...and return an error code
                            }
                        );
                    Debug.WriteLine( resFail.ToString() );
                    return resFail;

                } // if succeeeded...else

            } // ModelState == valid

            // If we got this far, something failed, so just return an unknown error
            return new JsonResult(
                    new
                    {
                        success = false,
                        errorCode = ErrorCode.UNKNOWN
                    }
                );

        } // LoginRemote


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register( RegisterViewModel model, string returnUrl = null )
        {
            if ( ModelState.IsValid )
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if ( result.Succeeded )
                {
                    _logger.LogInformation( "User created a new account with password." );

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                    await _emailSender.SendEmailConfirmationAsync( model.Email, callbackUrl );

                    await _signInManager.SignInAsync( user, isPersistent: false );
                    _logger.LogInformation( "User created a new account with password." );


                    return new JsonResult( new { } );
                }

            }

            // If we got this far, something failed, redisplay form
            return new JsonResult( new { } );
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation( "User logged out." );
            //return RedirectToAction( nameof( HomeController.Index ), "Home" );
            return new JsonResult( new { } );
        }

    } // class

} // namespace
