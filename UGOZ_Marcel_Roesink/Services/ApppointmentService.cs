using System;
using System.Collections.Generic;
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
    }
}
