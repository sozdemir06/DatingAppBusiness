using DatingApp.Business.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DatingApp.Business.DependencyResolvers.NetCore
{
    public  static class CloudinarySettingModule
    {
        public static IServiceCollection CloudinaryServices(this IServiceCollection services,IConfiguration config)
        {
            //services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
            return services;
        }
    }
}