using Moq;
using organizadorCapitulos.Application.Commands;
using organizadorCapitulos.Core.Interfaces.Repositories;

namespace organizadorCapitulos.Tests.Application.Commands
{
    public class RenameFileCommandTests
    {
        private readonly Mock<IFileRepository> _mockFileRepository;

        public RenameFileCommandTests()
        {
            _mockFileRepository = new Mock<IFileRepository>();
        }

        [Fact]
        public async Task ExecuteAsync_ShouldRenameFile_WhenDestinationDoesNotExist()
        {
            // Arrange
            var sourcePath = "C:\\Videos\\old.mp4";
            var destinationPath = "C:\\Videos\\new.mp4";
            _mockFileRepository.Setup(r => r.FileExists(destinationPath)).Returns(false);
            _mockFileRepository.Setup(r => r.MoveFileAsync(sourcePath, destinationPath))
                .Returns(Task.CompletedTask);

            var command = new RenameFileCommand(_mockFileRepository.Object, sourcePath, destinationPath);

            // Act
            await command.ExecuteAsync();

            // Assert
            _mockFileRepository.Verify(r => r.MoveFileAsync(sourcePath, destinationPath), Times.Once);
            Assert.True(command.CanUndo);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldThrowException_WhenDestinationExists()
        {
            // Arrange
            var sourcePath = "C:\\Videos\\old.mp4";
            var destinationPath = "C:\\Videos\\new.mp4";
            _mockFileRepository.Setup(r => r.FileExists(destinationPath)).Returns(true);

            var command = new RenameFileCommand(_mockFileRepository.Object, sourcePath, destinationPath);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => command.ExecuteAsync());
            _mockFileRepository.Verify(r => r.MoveFileAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task UndoAsync_ShouldRevertRename_WhenExecuted()
        {
            // Arrange
            var sourcePath = "C:\\Videos\\old.mp4";
            var destinationPath = "C:\\Videos\\new.mp4";
            _mockFileRepository.Setup(r => r.FileExists(destinationPath)).Returns(false);
            _mockFileRepository.Setup(r => r.MoveFileAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            var command = new RenameFileCommand(_mockFileRepository.Object, sourcePath, destinationPath);
            await command.ExecuteAsync();

            // Act
            await command.UndoAsync();

            // Assert
            _mockFileRepository.Verify(r => r.MoveFileAsync(destinationPath, sourcePath), Times.Once);
            Assert.False(command.CanUndo);
        }

        [Fact]
        public async Task UndoAsync_ShouldDoNothing_WhenNotExecuted()
        {
            // Arrange
            var sourcePath = "C:\\Videos\\old.mp4";
            var destinationPath = "C:\\Videos\\new.mp4";
            var command = new RenameFileCommand(_mockFileRepository.Object, sourcePath, destinationPath);

            // Act
            await command.UndoAsync();

            // Assert
            _mockFileRepository.Verify(r => r.MoveFileAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void Description_ShouldContainFileNames()
        {
            // Arrange
            var sourcePath = "C:\\Videos\\old_video.mp4";
            var destinationPath = "C:\\Videos\\new_video.mp4";
            var command = new RenameFileCommand(_mockFileRepository.Object, sourcePath, destinationPath);

            // Act
            var description = command.Description;

            // Assert
            Assert.Contains("old_video.mp4", description);
            Assert.Contains("new_video.mp4", description);
        }
    }
}
