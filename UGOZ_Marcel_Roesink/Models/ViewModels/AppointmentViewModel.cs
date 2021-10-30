using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UGOZ_Marcel_Roesink.Models.ViewModels
{
    public class AppointmentViewModel
    {
        #region Properties
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int? Id { get; set; }
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        public string StartDate { get; set; }
        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        public string EndDate { get; set; }
        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        /// <value>
        /// The duration.
        /// </value>
        public int Duration { get; set; }
        /// <summary>
        /// Gets or sets the doctor identifier.
        /// </summary>
        /// <value>
        /// The doctor identifier.
        /// </value>
        public string DoctorId { get; set; }
        /// <summary>
        /// Gets or sets the patient identifier.
        /// </summary>
        /// <value>
        /// The patient identifier.
        /// </value>
        public string PatientId { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is doctor approved.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is doctor approved; otherwise, <c>false</c>.
        /// </value>
        public bool IsDoctorApproved { get; set; }
        /// <summary>
        /// Gets or sets the admin identifier.
        /// </summary>
        /// <value>
        /// The admin identifier.
        /// </value>
        public string AdminId { get; set; }

        /// <summary>
        /// Gets or sets the name of the doctor.
        /// </summary>
        /// <value>
        /// The name of the doctor.
        /// </value>
        public string DoctorName { get; set; }
        /// <summary>
        /// Gets or sets the name of the patient.
        /// </summary>
        /// <value>
        /// The name of the patient.
        /// </value>
        public string PatientName { get; set; }
        /// <summary>
        /// Gets or sets the name of the admin.
        /// </summary>
        /// <value>
        /// The name of the admin.
        /// </value>
        public string AdminName { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is for client.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is for client; otherwise, <c>false</c>.
        /// </value>
        public bool IsForClient { get; set; } 
        #endregion

    }
}
