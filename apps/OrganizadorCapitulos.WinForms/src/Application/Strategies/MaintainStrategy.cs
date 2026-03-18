using OrganizadorCapitulos.Core.Entities;
using OrganizadorCapitulos.Core.Interfaces.Strategies;

namespace OrganizadorCapitulos.Application.Strategies
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
