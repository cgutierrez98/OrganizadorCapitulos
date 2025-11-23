using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;
using organizadorCapitulos.Core.Entities;
using organizadorCapitulos.Core.Interfaces.Services;

namespace organizadorCapitulos.Infrastructure.Services
{
    public class TmdbMetadataService : IMetadataService
    {
        private readonly HttpClient _httpClient;
        private string _apiKey;
        private const string BaseUrl = "https://api.themoviedb.org/3";

        public TmdbMetadataService()
        {
            _httpClient = new HttpClient();
        }

        public void Configure(string apiKey)
        {
            _apiKey = apiKey;
        }

        public bool IsConfigured()
        {
            return !string.IsNullOrEmpty(_apiKey);
        }

        public async Task<List<SeriesSearchResult>> SearchSeriesAsync(string query)
        {
            if (!IsConfigured()) throw new InvalidOperationException("API Key not configured.");

            string url = $"{BaseUrl}/search/tv?api_key={_apiKey}&query={Uri.EscapeDataString(query)}&language=es-ES";

            try
            {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                using (JsonDocument doc = JsonDocument.Parse(json))
                {
                    var results = new List<SeriesSearchResult>();
                    if (doc.RootElement.TryGetProperty("results", out JsonElement resultsElement))
                    {
                        foreach (var element in resultsElement.EnumerateArray())
                        {
                            results.Add(new SeriesSearchResult
                            {
                                Id = element.GetProperty("id").GetInt32(),
                                Name = element.TryGetProperty("name", out var name) ? name.GetString() : "Unknown",
                                OriginalName = element.TryGetProperty("original_name", out var originalName) ? originalName.GetString() : "Unknown",
                                FirstAirDate = element.TryGetProperty("first_air_date", out var date) ? date.GetString() : "",
                                Overview = element.TryGetProperty("overview", out var overview) ? overview.GetString() : ""
                            });
                        }
                    }
                    return results;
                }
            }
            catch (Exception ex)
            {
                // Log error?
                Console.WriteLine($"Error searching series: {ex.Message}");
                return new List<SeriesSearchResult>();
            }
        }

        public async Task<string> GetEpisodeTitleAsync(int seriesId, int season, int episode)
        {
            if (!IsConfigured()) throw new InvalidOperationException("API Key not configured.");

            string url = $"{BaseUrl}/tv/{seriesId}/season/{season}/episode/{episode}?api_key={_apiKey}&language=es-ES";

            try
            {
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode) return null;

                var json = await response.Content.ReadAsStringAsync();
                using (JsonDocument doc = JsonDocument.Parse(json))
                {
                    if (doc.RootElement.TryGetProperty("name", out JsonElement nameElement))
                    {
                        return nameElement.GetString();
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
