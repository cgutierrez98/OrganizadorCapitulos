using Moq;
using organizadorCapitulos.Application.Commands;
using organizadorCapitulos.Core.Interfaces.Observers;
using organizadorCapitulos.Core.Interfaces.Repositories;

namespace organizadorCapitulos.Tests.Application.Commands
{
    public class MoveFilesCommandTests
    {
        private readonly Mock<IFileRepository> _mockFileRepository;
        private readonly Mock<IProgressObserver> _mockProgressObserver;

        public MoveFilesCommandTests()
        {
            _mockFileRepository = new Mock<IFileRepository>();
            _mockProgressObserver = new Mock<IProgressObserver>();
        }

        [Fact]
        public async Task ExecuteAsync_ShouldMoveAllFiles()
        {
            // Arrange
            var sourcePaths = new List<string>
            {
                "C:\\Source\\file1.mp4",
                "C:\\Source\\file2.mkv"
            };
            var destinationFolder = "C:\\Destination";

            _mockFileRepository.Setup(r => r.FileExists(It.IsAny<string>())).Returns(false);
            _mockFileRepository.Setup(r => r.MoveFileAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            var command = new MoveFilesCommand(
                _mockFileRepository.Object,
                _mockProgressObserver.Object,
                sourcePaths,
                destinationFolder);

            // Act
            await command.ExecuteAsync();

            // Assert
            _mockFileRepository.Verify(
                r => r.MoveFileAsync("C:\\Source\\file1.mp4", "C:\\Destination\\file1.mp4"),
                Times.Once);
            _mockFileRepository.Verify(
                r => r.MoveFileAsync("C:\\Source\\file2.mkv", "C:\\Destination\\file2.mkv"),
                Times.Once);
            Assert.True(command.CanUndo);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldDeleteExistingFiles_WhenDestinationExists()
        {
            // Arrange
            var sourcePaths = new List<string> { "C:\\Source\\file1.mp4" };
            var destinationFolder = "C:\\Destination";
            var destinationPath = "C:\\Destination\\file1.mp4";

            _mockFileRepository.Setup(r => r.FileExists(destinationPath)).Returns(true);
            _mockFileRepository.Setup(r => r.MoveFileAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            var command = new MoveFilesCommand(
                _mockFileRepository.Object,
                _mockProgressObserver.Object,
                sourcePaths,
                destinationFolder);

            // Act
            await command.ExecuteAsync();

            // Assert
            _mockFileRepository.Verify(r => r.DeleteFile(destinationPath), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldUpdateProgress_ForEachFile()
        {
            // Arrange
            var sourcePaths = new List<string>
            {
                "C:\\Source\\file1.mp4",
                "C:\\Source\\file2.mkv",
                "C:\\Source\\file3.avi"
            };
            var destinationFolder = "C:\\Destination";

            _mockFileRepository.Setup(r => r.FileExists(It.IsAny<string>())).Returns(false);
            _mockFileRepository.Setup(r => r.MoveFileAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            var command = new MoveFilesCommand(
                _mockFileRepository.Object,
                _mockProgressObserver.Object,
                sourcePaths,
                destinationFolder);

            // Act
            await command.ExecuteAsync();

            // Assert
            _mockProgressObserver.Verify(p => p.UpdateProgress(1, 3, "file1.mp4"), Times.Once);
            _mockProgressObserver.Verify(p => p.UpdateProgress(2, 3, "file2.mkv"), Times.Once);
            _mockProgressObserver.Verify(p => p.UpdateProgress(3, 3, "file3.avi"), Times.Once);
        }

        [Fact]
        public async Task UndoAsync_ShouldRevertAllMoves_WhenExecuted()
        {
            // Arrange
            var sourcePaths = new List<string>
            {
                "C:\\Source\\file1.mp4",
                "C:\\Source\\file2.mkv"
            };
            var destinationFolder = "C:\\Destination";

            _mockFileRepository.Setup(r => r.FileExists(It.IsAny<string>())).Returns(false);
            _mockFileRepository.Setup(r => r.MoveFileAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            var command = new MoveFilesCommand(
                _mockFileRepository.Object,
                _mockProgressObserver.Object,
                sourcePaths,
                destinationFolder);

            await command.ExecuteAsync();

            // Act
            await command.UndoAsync();

            // Assert
            _mockFileRepository.Verify(
                r => r.MoveFileAsync("C:\\Destination\\file1.mp4", "C:\\Source\\file1.mp4"),
                Times.Once);
            _mockFileRepository.Verify(
                r => r.MoveFileAsync("C:\\Destination\\file2.mkv", "C:\\Source\\file2.mkv"),
                Times.Once);
            Assert.False(command.CanUndo);
        }

        [Fact]
        public async Task UndoAsync_ShouldDoNothing_WhenNotExecuted()
        {
            // Arrange
            var sourcePaths = new List<string> { "C:\\Source\\file1.mp4" };
            var destinationFolder = "C:\\Destination";

            var command = new MoveFilesCommand(
                _mockFileRepository.Object,
                _mockProgressObserver.Object,
                sourcePaths,
                destinationFolder);

            // Act
            await command.UndoAsync();

            // Assert
            _mockFileRepository.Verify(r => r.MoveFileAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void Description_ShouldContainFileCountAndDestination()
        {
            // Arrange
            var sourcePaths = new List<string>
            {
                "C:\\Source\\file1.mp4",
                "C:\\Source\\file2.mkv"
            };
            var destinationFolder = "C:\\Destination";

            var command = new MoveFilesCommand(
                _mockFileRepository.Object,
                _mockProgressObserver.Object,
                sourcePaths,
                destinationFolder);

            // Act
            var description = command.Description;

            // Assert
            Assert.Contains("2", description);
            Assert.Contains("C:\\Destination", description);
        }
    }
}
