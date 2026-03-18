using System.Threading.Tasks;
using OrganizadorCapitulos.Core.Entities;

namespace OrganizadorCapitulos.Core.Interfaces.Services
{
    public interface IAIService
    {
        Task<ChapterInfo?> AnalyzeFilenameAsync(string filename);
        Task<string> NormalizeTitleAsync(string title);
        bool IsAvailable();
    }
}
