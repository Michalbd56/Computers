using GameEngine.Objects;
using GameEngine.Services;
using PacWoman.GameServices;
using System;
using System.Threading.Tasks;
using Windows.System;

namespace PacWoman.GameObjects
{
    public class Pacman : GameMovingObject
    {
        private Scene _scene;
        private int _collectedCoins = 0;
        private int _hearts = 3;

        public Pacman(Scene scene, string fileName, double x, double y, double size) : base(fileName, x, y, size)
        {
            _scene = scene;
            Manager.Events.OnKeyUp += Move;
        }

        protected void Move(VirtualKey key)
        {
            if (key == ControlKeys.PlayerRunLeft)
                RunLeft();
            if (key == ControlKeys.PlayerRunRight)
                RunRight();
            if (key == ControlKeys.PlayerRunUp)
                RunUp(this);
            if (key == ControlKeys.PlayerRunDown)
                RunDown(this);
        }

        private void RunDown(Pacman pacman)
        {
            _speedX = 0;
            _speedY = 5;
            SetName("Objects/player/pac gif D.gif");
        }

        private void RunUp(Pacman pacman)
        {
            _speedX = 0;
            _speedY = -5;
            SetName("Objects/player/pac gif U.gif");
        }

        private void RunRight()
        {
            _speedX = 5;
            _speedY = 0;
            SetName("Objects/player/pac gif R.gif");
        }

        private void RunLeft()
        {
            _speedX = -5;
            _speedY = 0;
            SetName("Objects/player/pac gif L.gif");
        }

        public override void Collide(GameObject g)
        {
            if (g is Block)
            {
                if (_speedX < 0) { _speedX = 0; _x += 5; }
                if (_speedX > 0) { _speedX = 0; _x -= 5; }
                if (_speedY > 0) { _speedY = 0; _y -= 5; }
                if (_speedY < 0) { _speedY = 0; _y += 5; }
            }

            if (g is Coin)
            {
                _collectedCoins++;
                _scene.RemoveObject(g);
                Manager.Events.OnUpdateScore?.Invoke(_collectedCoins);

                // Win condition: all coins eaten and still alive
                if (_collectedCoins >= GameManager.TotalCoins && _hearts > 0)
                {
                    GameManager.GameEvents.OnLevelComplete?.Invoke();
                }
            }

            if (g is strawberry)
            {
                _collectedCoins += 10;
                _scene.RemoveObject(g);
                Manager.Events.OnUpdateScore?.Invoke(_collectedCoins);
                GameManager.GameEvents.OnStrawberryEaten?.Invoke();
            }

            if (g is Ghost ghost)
            {
                if (ghost.IsFrightened)
                {
                    // Pacman eats frightened ghost: +30 coins, ghost resets to spawn
                    _collectedCoins += 30;
                    Manager.Events.OnUpdateScore?.Invoke(_collectedCoins);
                    ghost.ResetToSpawn();
                }
                else
                {
                    // Normal ghost eats Pacman: lose a life and reset position
                    _hearts--;
                    GameManager.Events.OnRemoveLifes?.Invoke(_hearts);
                    _x = 770;
                    _y = 375;
                    _speedX = 0;
                    _speedY = 0;
                }
            }
        }
    }
}