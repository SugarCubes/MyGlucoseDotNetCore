using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyGlucoseDotNetCore.Models.ManageViewModels
{
    public class IndexViewModel
    {
        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        //Heather Harvey
        [FirstName]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        //Heather Harvey
        [LastName]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string StatusMessage { get; set; }
    }
}
