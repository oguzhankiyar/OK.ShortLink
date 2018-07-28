using System.ComponentModel.DataAnnotations;

namespace OK.ShortLink.Api.Requests
{
    public class CreateVisitorRequest
    {
        [Required]
        public int LinkId { get; set; }

        [Required]
        public string IPAddress { get; set; }

        [Required]
        public string UserAgent { get; set; }

        [Required]
        public string OSInfo { get; set; }

        [Required]
        public string DeviceInfo { get; set; }

        [Required]
        public string BrowserInfo { get; set; }
    }
}