using Microsoft.Extensions.DependencyInjection;
using OK.ShortLink.Common.Entities;
using OK.ShortLink.Common.Models;
using OK.ShortLink.Core.Logging;
using OK.ShortLink.Core.Mapping;
using OK.ShortLink.Engine.Logging;
using OK.ShortLink.Engine.Mapping;

namespace OK.ShortLink.Engine
{
    class MappingProfile : AutoMapper.Profile { }

    public static class ServiceCollectionExtensions
    {
        public static void AddEngineLayer(this IServiceCollection services)
        {
            services.AddSingleton<ILogger, NLoggerImpl>();

            services.AddTransient((provider) => { return CreateMappingProfile(); });
            services.AddTransient<IMapper, AutoMapperImpl>();
        }

        public static AutoMapper.Profile CreateMappingProfile()
        {
            var mappingProfile = new MappingProfile();

            mappingProfile.CreateMap<LinkEntity, LinkModel>();
            mappingProfile.CreateMap<LinkModel, LinkEntity>();

            mappingProfile.CreateMap<UserEntity, UserModel>();
            mappingProfile.CreateMap<UserModel, UserEntity>();

            mappingProfile.CreateMap<VisitorEntity, VisitorModel>();
            mappingProfile.CreateMap<VisitorModel, VisitorEntity>();

            return mappingProfile;
        }
    }
}