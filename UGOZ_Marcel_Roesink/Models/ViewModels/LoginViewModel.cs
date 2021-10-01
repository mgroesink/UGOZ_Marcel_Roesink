using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UGOZ_Marcel_Roesink.Models.ViewModels
{
    public class LoginViewModel
    {
        private string _email;

        [Required(ErrorMessage = "{0} is een verplicht veld")]
        [EmailAddress(ErrorMessage ="Dit is geen geldig e-mailadres")]
        [Display(Name ="E-mail")]
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value.ToLower();
            }

        }

        // Auto property
        [Required(ErrorMessage ="{0) is een verplicht veld") ]
        [DataType(DataType.Password)]
        [Display(Name ="Wachtwoord")]
        public string Password { get; set; }
        [Display(Name ="Onthoud mij")]
        public bool RememberMe { get; set; }

    }
}
