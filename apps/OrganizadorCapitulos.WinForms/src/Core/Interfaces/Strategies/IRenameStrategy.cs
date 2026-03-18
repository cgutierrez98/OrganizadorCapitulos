using OrganizadorCapitulos.Core.Entities;

namespace OrganizadorCapitulos.Core.Interfaces.Strategies
{
    public interface IRenameStrategy
    {
        string GetNewFileName(string originalFileName, ChapterInfo info);
        void UpdateAfterRename(ChapterInfo info);
        string GetDescription();
    }
}
