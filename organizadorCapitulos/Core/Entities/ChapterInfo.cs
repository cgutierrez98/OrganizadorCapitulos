using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace organizadorCapitulos.Core.Entities
{
    public class ChapterInfo
    {
        public int Season { get; set; }
        public int Chapter { get; set; }
        public string Title { get; set; } = string.Empty;

        public void IncrementChapter() => Chapter++;

        public string GenerateFileName(string extension)
        {
            return $"{Title.Trim()} S{Season:D2}E{Chapter:D2}{extension}";
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
