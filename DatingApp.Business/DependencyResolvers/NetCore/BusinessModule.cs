using DatingApp.Business.Abstract;
using DatingApp.Business.Concrete.Managers;
using DatingApp.Core.Utilities.Helpers.AuthHelpers;
using DatingApp.DataAccess.Abstract;
using DatingApp.DataAccess.Concrete;
using DatingApp.DataAccess.Concrete.EntityFramework;
using Microsoft.Extensions.DependencyInjection;


namespace DatingApp.Business.DependencyResolvers.NetCore
{
    public static class BusinessModule
    {
        public static IServiceCollection BusinessServicess(this IServiceCollection services)
        {
            
            
            services.AddDbContext<DataContext>();
            services.AddScoped<IValueService,ValueManager>();
            services.AddScoped<IValueDal,EfValueDal>();
            services.AddScoped<IUserService,UserManager>();
            services.AddScoped<IUserDal,EfUserDal>();
            services.AddScoped<IAuthHelper,AuthHelper>();
            services.AddScoped<IPhotoService,PhotoManager>();
            services.AddScoped<IPhotoDal,EfPhotoDal>();
            services.AddScoped<ILikeService,LikeManager>();
            services.AddScoped<ILikeDal,EfLikeDal>();
            services.AddScoped<IMessageService,MessageManager>();
            services.AddScoped<IMessageDal,EfMessageDal>();
            

            return services;
        }
    }
}