using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using UGOZ_Marcel_Roesink.Models;
using UGOZ_Marcel_Roesink.Models.ViewModels;
using UGOZ_Marcel_Roesink.Utility;

namespace UGOZ_Marcel_Roesink.Services
{
    public class AppointmentService : IAppointmentService
    {
        #region Fields
        private readonly ApplicationDbContext _db;
        private readonly IEmailSender _emailSender;

        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="AppointmentService"/> class.
        /// </summary>
        /// <param name="db">The database.</param>
        public AppointmentService(ApplicationDbContext db, IEmailSender emailSender)
        {
            _db = db;
            _emailSender = emailSender;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Adds or updates an appointment.
        /// </summary>
        /// <param name="model">The appointment data.</param>
        /// <returns></returns>
        public async Task<int> AddUpdate(AppointmentViewModel model)
        {
            // Let op het gebruik van een specifieke culture om de datum string te converteren naar een DateTime object
            var startDate = DateTime.Parse(model.StartDate, CultureInfo.CreateSpecificCulture("nl-NL"));
            var endDate = startDate.AddMinutes(Convert.ToDouble(model.Duration));
            var patient = _db.Users.FirstOrDefault(u => u.Id == model.PatientId);
            var doctor = _db.Users.FirstOrDefault(u => u.Id == model.DoctorId);
            if (model != null && model.Id > 0)
            {
                //TODO: Add code to update existing appointment
                var appointment = _db.Appointments.FirstOrDefault(a => a.Id == model.Id);
                appointment.Title = model.Title;
                appointment.Description = model.Description;
                appointment.StartDate = startDate;
                appointment.EndDate = endDate;
                appointment.Duration = model.Duration;
                appointment.DoctorId = model.DoctorId;
                appointment.PatientId = model.PatientId;
                appointment.IsDoctorApproved = false;
                appointment.AdminId = model.AdminId;
                await _db.SaveChangesAsync();
                return 1;
            }
            else
            {
                // Create appointment based on view model
                // Use object initialisation. For details:
                // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/object-and-collection-initializers
                Appointment appointment = new Appointment()
                {
                    Title = model.Title,
                    Description = model.Description,
                    StartDate = startDate,
                    EndDate = endDate,
                    Duration = model.Duration,
                    DoctorId = model.DoctorId,
                    PatientId = model.PatientId,
                    IsDoctorApproved = model.IsDoctorApproved,
                    AdminId = model.AdminId
                };
                await _emailSender.SendEmailAsync("mroesink@rocvantwente.nl", "Greetings from Mailjet.",
                    $"Er is een afspraak voor U ingepland met {patient.FullName}. Deze moet door U nog bevestigd worden.");
                await _emailSender.SendEmailAsync("mg.roesink@gmail.com", "Afspraak ingepland",
                    $"Er is een afspraak voor U ingepland met {doctor.FullName}. Deze moet door door de dokter nog bevestigd worden.");
                _db.Appointments.Add(appointment);
                await _db.SaveChangesAsync();
                return 2;
            }

        }

        /// <summary>
        /// Gets the doctor list.
        /// </summary>
        /// <returns></returns>
        public List<DoctorViewModel> GetDoctorList()
        {
            var doctors = (from user in _db.Users
                           join userRole in _db.UserRoles on user.Id equals userRole.UserId
                           join role in _db.Roles.Where(x => x.Name == Helper.Doctor) on userRole.RoleId equals role.Id
                           select new DoctorViewModel
                           {
                               Id = user.Id,
                               // Ternary operator
                               Name = string.IsNullOrEmpty(user.MiddleName) ? user.FirstName + " " + user.LastName :
                                user.FirstName + " " + user.MiddleName + " " + user.LastName
                           }
                           ).OrderBy(u => u.Name).ToList();
            return doctors;
        }

        /// <summary>
        /// Gets the patient list.
        /// </summary>
        /// <returns></returns>
        public List<PatientViewModel> GetPatientList()
        {
            var patients = (from user in _db.Users
                            join userRole in _db.UserRoles on user.Id equals userRole.UserId
                            join role in _db.Roles.Where(x => x.Name == Helper.Patient) on userRole.RoleId equals role.Id
                            select new PatientViewModel
                            {
                                Id = user.Id,
                                // Ternary operator
                                Name = string.IsNullOrEmpty(user.MiddleName) ? user.FirstName + " " + user.LastName :
                                 user.FirstName + " " + user.MiddleName + " " + user.LastName
                            }
               ).OrderBy(u => u.Name).ToList();
            return patients;
        }

        /// <summary>
        /// Get the patient's appointments.
        /// </summary>
        /// <param name="patientid">The patientid.</param>
        /// <returns></returns>
        public List<AppointmentViewModel> PatientAppointments(string patientid)
        {
            return _db.Appointments.Where(a => a.PatientId == patientid).ToList().Select(
                c => new AppointmentViewModel()
                {
                    Id = c.Id,
                    Description = c.Description,
                    StartDate = c.StartDate.ToString("yyyy-MM-dd HH:mm"),
                    EndDate = c.EndDate.ToString("yyyy-MM-dd HH:mm"),
                    Title = c.Title,
                    Duration = c.Duration,
                    IsDoctorApproved = c.IsDoctorApproved
                }).ToList();
        }

        /// <summary>
        /// Gets the Doctor's appointments.
        /// </summary>
        /// <param name="doctorid">The doctorid.</param>
        /// <returns></returns>
        public List<AppointmentViewModel> DoctorAppointments(string doctorid)
        {
            return _db.Appointments.Where(a => a.DoctorId == doctorid).ToList().Select(
                c => new AppointmentViewModel()
                {
                    Id = c.Id,
                    Description = c.Description,
                    StartDate = c.StartDate.ToString("yyyy-MM-dd HH:mm"),
                    EndDate = c.EndDate.ToString("yyyy-MM-dd HH:mm"),
                    Title = c.Title,
                    Duration = c.Duration,
                    IsDoctorApproved = c.IsDoctorApproved
                }).ToList();
        }

        /// <summary>
        /// Gets appointment by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public AppointmentViewModel GetById(int id)
        {
            return _db.Appointments.Where(a => a.Id == id).ToList().Select(
                c => new AppointmentViewModel()
                {
                    Id = c.Id,
                    Description = c.Description,
                    StartDate = c.StartDate.ToString("d-MM-yyyy HH:mm"),
                    EndDate = c.EndDate.ToString("d-M-yyyy HH: mm"),
                    Title = c.Title,
                    Duration = c.Duration,
                    IsDoctorApproved = c.IsDoctorApproved,
                    PatientId = c.PatientId,
                    DoctorId = c.DoctorId,
                    PatientName = _db.Users.Where(u => u.Id == c.PatientId).Select(u => u.FullName).FirstOrDefault(),
                    DoctorName = _db.Users.Where(u => u.Id == c.DoctorId).Select(u => u.FullName).FirstOrDefault()
                }).SingleOrDefault();
        }

        public async Task<int> DeleteAppointment(int id)
        {
            var appointment = _db.Appointments.FirstOrDefault(a => a.Id == id);
            if (appointment != null)
            {
                _db.Appointments.Remove(appointment);
                return await _db.SaveChangesAsync();
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> ConfirmAppointment(int id)
        {
            var appointment = _db.Appointments.FirstOrDefault(a => a.Id == id);
            if (appointment != null)
            {
                appointment.IsDoctorApproved = true;
                return await _db.SaveChangesAsync();
            }
            else
            {
                return 0;
            }
        }
        #endregion

    }
}
