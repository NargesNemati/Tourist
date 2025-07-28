using Tourist.UI.Models.Dto;

namespace Tourist.UI.Services
{
    public class AuthService
    {

        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "https://localhost:7241/api";

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> RegisterAsync(RegisterRequestDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_apiBaseUrl}/auth/register", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_apiBaseUrl}/auth/login", dto);
            var responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Response Body: {responseBody}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<LoginResponseDto>();
            }
            return null;
        }
    }
}
