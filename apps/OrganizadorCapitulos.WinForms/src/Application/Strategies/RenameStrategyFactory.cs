using OrganizadorCapitulos.Core.Enums;
using OrganizadorCapitulos.Core.Interfaces.Strategies;

namespace OrganizadorCapitulos.Application.Strategies
{
    public class RenameStrategyFactory
    {
        public IRenameStrategy CreateStrategy(RenameMode mode)
        {
            return mode switch
            {
                RenameMode.Maintain => new MaintainRenameStrategy(),
                RenameMode.Change => new ChangeStrategy(),
                _ => new ChangeStrategy()
            };
        }
    }
}
