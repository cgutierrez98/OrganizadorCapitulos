using System.Collections.Generic;
using System.Threading.Tasks;
using OrganizadorCapitulos.Core.Entities;
using OrganizadorCapitulos.Core.Interfaces.Services;

namespace OrganizadorCapitulos.Infrastructure.Services
{
    public class TmdbMetadataService : IMetadataService
    {
        public void Configure(string apiKey)
        {
            // No-op placeholder
        }

        public Task<string?> GetEpisodeTitleAsync(int seriesId, int season, int episode)
        {
            return Task.FromResult<string?>(null);
        }

        public Task<(int season, int episode, string title)?> FindEpisodeByTitleAsync(int seriesId, string episodeTitle)
        {
            return Task.FromResult<(int, int, string)?>(null);
        }

        public Task<List<SeriesSearchResult>> SearchSeriesAsync(string query)
        {
            return Task.FromResult(new List<SeriesSearchResult>());
        }

        public bool IsConfigured() => false;
    }
}
