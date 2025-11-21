using organizadorCapitulos.Application.Strategies;
using organizadorCapitulos.Core.Entities;

namespace organizadorCapitulos.Tests.Application.Strategies
{
    public class ChangeStrategyTests
    {
        [Fact]
        public void UpdateAfterRename_ShouldNotModifyChapterInfo()
        {
            // Arrange
            var strategy = new ChangeStrategy();
            var chapterInfo = new ChapterInfo
            {
                Season = 1,
                Chapter = 5,
                Title = "Test Series"
            };
            var originalChapter = chapterInfo.Chapter;

            // Act
            strategy.UpdateAfterRename(chapterInfo);

            // Assert
            Assert.Equal(originalChapter, chapterInfo.Chapter);
        }

        [Fact]
        public void GetDescription_ShouldReturnCorrectDescription()
        {
            // Arrange
            var strategy = new ChangeStrategy();

            // Act
            var description = strategy.GetDescription();

            // Assert
            Assert.Equal("Modo Cambiar - Mantiene el mismo número de capítulo", description);
        }
    }
}
