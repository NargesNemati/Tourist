
using System;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Tourist.UI.Models.Dto;

namespace Tourist.UI.Services
{

    public class BookingService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BookingService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> CreateBookingAsync(Guid tourId)
        {
            var token = _httpContextAccessor.HttpContext!.Session.GetString("JWToken");
            Console.WriteLine($"TOKEN: {token}");
            if (string.IsNullOrEmpty(token)) {
                return false; }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var dto = new AddBookingDto
            {
                TourId = tourId,
                BookingDate = DateTime.Today,    
                NumberOfPeople = 1
            };
            var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://localhost:7241/api/booking", content);
            Console.WriteLine($"Status Code: {response.StatusCode}");
            var responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Response Body: {responseBody}");

            return response.IsSuccessStatusCode;
        }

        public async Task<List<BookingDto>?> GetUserBookingsAsync()
        {
            var token = _httpContextAccessor.HttpContext!.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token)) return null;

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync("https://localhost:7241/api/booking/user");
            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<BookingDto>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
