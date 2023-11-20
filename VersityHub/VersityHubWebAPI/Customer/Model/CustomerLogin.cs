using System.ComponentModel.DataAnnotations;

namespace VersityHub.VersityHubWebAPI.Customer.Model
{
    public class CustomerLogin
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
