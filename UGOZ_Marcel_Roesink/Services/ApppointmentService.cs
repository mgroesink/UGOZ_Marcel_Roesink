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
        private readonly ApplicationDbContext _db;

        public AppointmentService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<int> AddUpdate(AppointmentViewModel model)
        {
            // Let op het gebruik van een specifieke culture om de datum string te converteren naar een DateTime object
            var startDate = DateTime.Parse(model.StartDate, CultureInfo.CreateSpecificCulture("en-US"));
            var endDate = startDate.AddMinutes(Convert.ToDouble(model.Duration));
            if(model != null && model.Id > 0)
            {
                //TODO: Add code to update existing appointment
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
                _db.Appointments.Add(appointment);
                await _db.SaveChangesAsync();
                return 2;
            }

        }

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

        public List<AppointmentViewModel> PatientAppointments(string patientid)
        {
            return _db.Appointments.Where(a => a.PatientId == patientid).ToList().Select(
                c => new AppointmentViewModel()
                {
                    Id = c.Id,
                    Description = c.Description,
                    StartDate = c.StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    EndDate = c.EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    Title = c.Title,
                    Duration = c.Duration,
                    IsDoctorApproved = c.IsDoctorApproved
                }).ToList();
        }

        public List<AppointmentViewModel> DoctorAppointments(string doctorid)
        {
            return _db.Appointments.Where(a => a.DoctorId == doctorid).ToList().Select(
                c => new AppointmentViewModel()
                {
                    Id = c.Id,
                    Description = c.Description,
                    StartDate = c.StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    EndDate = c.EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    Title = c.Title,
                    Duration = c.Duration,
                    IsDoctorApproved = c.IsDoctorApproved
                }).ToList();
        }
    }
}
