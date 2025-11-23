using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using organizadorCapitulos.Core.Entities;
using organizadorCapitulos.Core.Interfaces.Observers;
using organizadorCapitulos.Core.Interfaces.Repositories;
using organizadorCapitulos.Core.Interfaces.Strategies;

namespace organizadorCapitulos.Application.Services
{
    public class FileOrganizerService
    {
        private readonly IFileRepository _fileRepository;
        private readonly IProgressObserver _progressObserver;

        public FileOrganizerService(IFileRepository fileRepository, IProgressObserver progressObserver)
        {
            _fileRepository = fileRepository;
            _progressObserver = progressObserver;
        }

        public async Task<List<string>> LoadVideoFilesAsync(IEnumerable<string> folders)
        {
            _progressObserver?.UpdateStatus("Buscando archivos de video...");
            return new List<string>(await _fileRepository.GetVideoFilesAsync(folders));
        }

        public async Task<string> RenameFileAsync(string sourcePath, ChapterInfo chapterInfo, IRenameStrategy strategy)
        {
            if (!chapterInfo.IsValid())
            {
                throw new ArgumentException("La información del capítulo no es válida");
            }

            string extension = System.IO.Path.GetExtension(sourcePath);
            string newFileName = chapterInfo.GenerateFileName(extension);
            string? directory = System.IO.Path.GetDirectoryName(sourcePath);
            if (string.IsNullOrEmpty(directory)) throw new InvalidOperationException("No se pudo obtener el directorio del archivo.");
            string destinationPath = System.IO.Path.Combine(directory, newFileName);

            if (_fileRepository.FileExists(destinationPath))
            {
                throw new InvalidOperationException($"Ya existe un archivo con el nombre: {newFileName}");
            }

            await _fileRepository.MoveFileAsync(sourcePath, destinationPath);
            strategy.UpdateAfterRename(chapterInfo);

            return destinationPath;
        }

        public async Task MoveFilesAsync(List<string> sourcePaths, string destinationFolder)
        {
            _progressObserver?.UpdateStatus("Moviendo archivos...");

            if (!System.IO.Directory.Exists(destinationFolder))
            {
                System.IO.Directory.CreateDirectory(destinationFolder);
            }

            int totalFiles = sourcePaths.Count;
            int processedFiles = 0;

            foreach (string sourcePath in sourcePaths)
            {
                processedFiles++;
                string fileName = System.IO.Path.GetFileName(sourcePath);
                string destinationPath = System.IO.Path.Combine(destinationFolder, fileName);

                _progressObserver?.UpdateProgress(processedFiles, totalFiles, fileName);

                if (_fileRepository.FileExists(destinationPath))
                {
                    _fileRepository.DeleteFile(destinationPath);
                }

                await _fileRepository.MoveFileAsync(sourcePath, destinationPath);
            }
        }
    }
}
