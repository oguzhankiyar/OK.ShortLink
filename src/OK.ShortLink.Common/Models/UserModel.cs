using System;

namespace OK.ShortLink.Common.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}