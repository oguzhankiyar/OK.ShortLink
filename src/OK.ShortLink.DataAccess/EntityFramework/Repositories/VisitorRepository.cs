using OK.ShortLink.Common.Entities;
using OK.ShortLink.Core.Repositories;
using OK.ShortLink.DataAccess.EntityFramework.DataContexts;

namespace OK.ShortLink.DataAccess.EntityFramework.Repositories
{
    public class VisitorRepository : BaseRepository<VisitorEntity>, IVisitorRepository
    {
        public VisitorRepository(ShortLinkDataContext dataContext) : base(dataContext)
        {
        }
    }
}