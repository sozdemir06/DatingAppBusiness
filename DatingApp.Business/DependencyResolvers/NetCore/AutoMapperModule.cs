using Microsoft.Extensions.DependencyInjection;
using AutoMapper;


namespace DatingApp.Business.DependencyResolvers.NetCore
{
    public static class AutoMapperModule
    {
        public static IServiceCollection AutoMapperService(this IServiceCollection services)
        {
            services.AddAutoMapper();
            return services;
        }
    }
}