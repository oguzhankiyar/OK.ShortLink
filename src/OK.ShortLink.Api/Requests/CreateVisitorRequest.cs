namespace OK.ShortLink.Api.Requests
{
    public class CreateVisitorRequest
    {
        public int LinkId { get; set; }

        public string IPAddress { get; set; }

        public string UserAgent { get; set; }

        public string OSInfo { get; set; }

        public string DeviceInfo { get; set; }

        public string BrowserInfo { get; set; }
    }
}