using System;

namespace OK.ShortLink.Common.Models
{
    public class VisitorModel
    {
        public int Id { get; set; }

        public int LinkId { get; set; }

        public LinkModel Link { get; set; }

        public string IPAddress { get; set; }

        public string UserAgent { get; set; }

        public string OSInfo { get; set; }

        public string DeviceInfo { get; set; }

        public string BrowserIndo { get; set; }

        public DateTime CreatedDate { get; set; }
        
        public DateTime UpdatedDate { get; set; }
    }
}