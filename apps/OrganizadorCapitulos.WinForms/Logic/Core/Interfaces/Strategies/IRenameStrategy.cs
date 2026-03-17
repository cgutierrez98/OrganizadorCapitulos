namespace organizadorCapitulos.Core.Interfaces.Strategies
{
    public interface IRenameStrategy
    {
        string GetNewFileName(string originalFileName, organizadorCapitulos.Core.Entities.ChapterInfo info);
        void UpdateAfterRename(organizadorCapitulos.Core.Entities.ChapterInfo info);
        string GetDescription();
    }
}
