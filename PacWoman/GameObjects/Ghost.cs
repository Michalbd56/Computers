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

        // Store spawn position so we can reset after being eaten
        private double _spawnX;
        private double _spawnY;

        // Expose frightened state so Pacman can check it
        public bool IsFrightened => _isFrightened;


        public Ghost(Scene scene, GhostColor color, double x, double y, double width) : base(string.Empty, x, y, width)
        {
            _direction = random.Next(4);
            Color = color;
            _spawnX = x;
            _spawnY = y;
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

        // Called by Pacman when it eats this ghost during frightened mode
        public void ResetToSpawn()
        {
            _isFrightened = false;
            _x = _spawnX;
            _y = _spawnY;
            SetName(_normalGif);
            // Give the ghost a fresh random direction
            _direction = random.Next(4);
            SetSpeedByDirection();
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
                    int changedirection = random.Next(2, 4);
                    _direction = changedirection;
                    _x += 4;
                }

                if (_speedX > 0)// זז ימינה
                {
                    int changedirection = random.Next(2, 4);
                    _direction = changedirection;
                    _x -= 4;
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
            }

            // When frightened, do NOT eat Pacman — Pacman's Collide handles eating the ghost
            if (g is Pacman && _isFrightened)
            {
                // Do nothing — let Pacman's collision handler take over
                return;
            }
        }
    }
}