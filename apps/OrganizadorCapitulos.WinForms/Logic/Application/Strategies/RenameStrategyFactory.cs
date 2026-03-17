using organizadorCapitulos.Core.Enums;
using organizadorCapitulos.Core.Interfaces.Strategies;

namespace organizadorCapitulos.Application.Strategies
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
