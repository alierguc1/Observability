using Observability.ConsoleApp.Providers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Observability.ConsoleApp.Services
{
    internal class RequestWebService
    {
        static HttpClient _httpClient = new HttpClient();
        internal async Task<int> MakeRequestToGoogle()
        {
            using var activity = ActivitySourceProvider.Source.StartActivity(kind: ActivityKind.Producer, name: "MakeRequestToGoogle");
            var eventTags = new ActivityTagsCollection();                
            activity?.AddEvent(new("Request to Google started...",tags: eventTags));
            var result = await _httpClient.GetAsync("https://www.google.com");
            var responseContent = await result.Content.ReadAsStringAsync();
            eventTags.Add("Google request body lenght : ", responseContent.Length);
            activity?.AddEvent(new("Request to Google finished.!", tags: eventTags));
            return responseContent.Length;
        }
    }
}
