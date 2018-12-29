namespace Sales.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Common.Models;
    using Newtonsoft.Json;
    using Plugin.Connectivity;

    public class ApiService
    {

        public async Task<Response> CheckConnection()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Please turn on your internet settings."//Languages.TurnOnInternet,
                };
            }

            var isReachable = await CrossConnectivity.Current.IsRemoteReachable("google.com");
            if (!isReachable)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "No internet connection"//Languages.NoInternet,
                };
            }

            return new Response
            {
                IsSuccess = true,
            };
        }

        public async Task<Response> GetList<T>(string urlBase, string prefix, string controller) {
            try
            {
                var client = new HttpClient();
                var relativeUrl = $"{prefix}{controller}";
                client.BaseAddress = new Uri(urlBase);
                var response = await client.GetAsync(relativeUrl);
                var answer = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode) {
                    return new Response()
                    {
                        IsSuccess = false,
                        Message = answer
                    };
                }

                List<T> list = JsonConvert.DeserializeObject<List<T>>(answer);
                return new Response() {
                    IsSuccess = true,
                    Message = null,
                    Result = list
                };
            }
            catch (Exception ex) {
                return new Response()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Result = null
                };
            }
        }
    }

}
