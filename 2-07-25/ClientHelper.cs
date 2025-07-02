using FoodieMVC.DTO;
using System.Text;
using System.Text.Json;

namespace FoodieMVC.Infra
{
    public class ClientHelper
    {
        private readonly HttpClient _httpClient;
        public ClientHelper(HttpClient client, IConfiguration cfg)
        {
            _httpClient = client;
            string BaseAdress = cfg.GetValue<string>("ApiData:url");
            _httpClient.BaseAddress = new Uri(BaseAdress);
        }

        public async Task<T> GetData<T>(string crtName)
        {
            HttpResponseMessage msg = await _httpClient.GetAsync(crtName);
            msg.EnsureSuccessStatusCode();
            string RespString = await msg.Content.ReadAsStringAsync();
            T list = JsonSerializer.Deserialize<T>(RespString);
            return list;

        }
        //public async Task<T> GetData<T>(string crtName)
        //{
        //    HttpResponseMessage msg = await _httpClient.GetAsync(crtName.TrimStart('/'));

            //    if (!msg.IsSuccessStatusCode)
            //    {
            //        var error = await msg.Content.ReadAsStringAsync();
            //        throw new HttpRequestException($"Request failed with status {msg.StatusCode}: {error}");
            //    }

            //    string RespString = await msg.Content.ReadAsStringAsync();

            //    return JsonSerializer.Deserialize<T>(RespString, new JsonSerializerOptions
            //    {
            //        PropertyNameCaseInsensitive = true
            //    });
            //}




            //==================================

        public async Task<TResponse> PostData<TRequest, TResponse>(string crtName, TRequest data)
        {
            string json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(crtName, content);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<TResponse>(responseBody, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task PostData<T>(string crtName, T data)
        {
            string json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(crtName, content);
            response.EnsureSuccessStatusCode();
        }
    }
}
