using System.ComponentModel.DataAnnotations;

namespace OK.ShortLink.Api.Requests
{
    public class CreateUserRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}