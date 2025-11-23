using organizadorCapitulos.Core.Entities;
using organizadorCapitulos.Core.Interfaces.Observers;
using organizadorCapitulos.Core.Interfaces.Repositories;
using organizadorCapitulos.Core.Interfaces.Strategies;

namespace organizadorCapitulos.Application.Services
{
    public class FileOrganizerService(IFileRepository fileRepository, IProgressObserver progressObserver)
    {
        public async Task<List<string>> LoadVideoFilesAsync(IEnumerable<string> folders)
        {
            progressObserver?.UpdateStatus("Buscando archivos de video...");
            return [.. await fileRepository.GetVideoFilesAsync(folders)];
        }

        public async Task<string> RenameFileAsync(string sourcePath, ChapterInfo chapterInfo, IRenameStrategy strategy)
        {
            if (!chapterInfo.IsValid())
            {
                throw new ArgumentException("La información del capítulo no es válida");
            }

            string extension = Path.GetExtension(sourcePath);
            string newFileName = chapterInfo.GenerateFileName(extension);
            string? directory = Path.GetDirectoryName(sourcePath);
            if (string.IsNullOrEmpty(directory)) throw new InvalidOperationException("No se pudo obtener el directorio del archivo.");
            string destinationPath = Path.Combine(directory, newFileName);

            if (fileRepository.FileExists(destinationPath))
            {
                throw new InvalidOperationException($"Ya existe un archivo con el nombre: {newFileName}");
            }

            await fileRepository.MoveFileAsync(sourcePath, destinationPath);
            strategy.UpdateAfterRename(chapterInfo);

            return destinationPath;
        }

        public async Task MoveFilesAsync(List<string> sourcePaths, string destinationFolder)
        {
            progressObserver?.UpdateStatus("Moviendo archivos...");

            if (!Directory.Exists(destinationFolder))
            {
                Directory.CreateDirectory(destinationFolder);
            }

            int totalFiles = sourcePaths.Count;
            int processedFiles = 0;

            foreach (string sourcePath in sourcePaths)
            {
                processedFiles++;
                string fileName = Path.GetFileName(sourcePath);
                string destinationPath = Path.Combine(destinationFolder, fileName);

                progressObserver?.UpdateProgress(processedFiles, totalFiles, fileName);

                if (fileRepository.FileExists(destinationPath))
                {
                    fileRepository.DeleteFile(destinationPath);
                }

                await fileRepository.MoveFileAsync(sourcePath, destinationPath);
            }
        }
    }
}
