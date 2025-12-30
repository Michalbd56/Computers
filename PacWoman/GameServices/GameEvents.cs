using System;
using Windows.System;

namespace PacWoman.Services
{
    //במחלקה זו יגדרו כל האירועים של המשחק
    public class GameEvents
    {
        public Action<VirtualKey> OnPress;// האירוע יתרחש כאשר המשתמש ילחץ על הלחצן
        public Action<VirtualKey> OnRelease;// האירוע יתרחש כאשר המשתמש יפסיק ללחוץ על המקש
        public Action<double, double> OnMousePress;// האירוע יתרחש כאשר המשתמש ילחץ על העכבר
        public Action<int, int> OnUpdateScore;
    
    }

    
}
