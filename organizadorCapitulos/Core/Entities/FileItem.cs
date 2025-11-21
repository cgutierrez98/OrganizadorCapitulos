using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace organizadorCapitulos.Core.Entities
{
    public class FileItem
    {
        public string FileName { get; set; } = string.Empty;
        public string FullPath { get; set; } = string.Empty;
        public bool IsSelected { get; set; }

        public FileItem(string fileName, string fullPath)
        {
            FileName = fileName;
            FullPath = fullPath;
        }
    }
}
