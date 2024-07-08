using MutiTenantApplication.Models;

namespace MutiTenantApplication
{
    public class PostService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;
        private readonly string _tenantId;

        public PostService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiBaseUrl = configuration["ApiBaseUrl"];
            _tenantId = configuration["TenantId"];
        }

        //public async Task<List<Post>> GetPostsAsync()
        //{
        //    return await _httpClient.GetFromJsonAsync<List<Post>>($"{_apiBaseUrl}/api/posts");
        //}

        public async Task<List<Post>> GetPostsAsync()
        {
            // Assuming the API is hosted at "https://api.example.com"
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_apiBaseUrl}/api/posts");
            request.Headers.Add("Tenant-Id", _tenantId);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Post>>();
        }
    }
}
