using AspNet_Core.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNet_Core.Models.ViewModel
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        //to use clint side 
        [Remote("ISEmailInUse","account")]
        //custom email valide
        [ValidEmailDomain(allowDomain: "gmail.com",ErrorMessage ="Email domain must be gmail.com")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [DataType(DataType.Password)]
        [DisplayName("Confirm Passowrd")]
        [Compare("Password", ErrorMessage ="Don't Match Passowrd")]
        public string ConfirmPassword { get; set; }

        public string City { get; set; }
    }
}
