using Moq;
using organizadorCapitulos.Application.Services;
using organizadorCapitulos.Core.Entities;
using organizadorCapitulos.Core.Interfaces.Observers;
using organizadorCapitulos.Core.Interfaces.Repositories;
using organizadorCapitulos.Core.Interfaces.Strategies;

namespace organizadorCapitulos.Tests.Application.Services
{
    public class FileOrganizerServiceTests
    {
        private readonly Mock<IFileRepository> _mockFileRepository;
        private readonly Mock<IProgressObserver> _mockProgressObserver;
        private readonly FileOrganizerService _service;

        public FileOrganizerServiceTests()
        {
            _mockFileRepository = new Mock<IFileRepository>();
            _mockProgressObserver = new Mock<IProgressObserver>();
            _service = new FileOrganizerService(_mockFileRepository.Object, _mockProgressObserver.Object);
        }

        [Fact]
        public async Task LoadVideoFilesAsync_ShouldReturnVideoFiles()
        {
            // Arrange
            var folders = new List<string> { "C:\\Videos" };
            var expectedFiles = new List<string> { "video1.mp4", "video2.mkv" };
            _mockFileRepository.Setup(r => r.GetVideoFilesAsync(folders))
                .ReturnsAsync(expectedFiles);

            // Act
            var result = await _service.LoadVideoFilesAsync(folders);

            // Assert
            Assert.Equal(expectedFiles.Count, result.Count);
            Assert.Equal(expectedFiles, result);
            _mockProgressObserver.Verify(p => p.UpdateStatus("Buscando archivos de video..."), Times.Once);
        }

        [Fact]
        public async Task RenameFileAsync_ShouldThrowException_WhenChapterInfoIsInvalid()
        {
            // Arrange
            var chapterInfo = new ChapterInfo
            {
                Season = 0, // Invalid
                Chapter = 1,
                Title = "Test"
            };
            var mockStrategy = new Mock<IRenameStrategy>();

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() =>
                _service.RenameFileAsync("C:\\test.mp4", chapterInfo, mockStrategy.Object));
        }

        [Fact]
        public async Task RenameFileAsync_ShouldThrowException_WhenDestinationFileExists()
        {
            // Arrange
            var sourcePath = "C:\\Videos\\old.mp4";
            var chapterInfo = new ChapterInfo
            {
                Season = 1,
                Chapter = 1,
                Title = "Test Series"
            };
            var mockStrategy = new Mock<IRenameStrategy>();
            var expectedNewPath = "C:\\Videos\\Test Series S01E01.mp4";

            _mockFileRepository.Setup(r => r.FileExists(expectedNewPath))
                .Returns(true);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _service.RenameFileAsync(sourcePath, chapterInfo, mockStrategy.Object));
        }

        [Fact]
        public async Task RenameFileAsync_ShouldRenameFile_WhenValid()
        {
            // Arrange
            var sourcePath = "C:\\Videos\\old.mp4";
            var chapterInfo = new ChapterInfo
            {
                Season = 1,
                Chapter = 1,
                Title = "Test Series"
            };
            var mockStrategy = new Mock<IRenameStrategy>();
            var expectedNewPath = "C:\\Videos\\Test Series S01E01.mp4";

            _mockFileRepository.Setup(r => r.FileExists(It.IsAny<string>()))
                .Returns(false);
            _mockFileRepository.Setup(r => r.MoveFileAsync(sourcePath, expectedNewPath))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _service.RenameFileAsync(sourcePath, chapterInfo, mockStrategy.Object);

            // Assert
            Assert.Equal(expectedNewPath, result);
            _mockFileRepository.Verify(r => r.MoveFileAsync(sourcePath, expectedNewPath), Times.Once);
            mockStrategy.Verify(s => s.UpdateAfterRename(chapterInfo), Times.Once);
        }

        [Fact]
        public async Task MoveFilesAsync_ShouldCreateDestinationFolder_WhenNotExists()
        {
            // Arrange
            var sourcePaths = new List<string> { "C:\\Source\\file1.mp4" };
            var destinationFolder = "C:\\Destination";

            _mockFileRepository.Setup(r => r.FileExists(It.IsAny<string>()))
                .Returns(false);
            _mockFileRepository.Setup(r => r.MoveFileAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            // Act
            await _service.MoveFilesAsync(sourcePaths, destinationFolder);

            // Assert
            _mockProgressObserver.Verify(p => p.UpdateStatus("Moviendo archivos..."), Times.Once);
            _mockProgressObserver.Verify(p => p.UpdateProgress(1, 1, "file1.mp4"), Times.Once);
        }

        [Fact]
        public async Task MoveFilesAsync_ShouldDeleteExistingFile_WhenDestinationExists()
        {
            // Arrange
            var sourcePaths = new List<string> { "C:\\Source\\file1.mp4" };
            var destinationFolder = "C:\\Destination";
            var destinationPath = "C:\\Destination\\file1.mp4";

            _mockFileRepository.Setup(r => r.FileExists(destinationPath))
                .Returns(true);
            _mockFileRepository.Setup(r => r.MoveFileAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            // Act
            await _service.MoveFilesAsync(sourcePaths, destinationFolder);

            // Assert
            _mockFileRepository.Verify(r => r.DeleteFile(destinationPath), Times.Once);
            _mockFileRepository.Verify(r => r.MoveFileAsync(sourcePaths[0], destinationPath), Times.Once);
        }

        [Fact]
        public async Task MoveFilesAsync_ShouldUpdateProgress_ForEachFile()
        {
            // Arrange
            var sourcePaths = new List<string>
            {
                "C:\\Source\\file1.mp4",
                "C:\\Source\\file2.mkv",
                "C:\\Source\\file3.avi"
            };
            var destinationFolder = "C:\\Destination";

            _mockFileRepository.Setup(r => r.FileExists(It.IsAny<string>()))
                .Returns(false);
            _mockFileRepository.Setup(r => r.MoveFileAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            // Act
            await _service.MoveFilesAsync(sourcePaths, destinationFolder);

            // Assert
            _mockProgressObserver.Verify(p => p.UpdateProgress(1, 3, "file1.mp4"), Times.Once);
            _mockProgressObserver.Verify(p => p.UpdateProgress(2, 3, "file2.mkv"), Times.Once);
            _mockProgressObserver.Verify(p => p.UpdateProgress(3, 3, "file3.avi"), Times.Once);
        }
    }
}
