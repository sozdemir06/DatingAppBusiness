using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DatingApp.Core.Utilities.Helpers.AppHeaderHelper
{
    public static class AddPaginationHeader
    {
        public static void AddPagination(this HttpResponse response,int currentPage,int itemsPerPage,int totalItems,int totalPages)
        {
            var paginationheader=new PaginationHeader(currentPage,itemsPerPage,totalItems,totalPages);
            var camelCaseFormatter=new JsonSerializerSettings();
            camelCaseFormatter.ContractResolver=new CamelCasePropertyNamesContractResolver();
            response.Headers.Add("Pagination",JsonConvert.SerializeObject(paginationheader,camelCaseFormatter));
            response.Headers.Add("Access-Control-Expose-Headers","Pagination");

        }
    }
}