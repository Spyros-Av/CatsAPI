using CatsAPI.Models.ApiResponses;
using System.Text.Json;

namespace CatsAPI.Services
{
    public class SqlCatApiService : ICatApiService
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;

        public SqlCatApiService(HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.configuration = configuration;

            var baseUrl = configuration["CatsApi:BaseUrl"];
            var apiKey = configuration["CatsApi:ApiKey"];

            if (!string.IsNullOrEmpty(baseUrl))
            {
                httpClient.BaseAddress = new Uri(baseUrl);
            }

            if (!string.IsNullOrEmpty(apiKey))
            {
                httpClient.DefaultRequestHeaders.Add("x-api-key", apiKey);
            }
        }
        public async Task<List<CatApiResponses>> FetchCatsFromApiAsync(int count = 25)
        {
            var requestUrl = $"images/search?limit={count}";
            var response = await httpClient.GetAsync(requestUrl);
            response.EnsureSuccessStatusCode();
            var jsonResponseContent = await response.Content.ReadAsStringAsync();
            var cats = JsonSerializer.Deserialize<List<CatApiResponses>>(jsonResponseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return cats ?? new List<CatApiResponses>();
        }
    }
}
