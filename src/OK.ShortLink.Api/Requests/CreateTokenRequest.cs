using System.ComponentModel.DataAnnotations;

namespace OK.ShortLink.Api.Requests
{
    public class CreateTokenRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}