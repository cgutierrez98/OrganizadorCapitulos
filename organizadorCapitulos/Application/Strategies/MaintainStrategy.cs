using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using organizadorCapitulos.Core.Entities;
using organizadorCapitulos.Core.Interfaces.Strategies;

namespace organizadorCapitulos.Application.Strategies
{
    public class MaintainRenameStrategy : IRenameStrategy
    {
        public string GetNewFileName(string originalFileName, ChapterInfo info)
        {
            // Aquí va tu lógica compleja para buscar números viejos y reemplazarlos
            // Ejemplo simple:
            return $"{info.Title} - S{info.Season:D2}E{info.Chapter:D2}";
        }

        public void UpdateAfterRename(ChapterInfo info)
        {
            // Lógica para incrementar contadores si es necesario
        }

        public string GetDescription() => "Mantener nombre original";
    }
}
