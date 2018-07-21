namespace OK.ShortLink.Common.Entities
{
    public class LinkEntity : BaseEntity
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        
        public string ShortUrl { get; set; }
        
        public string OriginalUrl { get; set; }

        public bool IsActive { get; set; }
                
        public virtual UserEntity User { get; set; }
    }
}