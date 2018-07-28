using System.ComponentModel.DataAnnotations;

namespace OK.ShortLink.Api.Requests
{
    public class EditUserRequest
    {
        [Required]
        public int Id { get; set; }
        
        public string Password { get; set; }
        
        public bool? IsActive { get; set; }
    }
}