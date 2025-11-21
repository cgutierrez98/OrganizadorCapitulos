using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace organizadorCapitulos.Core.Exceptions
{
    public class FileOperationException : Exception
    {
        public string FilePath { get; }

        public FileOperationException(string message, string filePath) : base(message)
        {
            FilePath = filePath;
        }

        public FileOperationException(string message, string filePath, Exception innerException)
            : base(message, innerException)
        {
            FilePath = filePath;
        }
    }
}
