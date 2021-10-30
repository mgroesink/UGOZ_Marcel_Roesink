using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UGOZ_Marcel_Roesink.Models
{
    public class ApplicationUser: IdentityUser
    {

        #region Properties
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get; set; }
        /// <summary>
        /// Gets or sets the middle name.
        /// </summary>
        /// <value>
        /// The middle name.
        /// </value>
        public string MiddleName { get; set; }
        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets the full name.
        /// </summary>
        /// <value>
        /// The full name.
        /// </value>
        public string FullName
        {
            get
            {
                if (string.IsNullOrEmpty(MiddleName))
                {
                    return FirstName + " " + LastName;
                }
                else
                {
                    return FirstName + " " + MiddleName + " " + LastName;
                }
            }
        } 
        #endregion

    }
}
