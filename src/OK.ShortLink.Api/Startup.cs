using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OK.ShortLink.Api.Middlewares;
using OK.ShortLink.Core.Logging;
using OK.ShortLink.DataAccess;
using OK.ShortLink.Engine;

namespace OK.ShortLink.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDataAccessLayer(Configuration.GetConnectionString("ShortLinkConnection"));

            services.AddEngineLayer();

            services.AddMvc()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILogger logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            logger.SetGlobalProperty("ConnectionString", Configuration.GetConnectionString("ShortLinkConnection"));
            logger.SetGlobalProperty("Channel", "OK.ShortLink.Api");

            app.UseMiddleware<LoggingMiddleware>();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}