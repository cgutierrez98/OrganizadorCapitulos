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
        private string? _apiKey;
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
                using JsonDocument doc = JsonDocument.Parse(json);

                var results = new List<SeriesSearchResult>();
                if (doc.RootElement.TryGetProperty("results", out JsonElement resultsElement))
                {
                    foreach (var element in resultsElement.EnumerateArray())
                    {
                        results.Add(new SeriesSearchResult
                        {
                            Id = element.GetProperty("id").GetInt32(),
                            Name = element.TryGetProperty("name", out var name) ? (name.GetString() ?? "Unknown") : "Unknown",
                            OriginalName = element.TryGetProperty("original_name", out var originalName) ? (originalName.GetString() ?? "Unknown") : "Unknown",
                            FirstAirDate = element.TryGetProperty("first_air_date", out var date) ? (date.GetString() ?? "") : "",
                            Overview = element.TryGetProperty("overview", out var overview) ? (overview.GetString() ?? "") : ""
                        });
                    }
                }
                return results;
            }
            catch (Exception ex)
            {
                // Log error?
                Console.WriteLine($"Error searching series: {ex.Message}");
                return new List<SeriesSearchResult>();
            }
        }

        public async Task<string?> GetEpisodeTitleAsync(int seriesId, int season, int episode)
        {
            if (!IsConfigured()) throw new InvalidOperationException("API Key not configured.");

            string url = $"{BaseUrl}/tv/{seriesId}/season/{season}/episode/{episode}?api_key={_apiKey}&language=es-ES";

            try
            {
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode) return null;

                var json = await response.Content.ReadAsStringAsync();
                using JsonDocument doc = JsonDocument.Parse(json);

                if (doc.RootElement.TryGetProperty("name", out JsonElement nameElement))
                {
                    return nameElement.GetString();
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<(int season, int episode, string title)?> FindEpisodeByTitleAsync(int seriesId, string episodeTitle)
        {
            if (!IsConfigured()) throw new InvalidOperationException("API Key not configured.");
            if (string.IsNullOrWhiteSpace(episodeTitle)) return null;

            // Normalize the search title
            string searchTitle = episodeTitle.Trim().ToLowerInvariant();

            try
            {
                // First, get the series info to know how many seasons there are
                string seriesUrl = $"{BaseUrl}/tv/{seriesId}?api_key={_apiKey}&language=es-ES";
                var seriesResponse = await _httpClient.GetAsync(seriesUrl);
                if (!seriesResponse.IsSuccessStatusCode) return null;

                var seriesJson = await seriesResponse.Content.ReadAsStringAsync();
                int numberOfSeasons = 1;

                using JsonDocument seriesDoc = JsonDocument.Parse(seriesJson);

                if (seriesDoc.RootElement.TryGetProperty("number_of_seasons", out JsonElement seasonsElement))
                {
                    numberOfSeasons = seasonsElement.GetInt32();
                }

                // Search through each season
                for (int season = 1; season <= numberOfSeasons; season++)
                {
                    string seasonUrl = $"{BaseUrl}/tv/{seriesId}/season/{season}?api_key={_apiKey}&language=es-ES";
                    var seasonResponse = await _httpClient.GetAsync(seasonUrl);
                    if (!seasonResponse.IsSuccessStatusCode) continue;

                    var seasonJson = await seasonResponse.Content.ReadAsStringAsync();
                    using JsonDocument seasonDoc = JsonDocument.Parse(seasonJson);

                    if (seasonDoc.RootElement.TryGetProperty("episodes", out JsonElement episodesElement))
                    {
                        foreach (var ep in episodesElement.EnumerateArray())
                        {
                            if (ep.TryGetProperty("name", out JsonElement nameElement))
                            {
                                string? epName = nameElement.GetString();
                                if (epName != null &&
                                    (epName.Contains(searchTitle, StringComparison.OrdinalIgnoreCase) ||
                                     searchTitle.Contains(epName, StringComparison.OrdinalIgnoreCase)))
                                {
                                    int epNumber = ep.GetProperty("episode_number").GetInt32();
                                    return (season, epNumber, epName);
                                }
                            }
                        }
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
