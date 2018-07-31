using System.ComponentModel.DataAnnotations;

namespace OK.ShortLink.Api.Requests
{
    public class EditUserRequest
    {        
        public string Password { get; set; }
        
        public bool? IsActive { get; set; }
    }
}