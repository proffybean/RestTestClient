using System;
using HttpClient.Models;
using HttpClient.Controllers;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;


namespace HttpClient.Controllers
{
    public abstract class ControllerBase
    {
        protected static System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();
        protected string BaseUrl;
        protected string Endpoint;
        protected string QueryString;

        // TODO merge with method below:
        protected async Task<HttpResponseMessage> SendPostRequest<T>()
        {
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("*/*"));

            string requestUri = BaseUrl + Endpoint + (String.IsNullOrEmpty(QueryString) ? String.Empty : $"?{QueryString}");

            HttpResponseMessage response = await httpClient.PostAsync(requestUri, null);

            return response;
        }

        protected async Task<HttpResponseMessage> SendPostRequest<Tin, Tout>(Tin dto)
        {
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));

            string jsonString = JsonConvert.SerializeObject(dto);
            StringContent content = new StringContent(jsonString);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            string requestUri = BaseUrl + Endpoint + 
                                (String.IsNullOrEmpty(QueryString) 
                                ? String.Empty 
                                : $"?{QueryString}");

            var response = await httpClient.PostAsync(requestUri, content);

            // Deserialize here and return the json or response code?

            //Tout foo = await response.Content.ReadAsAsync<Tout>();

            return response;
        }

        protected async Task<Tout> SendPutRequest<Tin, Tout>(Tin dto)
        {
            throw new NotImplementedException();
        }

        protected async Task<Tout> SendGetRequest<Tout>()
        {
            throw new NotImplementedException();
        }
    }
}