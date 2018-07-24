namespace OK.ShortLink.Api.Responses
{
    public class AuthenticationResponse : BaseResponse
    {
        public string Token { get; set; }

        public int ExpiresIn { get; set; }
    }
}