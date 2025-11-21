using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using organizadorCapitulos.Core.Entities;

namespace organizadorCapitulos.Core.Interfaces.Strategies
{
    public interface IRenameStrategy
    {
       
        string GetNewFileName(string originalFileName, ChapterInfo chapterInfo);

        void UpdateAfterRename(ChapterInfo chapterInfo);
        string GetDescription();
    }
}
