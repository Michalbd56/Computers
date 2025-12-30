using System;

namespace GameEngine.Objects
{
    public abstract class GameMovingObject : GameObject
    {
        protected double _speedX;//תאוצה אופקית
        protected double _speedY;//מהירות אנכית
        protected double _accelerationX;// תאוצה אופקית
        protected double _accelerationY; //תאוצה אנכית
        protected double _toX;
        protected double _toY;


        protected GameMovingObject(string fileName, double x, double y, double size) :
            base(fileName, x, y, size)
        {
            Stop();
        }
        public virtual void Stop()
        {
            _speedX = _speedY = _accelerationX = _accelerationY = 0;
        }
        public override void Render()
        {
            _x += _speedX;
            _y += _speedY;
            _speedX += _accelerationX;
            _speedY += _accelerationY;
            if(Math.Abs(_x-_toX)<10 && Math.Abs(_y - _toY) < 10)
            {
                Stop();
                _x = _toX;
                _y = _toY;
            }
            base.Render();
        }

    }
}
