using Microsoft.AspNetCore.Mvc;
using MyGlucoseDotNetCore.Models;
using MyGlucoseDotNetCore.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
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
            //Patient patient = JsonConvert.DeserializeObject<Patient>( patientString );
            //Console.WriteLine( patient );

            if ( patient == null || string.IsNullOrEmpty( patient.UserName ) )
                return new JsonResult( new { success = false, errorCode = ErrorCode.UNKNOWN } );

            Patient dbPatient = await _patientRepository.ReadAsync( patient.UserName );

            if ( dbPatient != null && patient.RemoteLoginToken != dbPatient.RemoteLoginToken )
                return new JsonResult( new { success = false, errorCode = ErrorCode.INVALID_LOGIN_TOKEN } );

            Console.WriteLine( "Patient appears to be valid: " + patient.FirstName );

            if ( dbPatient != null && dbPatient.UpdatedAt < patient.UpdatedAt )  // If database is outdated...
            {
                DateTime updatedAt = patient.UpdatedAt;

            } // 

            // Sync the information sent to us
            if ( patient.GlucoseEntries != null && patient.GlucoseEntries.Count > 0 )
                await _glucoseEntryRepository.CreateOrUpdateEntries( patient.GlucoseEntries );

            if ( patient.MealEntries != null && patient.MealEntries.Count > 0 )
                await _mealEntryRepository.CreateOrUpdateEntries( patient.MealEntries );

            if ( patient.ExerciseEntries != null && patient.ExerciseEntries.Count > 0 )
                await _exerciseEntryRepository.CreateOrUpdateEntries( patient.ExerciseEntries );

            return new JsonResult( new
            {
                success = true,
                errorCode = ErrorCode.NO_ERROR,
                patient
            } );

        } // Sync

    } // class

} // namespace