using organizadorCapitulos.Core.Entities;

namespace organizadorCapitulos.Tests.Core.Entities
{
    public class ChapterInfoTests
    {
        [Fact]
        public void IncrementChapter_ShouldIncreaseChapterByOne()
        {
            // Arrange
            var chapterInfo = new ChapterInfo
            {
                Season = 1,
                Chapter = 5,
                Title = "Test Series"
            };

            // Act
            chapterInfo.IncrementChapter();

            // Assert
            Assert.Equal(6, chapterInfo.Chapter);
        }

        [Fact]
        public void GenerateFileName_ShouldFormatCorrectly()
        {
            // Arrange
            var chapterInfo = new ChapterInfo
            {
                Season = 2,
                Chapter = 15,
                Title = "My Series"
            };

            // Act
            string fileName = chapterInfo.GenerateFileName(".mkv");

            // Assert
            Assert.Equal("My Series S02E15.mkv", fileName);
        }

        [Fact]
        public void GenerateFileName_ShouldPadNumbersWithZeros()
        {
            // Arrange
            var chapterInfo = new ChapterInfo
            {
                Season = 1,
                Chapter = 3,
                Title = "Test"
            };

            // Act
            string fileName = chapterInfo.GenerateFileName(".mp4");

            // Assert
            Assert.Equal("Test S01E03.mp4", fileName);
        }

        [Fact]
        public void GenerateFileName_ShouldTrimTitle()
        {
            // Arrange
            var chapterInfo = new ChapterInfo
            {
                Season = 1,
                Chapter = 1,
                Title = "  Spaced Title  "
            };

            // Act
            string fileName = chapterInfo.GenerateFileName(".avi");

            // Assert
            Assert.Equal("Spaced Title S01E01.avi", fileName);
        }

        [Theory]
        [InlineData(1, 1, "Valid Title", true)]
        [InlineData(0, 1, "Valid Title", false)] // Season = 0
        [InlineData(1, 0, "Valid Title", false)] // Chapter = 0
        [InlineData(1, 1, "", false)] // Empty title
        [InlineData(1, 1, "   ", false)] // Whitespace title
        [InlineData(1, 1, "Invalid<>Title", false)] // Invalid characters
        public void IsValid_ShouldValidateCorrectly(int season, int chapter, string title, bool expected)
        {
            // Arrange
            var chapterInfo = new ChapterInfo
            {
                Season = season,
                Chapter = chapter,
                Title = title
            };

            // Act
            bool result = chapterInfo.IsValid();

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void IsValid_ShouldReturnFalse_WhenTitleContainsInvalidPathCharacters()
        {
            // Arrange
            var invalidChars = new[] { '/', '\\', ':', '*', '?', '"', '<', '>', '|' };

            foreach (var invalidChar in invalidChars)
            {
                var chapterInfo = new ChapterInfo
                {
                    Season = 1,
                    Chapter = 1,
                    Title = $"Invalid{invalidChar}Title"
                };

                // Act
                bool result = chapterInfo.IsValid();

                // Assert
                Assert.False(result, $"Should be invalid with character: {invalidChar}");
            }
        }

        [Fact]
        public void GenerateFileName_WithEpisodeTitle_ShouldFormatCorrectly()
        {
            // Arrange
            var chapterInfo = new ChapterInfo
            {
                Season = 2,
                Chapter = 15,
                Title = "My Series",
                EpisodeTitle = "The Great Adventure"
            };

            // Act
            string fileName = chapterInfo.GenerateFileName(".mkv");

            // Assert
            Assert.Equal("My Series The Great Adventure S02E15.mkv", fileName);
        }

        [Fact]
        public void GenerateFileName_WithEpisodeTitle_ShouldTrimBothTitles()
        {
            // Arrange
            var chapterInfo = new ChapterInfo
            {
                Season = 1,
                Chapter = 1,
                Title = "  My Series  ",
                EpisodeTitle = "  Episode Title  "
            };

            // Act
            string fileName = chapterInfo.GenerateFileName(".mp4");

            // Assert
            Assert.Equal("My Series Episode Title S01E01.mp4", fileName);
        }
    }
}
