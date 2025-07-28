using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Tourist.UI.Models.Dto;

namespace Tourist.UI.Services
{
    public class ReviewService
    {
        private readonly HttpClient httpClient;
        private readonly IHttpContextAccessor httpContextAccessor;
        public ReviewService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            this.httpClient = httpClient;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<ReviewDto>> GetReviewsByTourIdAsync(Guid tourId)
        {
            var response = await httpClient.GetAsync($"https://localhost:7241/api/review/Tour/{tourId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<ReviewDto>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return new List<ReviewDto>();
        }

        public async Task<bool> AddReviewAsync(AddReviewDto addReviewDto)
        {
            var token = httpContextAccessor.HttpContext.Session.GetString("JWToken");
            Console.WriteLine($"TOKEN: {token}");
            if (string.IsNullOrEmpty(token))
            {
                return false;
            }
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var content = new StringContent(JsonSerializer.Serialize(addReviewDto), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("https://localhost:7241/api/review", content);
            var responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Response Body: {responseBody}");
            return response.IsSuccessStatusCode;
        }
    }
}
