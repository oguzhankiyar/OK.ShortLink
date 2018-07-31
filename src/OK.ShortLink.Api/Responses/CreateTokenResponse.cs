using System;

namespace OK.ShortLink.Api.Responses
{
    public class CreateTokenResponse
    {
        public string Token { get; set; }

        public DateTime ExpiresIn { get; set; }
    }
}