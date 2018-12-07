using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyGlucoseDotNetCore.Models;
using MyGlucoseDotNetCore.Models.ViewModels;
using MyGlucoseDotNetCore.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace MyGlucoseDotNetCore.Controllers
{
    [Authorize( Roles = Roles.PATIENT + ", " + Roles.DOCTOR )]
    public class PatientController : Controller
    {
        private IPatientRepository _pat;
        private IDoctorRepository _doc;

        public PatientController( IPatientRepository pat,
                                 IDoctorRepository doc )
        {
            _pat = pat;
            _doc = doc;
        }

        public async Task<IActionResult> Index()
        {
            //var doctors = _doc.ReadAll();
            return View( await _pat.ReadAsync( User.Identity.Name ) );
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
            return View( model );
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create( Patient patient )
        {
            if( ModelState.IsValid )
            {
                await _pat.CreateAsync( patient );
                return RedirectToAction( "Index" );
            }
            return View( patient );

        } // Create


        //[Authorize( Roles = Roles.DOCTOR + ", " + Roles.PATIENT )]
        public async Task<IActionResult> Details( Patient patient )
        {
            var pat = await _pat.ReadAsync( patient.UserName );

            if( User.Identity.Name != pat.UserName && User.Identity.Name != pat.Doctor.UserName )
                return new UnauthorizedResult();
            return View( await _pat.ReadAsync( patient.UserName ) );

        } // Details


    } // class

} // namespace