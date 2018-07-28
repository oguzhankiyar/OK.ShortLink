using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using OK.ShortLink.Api.Filters;
using OK.ShortLink.Api.Middlewares;
using OK.ShortLink.Core.Logging;
using OK.ShortLink.DataAccess;
using OK.ShortLink.Engine;
using System.Text;

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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = Configuration["Jwt:Issuer"],
                            ValidAudience = Configuration["Jwt:Issuer"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                        };
                    });

            services.AddDataAccessLayer(Configuration.GetConnectionString("ShortLinkConnection"));

            services.AddEngineLayer();

            services.AddMvc(options => { options.Filters.Add(new ModelStateValidationFilter()); })
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

            app.UseAuthentication();

            app.UseMiddleware<LoggingMiddleware>();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}