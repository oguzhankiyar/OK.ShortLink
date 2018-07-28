namespace OK.ShortLink.Api.Requests
{
    public class CreateLinkRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string ShortUrl { get; set; }

        public string OriginalUrl { get; set; }

        public bool IsActive { get; set; }
    }
}