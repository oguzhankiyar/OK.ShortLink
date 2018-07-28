namespace OK.ShortLink.Api.Requests
{
    public class EditUserRequest
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public bool? IsActive { get; set; }
    }
}