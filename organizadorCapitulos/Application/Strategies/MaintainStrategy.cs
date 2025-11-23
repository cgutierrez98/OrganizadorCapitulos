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
            if (!string.IsNullOrWhiteSpace(info.EpisodeTitle))
            {
                // Formato con título de episodio: "NombreSerie Titulo S##E##"
                return $"{info.Title} {info.EpisodeTitle} S{info.Season:D2}E{info.Chapter:D2}";
            }
            else
            {
                // Formato sin título de episodio: "NombreSerie - S##E##"
                return $"{info.Title} - S{info.Season:D2}E{info.Chapter:D2}";
            }
        }

        public void UpdateAfterRename(ChapterInfo info)
        {
            // Lógica para incrementar contadores si es necesario
        }

        public string GetDescription() => "Mantener nombre original";
    }
}
