
using System.Net.Http;
using System.Net.Http.Json;
using Tourist.UI.Models.Dto;

namespace Tourist.UI.Services
    {
        public class TourService
        {
            private readonly HttpClient _httpClient;
            private readonly string _apiBaseUrl = "https://localhost:7241/api";

            public TourService(HttpClient httpClient)
            {
                _httpClient = httpClient;
            }

            public async Task<List<TourDto>> GetAllToursAsync()
            {
                var tours = await _httpClient.GetFromJsonAsync<List<TourDto>>($"{_apiBaseUrl}/Tour");
                return tours ?? new List<TourDto>();
            }
            public async Task<TourDto> GetTourByIdAsync(Guid id)
            {
                var tour = await _httpClient.GetFromJsonAsync<TourDto>($"{_apiBaseUrl}/Tour/{id}");
                return tour;
            }


    }
}
