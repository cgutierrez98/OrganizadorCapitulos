using System.IO;

namespace organizadorCapitulos.Core.Entities
{
    public class ChapterInfo
    {
        public int Season { get; set; }
        public int Chapter { get; set; }
        public string Title { get; set; } = string.Empty;
        public string EpisodeTitle { get; set; } = string.Empty;

        public void IncrementChapter() => Chapter++;

        public string GenerateFileName(string extension)
        {
            if (!string.IsNullOrWhiteSpace(EpisodeTitle))
            {
                return $"{Title.Trim()} {EpisodeTitle.Trim()} S{Season:D2}E{Chapter:D2}{extension}";
            }
            else
            {
                return $"{Title.Trim()} - S{Season:D2}E{Chapter:D2}{extension}";
            }
        }

        public bool IsValid()
        {
            return Season > 0 &&
                   Chapter > 0 &&
                   !string.IsNullOrWhiteSpace(Title) &&
                   Title.IndexOfAny(Path.GetInvalidFileNameChars()) < 0;
        }
    }
}
