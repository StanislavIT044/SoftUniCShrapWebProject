namespace WindowToTheSociety.Web.ViewModels.Identity
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using WindowToTheSociety.Data.Models;

    public class RegisterInputModel
    {
        [Required]
        [StringLength(25, ErrorMessage = "First Name must be between 2 and 25 characters long.", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "Surname must be between 2 and 25 characters long.", MinimumLength = 2)]
        public string Surname { get; set; }

        public Gender Gender { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
