using System.ComponentModel.DataAnnotations;

namespace OK.ShortLink.Api.Requests
{
    public class CreateLinkRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string ShortUrl { get; set; }

        [Required]
        public string OriginalUrl { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}