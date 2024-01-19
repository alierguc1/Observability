using Observability.ConsoleApp.Providers;
using Observability.ConsoleApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observability.ConsoleApp.Helper
{
    internal class ServiceHelper
    {
        static HttpClient _httpClient = new HttpClient();


        internal async Task Work_1()
        {
            using var activity = ActivitySourceProvider.Source.StartActivity();
            var _requestWebService = new RequestWebService();
            Console.WriteLine("--------------------------------------------------------------------------");
            Console.WriteLine($"Google Response Lenght : {await _requestWebService.MakeRequestToGoogle()}");
            Console.WriteLine("--------------------------------------------------------------------------");
            Console.WriteLine("Work_1 Finished");

        }

     

    }
}
