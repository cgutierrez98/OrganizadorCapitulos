using organizadorCapitulos.Core.Entities;
using organizadorCapitulos.Core.Interfaces.Strategies;

namespace organizadorCapitulos.Application.Strategies
{
    public class MaintainRenameStrategy : IRenameStrategy
    {
        public string GetNewFileName(string originalFileName, ChapterInfo info)
        {
            string extension = System.IO.Path.GetExtension(originalFileName);
            return info.GenerateFileName(extension);
        }

        public void UpdateAfterRename(ChapterInfo info)
        {
            // Lógica para incrementar contadores si es necesario
        }

        public string GetDescription() => "Mantener nombre original";
    }
}
