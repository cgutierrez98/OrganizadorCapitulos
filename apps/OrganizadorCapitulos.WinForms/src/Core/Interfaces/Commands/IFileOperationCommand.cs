using System.Threading.Tasks;

namespace OrganizadorCapitulos.Core.Interfaces.Commands
{
    public interface IFileOperationCommand
    {
        Task ExecuteAsync();
        Task UndoAsync();
        string Description { get; }
        bool CanUndo { get; }
    }
}
