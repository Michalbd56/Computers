using GameEngine.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacWoman.GameObjects
{
    public class Coin : GameObject
    {
        public Coin(string fileName, double x, double y, double size) : base(fileName, x, y, size)
        {

        }
    }
}
