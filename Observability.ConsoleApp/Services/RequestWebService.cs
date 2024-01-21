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

            try
            {
                var eventTags = new ActivityTagsCollection();
                activity?.AddEvent(new("Request to Google started...", tags: eventTags));
                activity?.AddTag("request.schema","https");
                activity?.AddTag("request.method","GET");
                var result = await _httpClient.GetAsync("https://www.google.com");
                var responseContent = await result.Content.ReadAsStringAsync();
                activity?.AddTag("response.lenght", responseContent.Length);
                activity?.AddEvent(new("Request to Google finished.!", tags: eventTags));
                return responseContent.Length;
            }
            catch (Exception ex)
            {
                activity?.SetStatus(ActivityStatusCode.Error, $"Error Detail : {ex.Message}");
                return -1;
            }
            
        }

        internal async Task<int> MakeRequestToYandex()
        {
            using var activity = ActivitySourceProvider.Source.StartActivity(kind: ActivityKind.Server, name: "MakeRequestToYandex");

            try
            {
                var eventTags = new ActivityTagsCollection();
                activity?.AddEvent(new("Request to Yandex started...", tags: eventTags));
                activity?.AddTag("request.schema", "https");
                activity?.AddTag("request.method", "GET");
                var result = await _httpClient.GetAsync("https://www.yandex.com");
                var responseContent = await result.Content.ReadAsStringAsync();
                activity?.AddTag("response.lenght", responseContent.Length);
                activity?.AddEvent(new("Request to Yandex finished.!", tags: eventTags));
                return responseContent.Length;
            }
            catch (Exception ex)
            {
                activity?.SetStatus(ActivityStatusCode.Error, $"Error Detail : {ex.Message}");
                return -1;
            }

        }
    }
}
