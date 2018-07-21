using System;

namespace OK.ShortLink.Common.Models
{
    public class LinkModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public UserModel User { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        
        public string ShortUrl { get; set; }
        
        public string OriginalUrl { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}