using System;
using Windows.UI.Xaml;

namespace GameEngine.Services
{
    public abstract class Manager
    {
        public Scene Scene { get; private set; }    //במה
        private static DispatcherTimer _runTimer;  // ללא הפסקה OnRun טיימר שידליק אירוע
        public static GameEvents Events { get; private set; } = new GameEvents(); //חבילת אירועים שניתן לגשת אליה מכל מקום

        public Manager(Scene scene)
        {
            Scene = scene;
            if (_runTimer == null)
            {
                _runTimer = new DispatcherTimer(); //כך בונים טיימר
                _runTimer.Interval = TimeSpan.FromMilliseconds(0.5);
                _runTimer.Start();
                _runTimer.Tick += Tick;
            }


            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            Window.Current.CoreWindow.KeyUp += CoreWindow_KeyUp;
        }

        private void CoreWindow_KeyUp(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            if (Events.OnKeyUp != null)
            {
                Events.OnKeyUp(args.VirtualKey); //כך הופעל האירוע
            }
        }

        private void CoreWindow_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            if (Events.OnKeyDown != null)
            {
                Events.OnKeyDown(args.VirtualKey); //כך הופעל האירוע
            }
        }

        private void Tick(object sender, object e)
        {
            if (Events.OnRun != null)   //כך מדליקים את האירוע. האירוע יתרחש 1000 פעמים שניה אחת
            {
                Events.OnRun();
            }
        }

       
    }
}
