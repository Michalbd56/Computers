using System;
using Windows.ApplicationModel.UserDataTasks;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace GameEngine.Objects 
{
    public abstract class GameObject
    {
        private double _placeX; /// המיקום ההתחלתי האופקי
        private double _placeY; /// המיקום ההתחלתי האנכי
        public Image Image { get; set; }  ///המראה
        protected double _x; //המיקום האופקי 
        protected double _y;//המיקום האנכי
        protected string _fileName;// שם הקובץ
        protected Canvas _scene; // הבמה שדמות מופיעה עליה
        public bool IsObjectCreated { get; set; } = false;
        public virtual Rect Rect()
        {
            return new Rect(_x, _y, Image.ActualWidth, Image.ActualHeight);
        }
        public bool Collisional { get; set; } = true;
        protected GameObject(string fileName, double x,
            double y, double size)
        {
            _fileName = fileName;
            _x = x;
            _y = y;
            _placeX = x;
            _placeY = y;
            Image = new Image();
            Image.Width = size;
            Render();
            SetName(_fileName);
           
        }
        public void Init() ///הפעולה מחזירה את הגוף למיקום היווצרותו
        {
            _x = _placeX;
            _y = _placeY;
        }

        public void SetName(string fileName)
        {
            Image.Source = new BitmapImage(new Uri($"ms-appx:///Assets/{fileName}"));
        }

        public virtual void Render()// הפעולה מציירת את הדמות על הבמה
        {
            Canvas.SetLeft(Image, _x);
            Canvas.SetTop(Image, _y);
        }

        public virtual void CollideAsync(GameObject g)
        {
            

        }

        
    }
}
