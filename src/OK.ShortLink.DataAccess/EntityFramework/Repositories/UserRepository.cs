using OK.ShortLink.Common.Entities;
using OK.ShortLink.Core.Repositories;
using OK.ShortLink.DataAccess.EntityFramework.DataContexts;

namespace OK.ShortLink.DataAccess.EntityFramework.Repositories
{
    public class UserRepository : BaseRepository<UserEntity>, IUserRepository
    {
        public UserRepository(ShortLinkDataContext dataContext) : base(dataContext)
        {
        }
    }
}