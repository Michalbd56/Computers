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

        public Pacman(Scene scene,string fileName, double x, double y, double size) : base(fileName, x, y, size)
        {
            _scene = scene;
            Manager.Events.OnKeyUp += Move;

        }
        protected void Move(VirtualKey key)
        {
            if (key == ControlKeys.PlayerRunLeft)
            {
                RunLeft();
            }
            if (key == ControlKeys.PlayerRunRight)
            {
                RunRight();
            }
            if (key == ControlKeys.PlayerRunUp)
            {
                RunUp(this);
            }

            if (key == ControlKeys.PlayerRunDown)
            {
                RunDown(this);
            }
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

        public override async void Collide(GameObject g)
        {
            if (g is Block)
            {
                if (_speedX < 0)
                {
                    _speedX = 0;
                    _x += 5;
                }

                if (_speedX > 0)
                {
                    _speedX = 0;
                    _x -= 5;
                }
                if (_speedY > 0)
                {
                    _speedY = 0;
                    _y -= 5;
                }
                if (_speedY < 0)
                {
                    _speedY = 0;
                    _y += 5;
                }
            }
            if (g is Coin)
            {
                _collectedCoins++;
                _scene.RemoveObject(g);
                Manager.Events.OnUpdateScore?.Invoke(_collectedCoins);
            }
            if (g is strawberry)
            {
                _collectedCoins += 10;
                _scene.RemoveObject(g);
                Manager.Events.OnUpdateScore?.Invoke(_collectedCoins);

                foreach (var obj in _scene.GameObject)
                {
                    if (obj is Ghost ghost)
                    {
                        ghost.SetFrightened();
                    }
                }
            }
            if (g is Ghost)
            {
                _hearts--;
                GameManager.Events.OnRemoveLifes?.Invoke(_hearts);
                _x = 770;
                _y =  375;
                _speedX = 0;
                _speedY = 0;
            }
        }
        // protected void MatchGifToState()
        //{
        //    switch (PlayerState)
        //    {
        //        case StateType.runDown: SetName("Objects/player/pac gif D.gif"); break;
        //        case StateType.runLeft: SetName("Objects/player/pac gif L.gif"); break;
        //        case StateType.runRight: SetName("Objects/player/pac gif R.gif"); break;
        //        case StateType.runUp: SetName("Objects/player/pac gif U.gif"); break;

        //    }
        //}


        // public override void Render()
        //{
        //    base.Render();
        //    if ((_x <= 0 && _speedX < 0) || (Rect().Right >= _scene.ActualWidth && _speedX > 0))
        //    {
        //        _speedX = 0;
        //    }
        //    if ((_y <= 0 && _speedY < 0) || (Rect().Bottom >=0 _scene.ActualHeight && _speedY > 0))
        //    {
        //        _speedY = 0;
        //    }
        //    _x += _speedX;
        //    _y += _speedY;







    }
}
