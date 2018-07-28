namespace OK.ShortLink.Api.Responses
{
    public class CreateTokenResponse
    {
        public string Token { get; set; }

        public int ExpiresIn { get; set; }
    }
}