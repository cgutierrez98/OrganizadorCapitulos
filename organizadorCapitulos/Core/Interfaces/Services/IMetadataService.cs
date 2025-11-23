using System.Collections.Generic;
using System.Threading.Tasks;
using organizadorCapitulos.Core.Entities;

namespace organizadorCapitulos.Core.Interfaces.Services
{
    public interface IMetadataService
    {
        Task<List<SeriesSearchResult>> SearchSeriesAsync(string query);
        Task<string> GetEpisodeTitleAsync(int seriesId, int season, int episode);
        bool IsConfigured();
        void Configure(string apiKey);
    }
}
