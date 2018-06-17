using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace ContactManagementWebSite.Models
{
    public class Contact
    {

        public int Id { get; set; }

        [StringLength(30, ErrorMessage = "First name cannot be longer than 30 characters.")]
        [Required(ErrorMessage = "FirstName is required")]
        [RegularExpression(@"^[0-9a-zA-Z''-'\s]{1,40}$", ErrorMessage = "First name :- special characters are not  allowed.")]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [StringLength(30, ErrorMessage = "Last name cannot be longer than 30 characters.")]
        [RegularExpression(@"^[0-9a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Last name :- special characters are not  allowed.")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [StringLength(30, ErrorMessage = "Email cannot be longer than 30 characters.")]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [StringLength(10, ErrorMessage = "Phone number cannot be longer than 10 digits.")]
        [Required(ErrorMessage = "Phone number is required")]
        [Display(Name = "Phone")]
        [Range(0, 9999999999, ErrorMessage = "Invalid Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Status")]
        public int Status { get; set; }

    }

    public enum ContactStatus : int
    {
        Active = 0,
        Inactive = 1
    }

}