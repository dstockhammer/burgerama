using System.ComponentModel.DataAnnotations;

namespace Burgerama.Web.Maintenance.Models.Ratings
{
    public sealed class CreateContextModel
    {
        [Required]
        [Display(Name = "Key")]
        public string ContextKey { get; set; }

        [Required]
        [Display(Name = "Gracefully handle unknown candidates")]
        public bool GracefullyHandleUnknownCandidates { get; set; }
    }
}