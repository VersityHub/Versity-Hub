using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace VersityHub.VersityHubWebAPI.Customer.Model
{
    public class ApplicationCustomer : IdentityUser
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        [RegularExpression(@"^\+234\d{10}$", ErrorMessage = "Invalid Phone Number format. It should start with +234 and have 10 additional digits.")]
        public string PhoneNumber { get; set; }

        public string Gender { get; set; }

        public string State { get; set; }

        public string University { get; set; }

        public string UniversityID { get; set; }

        [Required(ErrorMessage = "Date of Birth is required")]
        [RegularExpression(@"^\d{4}-\d{2}-\d{2}$", ErrorMessage = "Invalid Date of Birth format")]
        public string DateOfBirth { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
