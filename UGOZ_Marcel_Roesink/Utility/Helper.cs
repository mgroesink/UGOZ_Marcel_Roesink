using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UGOZ_Marcel_Roesink.Utility
{
    public static class Helper
    {
        #region Constants
        public static readonly string Admin = "Beheerder";
        public static readonly string Patient = "Patient";
        public static readonly string Doctor = "Dokter";
        public static string AppointmentAdded = "Afspraak succesvol opgeslagen.";
        public static string AppointmentConfirmed = "Afspraak bevestigd.";
        public static string AppointmentUpdated = "Afspraak succesvol gewijzigd.";
        public static string AppointmentDeleted = "Afspraak succesvol verwijderd.";
        public static string AppointmentExists = "Afspraak bestaat al op gegeven datum en tijdstip.";
        public static string AppointmentNotExists = "Afspraak bestaat niet.";
        public static string AppointmentAddError = "Er ging iets mis. Afspraak niet toegevoegd.";
        public static string AppointmentConfirmError = "Er ging iets mis. Afspraak niet bevestigd.";
        public static string SomethingWentWrong = "Er ging iets mis. Probeer het opnieuw.";
        public static string AppointmentUpdatError = "Er ging iets mis. Afspraak niet gewijzigd.";
        public static int Succes_code = 1;
        public static int Failure_code = 0;
        #endregion

        #region Methods
        /// <summary>
        /// Gets the roles for drop down.
        /// </summary>
        /// <param name="isAdmin">if set to <c>true</c> [is admin].</param>
        /// <returns></returns>
        public static List<SelectListItem> GetRolesForDropDown(bool isAdmin = false)
        {
            var items = new List<SelectListItem>
            {
                new SelectListItem{Value=Helper.Admin , Text=Helper.Admin},
                new SelectListItem{Value=Helper.Patient , Text=Helper.Patient},
                new SelectListItem{Value=Helper.Doctor , Text=Helper.Doctor}
            };
            return items.OrderBy(s => s.Text).ToList();
        }

        /// <summary>
        /// Gets a dropdownlist with time blocks.
        /// </summary>
        /// <param name="maxtime">The maximum time for an appointment.</param>
        /// <param name="blocksize">The blocksize for one appointment.</param>
        /// <returns></returns>
        public static List<SelectListItem> GetTimeDropDown(int maxtime, int blocksize)
        {
            List<SelectListItem> durations = new List<SelectListItem>();
            for (int i = blocksize; i <= maxtime; i += blocksize)
            {
                durations.Add(new SelectListItem { Value = i.ToString(), Text = i + " minuten" });
            }
            return durations;
        } 
        #endregion

    }
}
