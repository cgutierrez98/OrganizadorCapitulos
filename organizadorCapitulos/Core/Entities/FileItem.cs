using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace organizadorCapitulos.Core.Entities
{
    public class FileItem(string fileName, string fullPath)
    {
        public string FileName { get; set; } = fileName;
        public string FullPath { get; set; } = fullPath;
        public bool IsSelected { get; set; }
    }
}
