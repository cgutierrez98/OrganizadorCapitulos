using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace organizadorCapitulos.Core.Exceptions
{
    public class DirectoryAccessException : Exception
    {
        public string DirectoryPath { get; }

        public DirectoryAccessException(string message, string directoryPath) : base(message)
        {
            DirectoryPath = directoryPath;
        }

        public DirectoryAccessException(string message, string directoryPath, Exception innerException)
            : base(message, innerException)
        {
            DirectoryPath = directoryPath;
        }
    }
}
