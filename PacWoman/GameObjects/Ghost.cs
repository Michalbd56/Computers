using GameEngine.Objects;
using GameEngine.Services;
using PacWoman.GameServices;
using System;
using System.Threading.Tasks;

namespace PacWoman.GameObjects
{

    public enum GhostColor
    {
        pink, red, blue, orange,
    }
    public class Ghost : GameMovingObject
    {
        private Scene _scene;
        private static Random random = new Random();
        public GhostColor Color { get; private set; }
        private int _direction;
        private bool _isFrightened = false;
        private string _normalGif;


        public Ghost(Scene scene, GhostColor color, double x, double y, double width) : base(string.Empty, x, y, width)
        {
            _direction = random.Next(4);
            Color = color;
            SetSpeedByDirection();
            SetImageByColor();
            GameManager.GameEvents.OnStrawberryEaten += SetFrightened;
        }
        private void SetImageByColor()
        {
            switch (Color)
            {
                case GhostColor.pink:
                    _normalGif = "Objects/ghosts/pinkghost gif.gif";
                    break;
                case GhostColor.red:
                    _normalGif = "Objects/ghosts/redghost gif.gif";
                    break;
                case GhostColor.orange:
                    _normalGif = "Objects/ghosts/orangeghost gif.gif";
                    break;
                case GhostColor.blue:
                    _normalGif = "Objects/ghosts/blueghost gif.gif";
                    break;
            }

            SetName(_normalGif);
        }

        public async void SetFrightened()
        {
            if (_isFrightened) return;

            _isFrightened = true;
            SetName("Objects/ghosts/scaredghost gif.gif");

            await Task.Delay(10000); // scared for 10 seconds

            _isFrightened = false;
            SetName(_normalGif);
        }

        private void SetSpeedByDirection()
        {
            switch (_direction)
            {
                case 0: // ימינה
                    _speedX = 4;
                    _speedY = 0;
                    break;
                case 2: // שמאל
                    _speedY = -4;
                    _speedX = 0;
                    break;
                case 1: // למעלה
                    _speedX = -4;
                    _speedY = 0;
                    break;
                case 3: // למעטה
                    _speedY = 4;
                    _speedX = 0;
                    break;
            }
        }

        public override void Collide(GameObject g)
        {
           
            if (g is Block)
            {
                if (_speedX < 0)//זז שמאל
                {
                    int changedirection= random.Next(2,4);
                    _direction = changedirection;
                    _x += 4;
                }

                if (_speedX > 0)// זז ימינה
                {
                    int changedirection = random.Next(2, 4);
                    _direction = changedirection;
                    _x -=4;
                }
                if (_speedY > 0)// זז למטה
                {
                    int changedirection = random.Next(0, 2);
                    _direction = changedirection;
                    _y -= 5;
                }
                if (_speedY < 0)//זז למעלה
                {
                    int changedirection = random.Next(0, 2);
                    _direction = changedirection;
                    _y += 5;
                }
                SetSpeedByDirection();

                if (g is Pacman)
                {
                    
                }

            }
        }
    }
}
