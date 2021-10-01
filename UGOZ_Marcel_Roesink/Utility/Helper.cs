using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UGOZ_Marcel_Roesink.Utility
{
    public static class Helper
    {
        public static readonly string Admin = "Beheerder";
        public static readonly string Patient = "Patient";
        public static readonly string Doctor = "Dokter";

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

    }
}
