using System.Threading.Tasks;
using OrganizadorCapitulos.Core.Entities;
using OrganizadorCapitulos.Core.Interfaces.Services;

namespace OrganizadorCapitulos.Infrastructure.Services
{
    public class PythonAIService : IAIService
    {
        public bool IsAvailable() => false;

        public Task<ChapterInfo?> AnalyzeFilenameAsync(string filename)
        {
            // Placeholder: not available in this environment
            return Task.FromResult<ChapterInfo?>(null);
        }

        public Task<string> NormalizeTitleAsync(string title)
        {
            return Task.FromResult(title);
        }
    }
}
