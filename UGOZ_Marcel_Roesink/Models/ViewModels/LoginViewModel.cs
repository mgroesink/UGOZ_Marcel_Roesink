using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UGOZ_Marcel_Roesink.Models.ViewModels
{
    public class LoginViewModel
    {

        #region Properties
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [Required(ErrorMessage = "{0} is een verplicht veld.")]
        [EmailAddress(ErrorMessage = "Dit is geen geldig e-mailadres")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [Required(ErrorMessage = "{0} is een verplicht veld.")]
        [DataType(DataType.Password)]
        [Display(Name = "Wachtwoord")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [remember me].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [remember me]; otherwise, <c>false</c>.
        /// </value>
        [Display(Name = "Onthoud mij")]
        public bool RememberMe { get; set; } 
        #endregion

    }
}





