using System.Threading.Tasks;
using organizadorCapitulos.Core.Entities;
using organizadorCapitulos.Core.Interfaces.Services;

namespace organizadorCapitulos.Infrastructure.Services
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
