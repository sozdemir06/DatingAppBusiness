using System.Net;
using DatingApp.Core.Utilities.Helpers.AppHeaderHelper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace DatingApp.Business.Extensions
{
    public static class GlobalErrorHandling
    {
          public static IApplicationBuilder UseGlobalException(this IApplicationBuilder app)
            {
                app.UseExceptionHandler(builder=>{
                    builder.Run(async context=>{
                        context.Response.StatusCode=(int)HttpStatusCode.InternalServerError;
                        var error=context.Features.Get<IExceptionHandlerFeature>();
                        if(error!=null)
                        {
                            context.Response.AddApplicationErrorHeader(error.Error.Message);
                            await context.Response.WriteAsync(error.Error.Message);
                        }    
                    });
                });
                
                return app;
            }
    }
}