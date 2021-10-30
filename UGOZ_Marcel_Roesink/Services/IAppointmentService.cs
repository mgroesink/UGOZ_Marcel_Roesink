using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UGOZ_Marcel_Roesink.Models.ViewModels;

namespace UGOZ_Marcel_Roesink.Services
{
    public interface IAppointmentService
    {

        #region Methods
        /// <summary>
        /// Gets the doctor list.
        /// </summary>
        /// <returns></returns>
        public List<DoctorViewModel> GetDoctorList();
        /// <summary>
        /// Gets the patient list.
        /// </summary>
        /// <returns></returns>
        public List<PatientViewModel> GetPatientList();
        /// <summary>
        /// Adds the update.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public Task<int> AddUpdate(AppointmentViewModel model);
        /// <summary>
        /// Doctors the appointments.
        /// </summary>
        /// <param name="doctorid">The doctorid.</param>
        /// <returns></returns>
        public List<AppointmentViewModel> DoctorAppointments(string doctorid);
        /// <summary>
        /// Patients the appointments.
        /// </summary>
        /// <param name="patientid">The patientid.</param>
        /// <returns></returns>
        public List<AppointmentViewModel> PatientAppointments(string patientid);
        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public AppointmentViewModel GetById(int id);

        public Task<int> DeleteAppointment(int id);

        public Task<int> ConfirmAppointment(int id);

        #endregion

    }
}


