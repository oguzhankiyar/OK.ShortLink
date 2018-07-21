using OK.ShortLink.Common.Entities;
using OK.ShortLink.Core.Repositories;
using OK.ShortLink.DataAccess.EntityFramework.DataContexts;

namespace OK.ShortLink.DataAccess.EntityFramework.Repositories
{
    public class LinkRepository : BaseRepository<LinkEntity>, ILinkRepository
    {
        public LinkRepository(ShortLinkDataContext dataContext) : base(dataContext)
        {
        }
    }
}