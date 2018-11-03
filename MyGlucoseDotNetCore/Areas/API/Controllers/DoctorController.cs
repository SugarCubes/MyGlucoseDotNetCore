using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyGlucoseDotNetCore.Services.Interfaces;

namespace MyGlucoseDotNetCore.Areas.API.Controllers
{
    [Area("API")]
    public class DoctorController : Controller
    {
        private IDoctorRepository _doctorRepository;

        public DoctorController( IDoctorRepository doctorRepository )
        {
            _doctorRepository = doctorRepository;

        } // injection constructor


        // POST: /API/Doctor/List
        [HttpPost]
        public ActionResult List()
        {
            var doctorList = _doctorRepository.ReadAll().ToList();
            return new JsonResult( new {
                success = doctorList.Count > 0,
                ErrorCode = doctorList.Count > 0 ? ErrorCode.NO_ERROR : ErrorCode.NO_DOCTORS_REGISTERED,
                Doctors = doctorList }  );

        } // List

    } // class

} // namespace