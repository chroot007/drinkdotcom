using DrinkDotCom.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DrinkDotCom.ViewModels
{
    public class LoginViewModel : PageViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel : PageViewModel
    {
        public string FullName { get; set; }
        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        [DisplayName("Date of Birth")]
        public DateTime? Dateofbirth { get; set; }

        public string PhoneNumber { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        [DisplayName("Postcode")]
        public string Zipcode { get; set; }

    }

    public class ForgotPasswordViewModel : PageViewModel
    {
        public string Username { get; set; }
    }


    public class ResetPasswordViewModel : PageViewModel
    {
        public string UserId { get; set; }
        public string Code { get; set; }

        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class ProfileDetailsViewModel : PageViewModel
    {
        public DrinkDotComUser User { get; set; }
    }

    public class UpdateProfileDetailsViewModel : PageViewModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
    }

    public class UpdatePasswordViewModel : PageViewModel
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class ExternalLoginConfirmationViewModel
    {
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }
}