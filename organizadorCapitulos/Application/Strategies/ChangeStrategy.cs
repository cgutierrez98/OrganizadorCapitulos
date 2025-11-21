using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using organizadorCapitulos.Core.Entities;
using organizadorCapitulos.Core.Interfaces.Strategies;

namespace organizadorCapitulos.Application.Strategies
{
    public class ChangeStrategy : IRenameStrategy
    {
        public void UpdateAfterRename(ChapterInfo chapterInfo)
        {
            // No hacer nada - el usuario cambiará manualmente
        }

        public string GetDescription()
        {
            return "Modo Cambiar - Mantiene el mismo número de capítulo";
        }

        string IRenameStrategy.GetNewFileName(string originalFileName, ChapterInfo chapterInfo)
        {
            throw new NotImplementedException();
        }
    }
}
