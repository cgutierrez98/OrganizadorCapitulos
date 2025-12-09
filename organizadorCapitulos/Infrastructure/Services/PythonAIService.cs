using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using organizadorCapitulos.Core.Entities;
using organizadorCapitulos.Core.Interfaces.Services;

namespace organizadorCapitulos.Infrastructure.Services
{
    public class PythonAIService : IAIService
    {
        private readonly string _pythonScriptPath;
        private readonly string _pythonExecutable;

        public PythonAIService()
        {
            // Assuming the script is in a 'Python' folder relative to the executable or project
            // Adjust path logic as needed for dev vs prod
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string projectDir = Path.GetFullPath(Path.Combine(baseDir, @"..\..\..\")); // Dev environment

            // Check for bundled exe first (Production)
            string bundledExe = Path.Combine(baseDir, "ai_service.exe");
            if (File.Exists(bundledExe))
            {
                _pythonExecutable = bundledExe;
                _pythonScriptPath = ""; // Not needed for exe
            }
            else
            {
                // Fallback to script (Development)
                _pythonExecutable = "python"; // Assume in PATH
                _pythonScriptPath = Path.Combine(projectDir, "Python", "ai_service.py");

                if (!File.Exists(_pythonScriptPath))
                {
                    // Try looking in the same folder as the executable (if manually placed)
                    _pythonScriptPath = Path.Combine(baseDir, "Python", "ai_service.py");
                }
            }
        }

        public bool IsAvailable()
        {
            // Simple check: can we start the process?
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = _pythonExecutable,
                    Arguments = string.IsNullOrEmpty(_pythonScriptPath) ? "--help" : $"{_pythonScriptPath} --help",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                using (var process = Process.Start(psi))
                {
                    if (process != null)
                    {
                        process.WaitForExit();
                        return process.ExitCode == 0;
                    }
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public async Task<ChapterInfo?> AnalyzeFilenameAsync(string filename)
        {
            var json = await RunPythonCommand("analyze", filename);
            if (string.IsNullOrEmpty(json)) return null;

            try
            {
                using (JsonDocument doc = JsonDocument.Parse(json))
                {
                    var root = doc.RootElement;
                    if (root.TryGetProperty("error", out _)) return null;

                    return new ChapterInfo
                    {
                        Season = root.GetProperty("season").GetInt32(),
                        Chapter = root.GetProperty("episode").GetInt32(),
                        Title = root.GetProperty("series").GetString() ?? string.Empty,
                        EpisodeTitle = root.GetProperty("title").GetString() ?? string.Empty
                    };
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<string> NormalizeTitleAsync(string title)
        {
            var json = await RunPythonCommand("normalize", title);
            if (string.IsNullOrEmpty(json)) return title;

            try
            {
                using (JsonDocument doc = JsonDocument.Parse(json))
                {
                    var root = doc.RootElement;
                    if (root.TryGetProperty("result", out var result))
                    {
                        return result.GetString() ?? title;
                    }
                }
            }
            catch { }
            return title;
        }

        private async Task<string?> RunPythonCommand(string command, string input)
        {
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = _pythonExecutable,
                    Arguments = string.IsNullOrEmpty(_pythonScriptPath)
                        ? $"{command} --input \"{input}\""
                        : $"\"{_pythonScriptPath}\" {command} --input \"{input}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (var process = new Process { StartInfo = psi })
                {
                    process.Start();
                    string output = await process.StandardOutput.ReadToEndAsync();
                    string error = await process.StandardError.ReadToEndAsync();
                    process.WaitForExit();

                    if (!string.IsNullOrEmpty(error))
                    {
                        // Log error if needed
                        // Console.WriteLine(error);
                    }

                    return output;
                }
            }
            catch (Exception)
            {
                // Handle exception
                return null;
            }
        }
    }
}
