using System.ComponentModel.DataAnnotations;

namespace Burgerama.Services.Users.Api.Models.Account
{
    public class RegisterExternalBindingModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Surname")]
        public string Surname { get; set; }
    }
}