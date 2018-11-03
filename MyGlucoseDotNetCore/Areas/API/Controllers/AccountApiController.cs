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
        private IApplicationUserRepository _userRepository;
        IPatientRepository _patientRepository;
        IDoctorRepository _doctorRepository;

        [TempData]
        public string ErrorMessage { get; set; }


        public AccountApiController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ILogger<AccountApiController> logger,
            IApplicationUserRepository users,
            IPatientRepository patientRepository,
            IDoctorRepository doctorRepository )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
            _userRepository = users;
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;

        } // constructor


        /// <summary>Allows a patient to login from a remote device.</summary>
        /// <param name="Email">The patient's email.</param>
        /// <param name="Password">The patient's password.</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Login( string Email, string Password )
        {
            // result is a boolean value: whether or not the signin was successful:
            var result = await _signInManager.PasswordSignInAsync( Email, Password, true, lockoutOnFailure: false);
            if ( result.Succeeded )                                     // Login was successful
            {
                Patient patient = await _patientRepository.ReadAsync( Email ); // Read user from the repository

                patient.RemoteLoginToken = Guid.NewGuid();              // Create a login token, similar to a "session id"
                                                                        // Not tested: Probably won't be used to force user to login again until much later in development:
                patient.RemoteLoginExpiration = (long) DateTime.UtcNow.Subtract( new DateTime( 1970, 1, 1 ).AddDays( 30 ) ).TotalSeconds;
                await _patientRepository.UpdateAsync( patient.UserName, patient );        // Update the user with the repo
                var res = new JsonResult(                               // This implements IActionResult. If you were 
                        new                                             //      to inspect the output, you would see a 
                        {                                               //      Json-formatted string.
                            success = true,
                            errorCode = ErrorCode.NO_ERROR,
                            patient.UserName,
                            remoteLoginToken = patient.RemoteLoginToken.ToString(),
                            patient.RemoteLoginExpiration,
                            patient.Address1,
                            patient.Address2,
                            patient.City,
                            patient.Email,
                            patient.FirstName,
                            patient.Id,
                            patient.LastName,
                            patient.PhoneNumber,
                            patient.State,
                            patient.Zip1,
                            patient.Zip2,
                            patient.DoctorUserName,
                            patient.DoctorId
                        }
                        );
                Debug.WriteLine( res.ToString() );
                return res;

            }
            else                                                                // Login didn't work
            {
                var resFail = new JsonResult(                                   // We still return a JsonResult
                            new
                            {
                                success = false,                                // Indicate the login was unsuccessful
                                errorCode = ErrorCode.INVALID_EMAIL_PASSWORD    //  ...and return an error code
                            }
                        );
                Debug.WriteLine( resFail.ToString() );
                return resFail;

            } // if succeeeded...else

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
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Register( RegisterViewModel model )
        {
            var doctor = await _doctorRepository.ReadAsync( model.DoctorUserName );
            var patient = model.GetNewPatient();
            patient.Doctor = doctor;

            var result = await _userManager.CreateAsync(patient, model.Password);

            if ( result.Succeeded )
            {
                _logger.LogInformation( "User created a new account with password." );

                patient.CreatedAt = DateTime.Now;
                patient.UpdatedAt = patient.CreatedAt;
                //patient = await _patientRepository.ReadAsync( model.Email );
                //patient.Doctor = doctor;
                //patient.DoctorUserName = model.DoctorUserName;
                await _patientRepository.UpdateAsync( model.Email, patient );

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(patient);
                var callbackUrl = Url.EmailConfirmationLink(patient.Id, code, Request.Scheme);
                await _emailSender.SendEmailConfirmationAsync( model.Email, callbackUrl );

                //await _signInManager.SignInAsync( patient, isPersistent: false );
                _logger.LogInformation( "User created a new account with password." );

                return await Login( model.Email, model.Password );// new JsonResult( new { } ); //
            }

            // Else, if registering didn't succeed...
            patient = await _patientRepository.ReadAsync( model.Email );
            if ( patient != null && !String.IsNullOrEmpty( patient.RemoteLoginToken.ToString() ) )
                return new JsonResult( new
                {
                    success = true,
                    errorCode = ErrorCode.USER_ALREADY_LOGGED_IN,
                    patient.RemoteLoginToken,
                    patient.RemoteLoginExpiration,
                    patient.DoctorUserName,
                    doctorId = patient.Doctor != null ? patient.Doctor.Id : ""
                } );
            

            return await Login( model.Email, model.Password );//new JsonResult( new { success = false, errorCode = ErrorCode.USER_ALREADY_REGISTERED } );

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
