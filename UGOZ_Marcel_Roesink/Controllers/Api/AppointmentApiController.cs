using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UGOZ_Marcel_Roesink.Models.ViewModels;
using UGOZ_Marcel_Roesink.Services;
using UGOZ_Marcel_Roesink.Utility;

namespace UGOZ_Marcel_Roesink.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentApiController : ControllerBase
    {

        #region Fields
        private readonly IAppointmentService _appointmentService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string loginUserId;
        private readonly string role;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="AppointmentApiController"/> class.
        /// </summary>
        /// <param name="appointmentService">The appointment service.</param>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        public AppointmentApiController(IAppointmentService appointmentService,
            IHttpContextAccessor httpContextAccessor)
        {
            _appointmentService = appointmentService;
            _httpContextAccessor = httpContextAccessor;
            loginUserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            role = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Saves the calendar data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveCalendarData")]
        public IActionResult SaveCalendarData(AppointmentViewModel data)
        {
            CommonResponse<int> commonResponse = new CommonResponse<int>();
            try
            {
                commonResponse.Status = _appointmentService.AddUpdate(data).Result;
                if (commonResponse.Status == 1)
                {
                    // Successfull update
                    commonResponse.Message = Helper.AppointmentUpdated;
                }
                else if (commonResponse.Status == 2)
                {
                    // Successfull addition
                    commonResponse.Message = Helper.AppointmentAdded;
                }
            }
            catch (Exception ex)
            {
                commonResponse.Message = ex.Message;
                commonResponse.Status = Helper.Failure_code;
            }
            return Ok(commonResponse);
        }

        /// <summary>
        /// Gets the calendar data.
        /// </summary>
        /// <param name="doctorId">The doctor identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCalendarData")]
        public IActionResult GetCalendarData(string doctorId)
        {
            CommonResponse<List<AppointmentViewModel>> commonResponse = new CommonResponse<List<AppointmentViewModel>>();
            try
            {
                if (role == Helper.Patient)
                {
                    commonResponse.Dataenum = _appointmentService.PatientAppointments(loginUserId);
                    commonResponse.Status = Helper.Succes_code;
                }
                else if (role == Helper.Doctor)
                {
                    commonResponse.Dataenum = _appointmentService.DoctorAppointments(loginUserId);
                    commonResponse.Status = Helper.Succes_code;
                }
                else
                {
                    commonResponse.Dataenum = _appointmentService.DoctorAppointments(doctorId);
                    commonResponse.Status = Helper.Succes_code;
                }
            }
            catch (Exception ex)
            {
                commonResponse.Message = ex.Message;
                commonResponse.Status = Helper.Failure_code;
            }
            return Ok(commonResponse);
        }

        /// <summary>
        /// Gets the calendar data by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCalendarDataById/{id}")]
        public IActionResult GetCalendarDataById(int id)
        {
            CommonResponse<AppointmentViewModel> commonResponse = new CommonResponse<AppointmentViewModel>();
            try
            {
                commonResponse.Dataenum = _appointmentService.GetById(id);
                commonResponse.Status = Helper.Succes_code;
            }
            catch (Exception ex)
            {
                commonResponse.Message = ex.Message;
                commonResponse.Status = Helper.Failure_code;
            }
            return Ok(commonResponse);
        }

        [HttpGet]
        [Route("ConfirmAppointment/{id}")]
        public IActionResult ConfirmAppointment(int id)
        {
            CommonResponse<AppointmentViewModel> commonResponse = new CommonResponse<AppointmentViewModel>();
            try
            {
                var result = _appointmentService.ConfirmAppointment(id).Result;
                if (result > 0)
                {
                    commonResponse.Status = Helper.Succes_code;
                    commonResponse.Message = Helper.AppointmentConfirmed;
                }
                else
                {
                    commonResponse.Status = Helper.Failure_code;
                    commonResponse.Message = Helper.AppointmentConfirmError;

                }
            }
            catch (Exception ex)
            {
                commonResponse.Message = ex.Message;
                commonResponse.Status = Helper.Failure_code;
            }
            return Ok(commonResponse);
        }

        [HttpGet]
        [Route("DeleteAppointment/{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            CommonResponse<AppointmentViewModel> commonResponse = new CommonResponse<AppointmentViewModel>();
            try
            {
                commonResponse.Status = await _appointmentService.DeleteAppointment(id);
                commonResponse.Message = commonResponse.Status == 1 ? Helper.AppointmentDeleted : Helper.SomethingWentWrong;

            }
            catch (Exception ex)
            {
                commonResponse.Message = ex.Message;
                commonResponse.Status = Helper.Failure_code;
            }
            return Ok(commonResponse);
        }
        #endregion

    }
}
