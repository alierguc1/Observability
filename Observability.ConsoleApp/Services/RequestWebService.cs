﻿using Observability.ConsoleApp.Providers;
using System;
using System.Collections.Generic;
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
            using var activity = ActivitySourceProvider.Source.StartActivity();
            var result = await _httpClient.GetAsync("https://www.google.com");
            var responseContent = await result.Content.ReadAsStringAsync();
            return responseContent.Length;
        }
    }
}
