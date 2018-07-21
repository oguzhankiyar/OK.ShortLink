using OK.ShortLink.Common.Entities;
using OK.ShortLink.Core.Repositories;
using OK.ShortLink.DataAccess.EntityFramework.DataContexts;

namespace OK.ShortLink.DataAccess.EntityFramework.Repositories
{
    public class LogRepository : BaseRepository<LogEntity>, ILogRepository
    {
        public LogRepository(ShortLinkDataContext dataContext) : base(dataContext)
        {
        }
    }
}