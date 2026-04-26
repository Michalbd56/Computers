using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacWoman.Modles
{
   public class GameLevel
    {
        public int LevelId { get; set; } = 1;
        public int CountGhosts { get; set; } = 2;
        public int GhostsSpeed { get; set; } = 3;
    }
}
