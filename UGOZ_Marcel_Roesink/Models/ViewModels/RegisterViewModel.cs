using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UGOZ_Marcel_Roesink.Models.ViewModels
{
    public class RegisterViewModel
    {
        #region Properties
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        [DisplayName("Voornaam")]
        [Required(ErrorMessage = "{0} is een verplicht veld.")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the name of the middle.
        /// </summary>
        /// <value>
        /// The name of the middle.
        /// </value>
        [DisplayName("Tussenvoegsels")]
        public string MiddleName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [DisplayName("Achternaam")]
        [Required(ErrorMessage = "{0} is een verplicht veld.")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [EmailAddress]
        [Required(ErrorMessage = "{0} is een verplicht veld.")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [Required(ErrorMessage = "{0} is een verplicht veld.")]
        [DataType(DataType.Password)]
        [DisplayName("Wachtwoord")]
        [StringLength(100, ErrorMessage = "Het {0} moet minstens {2} tekens bevatten.",
            MinimumLength = 6)]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the password confirm.
        /// </summary>
        /// <value>
        /// The password confirm.
        /// </value>
        [DisplayName("Bevestig wachtwoord")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Wachtwoorden komen niet overeen")]
        public string PasswordConfirm { get; set; }

        /// <summary>
        /// Gets or sets the name of the role.
        /// </summary>
        /// <value>
        /// The name of the role.
        /// </value>
        [DisplayName("Rol")]
        [Required(ErrorMessage = "{0} is een verplicht veld.")]
        public string RoleName { get; set; } 
        #endregion
    }
}

