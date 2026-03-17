using System.Threading.Tasks;
using organizadorCapitulos.Core.Entities;

namespace organizadorCapitulos.Core.Interfaces.Services
{
    public interface IAIService
    {
        Task<ChapterInfo?> AnalyzeFilenameAsync(string filename);
        Task<string> NormalizeTitleAsync(string title);
        bool IsAvailable();
    }
}
