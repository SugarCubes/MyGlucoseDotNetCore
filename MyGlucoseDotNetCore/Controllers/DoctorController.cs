using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyGlucoseDotNetCore.Models.ViewModels;
using MyGlucoseDotNetCore.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace MyGlucoseDotNetCore.Controllers
{
    [Authorize( Roles = Roles.DOCTOR )]
    public class DoctorController : Controller
    {
        private IDoctorRepository _doctorRepository;
        private IPatientRepository _patientRepository;

        public DoctorController( IPatientRepository patientRepository,
            IDoctorRepository doctorRepository )
        {
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
        }

        public async Task<IActionResult> Index()
        {
            var doctor = await _doctorRepository.ReadAsync( User.Identity.Name );
            doctor.Patients = doctor.Patients
                .OrderBy( l => l.LastName )
                .ThenBy( f => f.FirstName )
                .ToList();
            return View( doctor );

        } // Index

        public IActionResult PatientNames()
        {
            var model = _patientRepository.ReadAll()
                .OrderBy( l => l.LastName )
                .ThenBy( f => f.FirstName )
            .Select(p => new PatientViewModel
            {
                FirstName = p.FirstName,
                LastName = p.LastName,
                PhoneNumber = p.PhoneNumber
            });
            return View( model );

        } // PatientNames


        //[HttpPost]
        public IActionResult PatientDetails()
        {

            //var patient = _pat.ReadAsync(userName);

            var model = _patientRepository.ReadAll()
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

        } // PatientDetails

    } // class

} // namespace