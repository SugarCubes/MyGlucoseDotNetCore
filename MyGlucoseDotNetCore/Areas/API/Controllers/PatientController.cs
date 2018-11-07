using Microsoft.AspNetCore.Mvc;
using MyGlucoseDotNetCore.Models;
using MyGlucoseDotNetCore.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace MyGlucoseDotNetCore.Areas.API.Controllers
{
    [Area( "API" )]
    public class PatientController : Controller
    {
        private IPatientRepository _patientRepository;
        private IMealEntryRepository _mealEntryRepository;
        private IMealItemRepository _mealItemRepository;
        private IGlucoseEntriesRepository _glucoseEntryRepository;
        private IExerciseEntryRepository _exerciseEntryRepository;

        public PatientController( IPatientRepository patientRepository,
            IMealEntryRepository mealEntryRepository,
            IMealItemRepository mealItemRepository,
            IGlucoseEntriesRepository glucoseEntriesRepository,
            IExerciseEntryRepository exerciseEntryRepository )
        {
            _patientRepository = patientRepository;
            _mealEntryRepository = mealEntryRepository;
            _mealItemRepository = mealItemRepository;
            _glucoseEntryRepository = glucoseEntriesRepository;
            _exerciseEntryRepository = exerciseEntryRepository;

        } // injection constructor


        // GET: /API/Patient/Sync
        [HttpPost]
        public async Task<ActionResult> Sync( [FromBody] Patient patient )
        {
            // Make sure there was no trouble binding all of the patient's data:
            if ( patient == null || string.IsNullOrEmpty( patient.UserName ) )
                return new JsonResult( new { success = false, errorCode = ErrorCode.UNKNOWN } );

            // If no problems, get the current data from the DB:
            Patient dbPatient = await _patientRepository.ReadAsync( patient.UserName );

            // If we have no data, or login token doesn't match, return invalid:
            bool loggedIn = patient.RemoteLoginToken == dbPatient.RemoteLoginToken;
            if ( dbPatient == null || !loggedIn )
                return new JsonResult( new { success = false, errorCode = ErrorCode.INVALID_LOGIN_TOKEN } );
            
            Console.WriteLine( "Patient appears to be valid: " + patient.FirstName );

            // Check if data needs to be updated:
            if ( dbPatient != null && dbPatient.UpdatedAt < patient.UpdatedAt )  // If database is outdated...
            {
                DateTime updatedAt = patient.UpdatedAt;
                await _patientRepository.UpdateAsync( patient.UserName, patient );

            } // if

            // Check the data sent to the server, to check if needs to be updated:
            if ( patient.GlucoseEntries != null && patient.GlucoseEntries.Count > 0 )
                await _glucoseEntryRepository.CreateOrUpdateEntries( patient.GlucoseEntries );

            if ( patient.MealEntries != null && patient.MealEntries.Count > 0 )
                await _mealEntryRepository.CreateOrUpdateEntries( patient.MealEntries );

            if ( patient.ExerciseEntries != null && patient.ExerciseEntries.Count > 0 )
                await _exerciseEntryRepository.CreateOrUpdateEntries( patient.ExerciseEntries );

            // Nullify sensitive information:
            patient.PasswordHash = "";
            patient.SecurityStamp = "";
            patient.ConcurrencyStamp = "";
            patient.Doctor = null;

            return new JsonResult( new
            {
                success = true,
                errorCode = ErrorCode.NO_ERROR,
                patient,
                patient.DoctorUserName
            } );

        } // Sync

    } // class

} // namespace