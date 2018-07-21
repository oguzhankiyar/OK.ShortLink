namespace OK.ShortLink.Common.Entities
{
    public class VisitorEntity : BaseEntity
    {
        public int LinkId { get; set; }

        public string IPAddress { get; set; }

        public string UserAgent { get; set; }

        public string OSInfo { get; set; }

        public string DeviceInfo { get; set; }

        public string BrowserIndo { get; set; }

        public virtual LinkEntity Link { get; set; }
    }
}