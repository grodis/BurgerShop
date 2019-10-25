using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace beeftechee.ViewModels
{
    public class EditPersonalInfoViewModel
    {

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string City { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        public string Address { get; set; }

        [Required]
        [Range(10000, 99999, ErrorMessage = "Invalid Postal code(10000-99999).")]
        public int PostalCode { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [StringLength(14, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 10)]
        [Phone]
        public string PhoneNumber { get; set; }
    }
}