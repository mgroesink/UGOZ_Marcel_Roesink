using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UGOZ_Marcel_Roesink.Services;
using UGOZ_Marcel_Roesink.Utility;

namespace UGOZ_Marcel_Roesink.Controllers
{
    public class AppointmentController : Controller
    {

        #region Fields
        private readonly IAppointmentService _appointmentService; 
        #endregion

        #region Constructors
        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        } 
        #endregion

        #region Methods
        public IActionResult Index()
        {
            ViewBag.DoctorList = _appointmentService.GetDoctorList();
            ViewBag.PatientList = _appointmentService.GetPatientList();
            ViewBag.Duration = Helper.GetTimeDropDown(90, 10);

            return View();

        } 
        #endregion

    }
}
