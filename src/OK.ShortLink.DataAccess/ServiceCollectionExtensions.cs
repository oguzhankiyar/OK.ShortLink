using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OK.ShortLink.DataAccess.EntityFramework.DataContexts;

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
        }
    }
}