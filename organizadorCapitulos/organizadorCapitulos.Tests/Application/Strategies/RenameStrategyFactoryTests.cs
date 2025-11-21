using organizadorCapitulos.Application.Strategies;
using organizadorCapitulos.Core.Enums;

namespace organizadorCapitulos.Tests.Application.Strategies
{
    public class RenameStrategyFactoryTests
    {
        [Fact]
        public void CreateStrategy_ShouldReturnMaintainRenameStrategy_WhenModeMaintain()
        {
            // Arrange
            var factory = new RenameStrategyFactory();

            // Act
            var strategy = factory.CreateStrategy(RenameMode.Maintain);

            // Assert
            Assert.IsType<MaintainRenameStrategy>(strategy);
        }

        [Fact]
        public void CreateStrategy_ShouldReturnChangeStrategy_WhenModeChange()
        {
            // Arrange
            var factory = new RenameStrategyFactory();

            // Act
            var strategy = factory.CreateStrategy(RenameMode.Change);

            // Assert
            Assert.IsType<ChangeStrategy>(strategy);
        }

        [Fact]
        public void CreateStrategy_ShouldReturnChangeStrategy_WhenInvalidMode()
        {
            // Arrange
            var factory = new RenameStrategyFactory();
            var invalidMode = (RenameMode)999;

            // Act
            var strategy = factory.CreateStrategy(invalidMode);

            // Assert
            // Based on current implementation, it returns ChangeStrategy for invalid modes
            Assert.IsType<ChangeStrategy>(strategy);
        }
    }
}
