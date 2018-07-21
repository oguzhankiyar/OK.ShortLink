using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OK.ShortLink.Core.Repositories;
using OK.ShortLink.DataAccess.EntityFramework.DataContexts;
using OK.ShortLink.DataAccess.EntityFramework.Repositories;

namespace OK.ShortLink.DataAccess
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDataAccessLayer(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ShortLinkDataContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.BuildServiceProvider().GetService<ShortLinkDataContext>().Database.Migrate();

            services.AddTransient<ILinkRepository, LinkRepository>();
            services.AddTransient<ILogRepository, LogRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IVisitorRepository, VisitorRepository>();
        }
    }
}