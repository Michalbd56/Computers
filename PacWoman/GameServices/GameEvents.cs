using System;
using Windows.System;

namespace PacWoman.GameServices
{
    //במחלקה זו יגדרו כל האירועים של המשחק
    public class GameEvents
    {
        public Action OnStrawberryEaten;
        public Action OnLevelComplete;//fires when Pacman collects every coin on the map
    }
}