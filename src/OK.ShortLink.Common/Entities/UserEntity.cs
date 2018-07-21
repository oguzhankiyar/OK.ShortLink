namespace OK.ShortLink.Common.Entities
{
    public class UserEntity : BaseEntity
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public bool IsActive { get; set; }
    }
}