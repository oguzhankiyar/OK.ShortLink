using Microsoft.Extensions.DependencyInjection;
using OK.ShortLink.Core.Logging;
using OK.ShortLink.Engine.Logging;

namespace OK.ShortLink.Engine
{
    public static class ServiceCollectionExtensions
    {
        public static void AddEngineLayer(this IServiceCollection services)
        {
            services.AddSingleton<ILogger, NLoggerImpl>();
        }
    }
}