using Microsoft.AspNetCore.Http;

namespace DatingApp.Core.Utilities.Helpers.AppHeaderHelper
{
    public static class ApplicationHeader
    {
        public static void AddApplicationErrorHeader(this HttpResponse response,string message)
        {
            response.Headers.Add("Application-Error",message);
            response.Headers.Add("Access-Control-Expose-Headers","Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin","*");
        }
    }
}