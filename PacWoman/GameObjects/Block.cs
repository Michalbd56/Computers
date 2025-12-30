using GameEngine.Objects;
using Windows.Media.Devices;

namespace PacWoman.GameObjects
{
    public enum DirectionBlockType
    {
        horizontal, vertical,
    }
    
    public class Block : GameObject
    {
        public DirectionBlockType Direction { get; private set; }
        public Block(DirectionBlockType direction, double x, double y, double width) : base(string.Empty, x, y, width)
        {
            Direction = direction;
            switch(Direction)
            {
                case DirectionBlockType.horizontal:
                    SetName("Objects/block/horizontal.png");
                    //Image.Height = width * 15.75;
                    break;
                case DirectionBlockType.vertical:
                    SetName("Objects/block/vertical.png");
                    //Image.Height = width * 9.75;
                    break;

            }
            
        }
    }
    
}
