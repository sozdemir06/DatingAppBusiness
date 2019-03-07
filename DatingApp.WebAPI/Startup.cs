using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DatingApp.Business.DependencyResolvers.NetCore;
using DatingApp.Business.Extensions;
using DatingApp.Business.Mappings.AutoMapper.Dtos;
using DatingApp.Business.ValidationRules.FluentValidation;
using DatingApp.WebAPI.Filters;
using DatingApp.WebAPI.SeedData;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DatingApp.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.BusinessServicess();
            services.AddMvc()
                             .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                             .AddFluentValidation()
                             .AddJsonOptions(opt => {
                     opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                 });
            services.FluentValidationServicess();
            services.AddCors();
            services.AddTransient<Seed>();
            services.AutoMapperService();
            services.AuthServices(Configuration);
            services.Configure<CloudinarySettings>(Configuration.GetSection("CloudinarySettings"));
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,Seed seeder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
                app.UseGlobalException();
            }

            //app.UseHttpsRedirection();
            //seeder.SeedUsers(); 
            app.UseCors(cors=>cors.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
