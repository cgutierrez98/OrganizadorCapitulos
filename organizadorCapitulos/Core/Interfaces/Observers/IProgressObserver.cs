using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace organizadorCapitulos.Core.Interfaces.Observers
{
    public interface IProgressObserver
    {
        void UpdateProgress(int current, int total, string filename);
        void UpdateStatus(string status);
    }
}
