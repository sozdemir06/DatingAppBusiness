using DatingApp.Business.Mappings.AutoMapper.Dtos;
using DatingApp.Business.ValidationRules.FluentValidation;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace DatingApp.Business.DependencyResolvers.NetCore
{
    public static class FluentValidatonModule
    {
         public static IServiceCollection FluentValidationServicess(this IServiceCollection services)
        {
            services.AddTransient<IValidator<UserForLoginDto>, UserForLoginDtoValidator>();
            services.AddTransient<IValidator<UserForRegisterDto>, UserForRegisterDtoValidator>();
            return services;
        }
            
    }
}