using System.ComponentModel.DataAnnotations;

namespace Burgerama.Services.Users.Api.Models.Account
{
    public class AddExternalLoginBindingModel
    {
        [Required]
        [Display(Name = "External access token")]
        public string ExternalAccessToken { get; set; }
    }
}