using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.Business.DependencyResolvers.NetCore
{
    public static class AuthenticationModule
    {
        public static IServiceCollection AuthServices(this IServiceCollection services,IConfiguration config)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options=>{
                        options.TokenValidationParameters=new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey=true,
                            IssuerSigningKey=new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config.GetSection("AppSettings:Token").Value)),
                            ValidateIssuer=false,
                            ValidateAudience=false   
                        };
                    });
            return services;
        }
    }
}