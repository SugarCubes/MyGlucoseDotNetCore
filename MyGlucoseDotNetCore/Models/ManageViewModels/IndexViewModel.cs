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
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        //Heather Harvey
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        //Heather Harvey
        [Display(Name = "Address 1")]
        public string Address1 { get; set; }

        //Heather Harvey
        [Display(Name = "Address 2")]
        public string Address2 { get; set; }

        //Heather Harvey
        [Display(Name = "City")]
        public string City { get; set; }

        //Heather Harvey
        [Display(Name = "State")]
        public string State { get; set; }

        //Heather Harvey
        [Display(Name = "Zip Code 1")]
        public int Zip1 { get; set; }

        //Heather Harvey
        [Display(Name = "Zip Code 2")]
        public int Zip2 { get; set; }

        public string StatusMessage { get; set; }
    }
}
