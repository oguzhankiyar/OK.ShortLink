namespace OK.ShortLink.Api.Requests
{
    public class CreateUserRequest
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public bool IsActive { get; set; }
    }
}