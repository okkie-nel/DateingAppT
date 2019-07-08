using System;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Datingapp.API.Helpers
{
    public static class extensions
    {
        public static void AddApplicationError(this HttpResponse response, string message){
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }

        public static void AddPagination(this HttpResponse response, int currentpage, int itemsperpage, int totalitems, int totalpages)
        {
            var paginationheader = new PaginationHeader(currentpage, itemsperpage, totalitems, totalpages);
            var camelCaseFromat = new JsonSerializerSettings();
            camelCaseFromat.ContractResolver = new  CamelCasePropertyNamesContractResolver();
            response.Headers.Add("Pagination", JsonConvert.SerializeObject(paginationheader, camelCaseFromat));
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }
        public static int CalcAge(this DateTime theDateTime){
            var age = DateTime.Today.Year - theDateTime.Year;
            if (theDateTime.AddYears(age) > DateTime.Today)
            age--;
            
            return age;
        } 
    }
}