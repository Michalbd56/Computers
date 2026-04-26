using GameEngine.Services;
using PacWoman.GameObjects;
using PacWoman.Modles;
using System;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;

namespace PacWoman.GameServices
{
    public class GameManager : Manager
    {
        public static GameUser Gameuser  = new GameUser();
        public static GameServices.GameEvents GameEvents = new GameServices.GameEvents();
        private Scene _scene;
        Random r = new Random();
        private DispatcherTimer _RunTimer; // טיימר שבזכותו האובייקטים הנעים ינועו //
        private int _level;

        

        public GameManager(Scene scene, int level) : base(scene)
        {
            _scene = scene;
            _level = level;

            _scene.AddObject(new Pacman(_scene, "Objects/Player/pac gif R.gif", 770, 375, 30));

            CreateMaze();
            CreateCoins();
            CreateGhots();
        }


        private void CreateCoins()
        {
            //שורות אנכיות של מטבעות |||||||||||||||
            for (int i = 90; i <= 500; i += 50)
            {
                _scene.AddObject(new Coin("Objects/rewards/coin.png", 40, i, 6));
            }

            for (int i = 80; i <= 500; i += 47)
            {
                _scene.AddObject(new Coin("Objects/rewards/coin.png", 310, i, 6));
            }

            for (int i = 80; i <= 480; i += 47)
            {
                _scene.AddObject(new Coin("Objects/rewards/coin.png", 890, i, 6));
            }

            for (int i = 40; i <= 500; i += 50)
            {
                _scene.AddObject(new Coin("Objects/rewards/coin.png", 1170, i, 6));
            }



            //שורות אופקית של מטבעות ---------------------------
            for (int i = 40; i <= 1160; i += 50)
            {
                _scene.AddObject(new Coin("Objects/rewards/coin.png", i, 40, 6));
            }


            for (int i = 72; i <= 280; i += 47)
            {
                _scene.AddObject(new Coin("Objects/rewards/coin.png ", i, 215, 6));
            }

            for (int i = 72; i <= 280; i += 47)
            {
                _scene.AddObject(new Coin("Objects/rewards/coin.png", i, 300, 6));
            }

            for (int i = 85; i <= 1160; i += 50)
            {
                _scene.AddObject(new Coin("Objects/rewards/coin.png", i, 490, 6));
            }


            _scene.AddObject(new strawberry("Objects/rewards/strawberry.png", 25, 380, 35));

            for (int i = 940; i <= 1165; i += 47)
            {
                _scene.AddObject(new Coin("Objects/rewards/coin.png ", i, 215, 6));
            }

            for (int i = 940; i <= 1165; i += 47)
            {
                _scene.AddObject(new Coin("Objects/rewards/coin.png", i, 300, 6));
            }


            for (int i = 370; i <= 840; i += 50)//-------------- באמצע
            {
                _scene.AddObject(new Coin("Objects/rewards/coin.png", i, 155, 6));
            }

            for (int i = 370; i <= 700; i += 50)
            {
                _scene.AddObject(new Coin("Objects/rewards/coin.png", i, 388, 6));
            }
            _scene.AddObject(new Coin("Objects/rewards/coin.png", 770, 388, 6));


        }

        private void CreateMaze()
        {
            //horizontalsssss -------------
            for (int i = 0; i <= 1015; i += 203) // גבול עליון
            {
                _scene.AddObject(new Block(DirectionBlockType.horizontal, i, 0, 203));
            }
            for (int i = 0; i <= 1015; i += 203) //גבול תחתון
            {
                _scene.AddObject(new Block(DirectionBlockType.horizontal, i, 515, 203));
            }

            for (int i = 70; i <= 170; i += 100) //מלבנים משמאל
            {
                _scene.AddObject(new Block(DirectionBlockType.horizontal, i, 70, 100));

            }

            for (int i = 70; i <= 170; i += 100)
            {
                _scene.AddObject(new Block(DirectionBlockType.horizontal, i, 170, 100));
            }

            for (int i = 0; i <= 220; i += 83)
            {
                _scene.AddObject(new Block(DirectionBlockType.horizontal, i, 257, 83));
            }

            for (int i = 70; i <= 170; i += 100)
            {
                _scene.AddObject(new Block(DirectionBlockType.horizontal, i, 340, 100));
            }

            for (int i = 70; i <= 170; i += 100) //----------------
            {
                _scene.AddObject(new Block(DirectionBlockType.horizontal, i, 445, 100));

            }


            for (int i = 350; i <= 800; i += 100)//-------------- באמצע
            {
                _scene.AddObject(new Block(DirectionBlockType.horizontal, i, 125, 100));
            }

            for (int i = 350; i <= 800; i += 100)
            {
                _scene.AddObject(new Block(DirectionBlockType.horizontal, i, 410, 100));
            }


            for (int i = 490; i <= 610; i += 100)//בית של רוחות
            {
                _scene.AddObject(new Block(DirectionBlockType.horizontal, i, 310, 100));//-------
            }
            for (int i = 210; i <= 245; i += 20)//בית של רוחות
            {
                _scene.AddObject(new Block(DirectionBlockType.vertical, 682, i, 9));//||||||

            }
            for (int i = 210; i <= 245; i += 20)//בית של רוחות
            {
                _scene.AddObject(new Block(DirectionBlockType.vertical, 490, i, 9));
            }


            for (int i = 940; i <= 1040; i += 100) // מלבנים מימין
            {
                _scene.AddObject(new Block(DirectionBlockType.horizontal, i, 70, 100));

            }

            for (int i = 940; i <= 1040; i += 100)
            {
                _scene.AddObject(new Block(DirectionBlockType.horizontal, i, 170, 100));

            }

            for (int i = 940; i <= 1100; i += 20)
            {
                _scene.AddObject(new Block(DirectionBlockType.horizontal, i, 257, 100));

            }

            for (int i = 940; i <= 1040; i += 100)
            {
                _scene.AddObject(new Block(DirectionBlockType.horizontal, i, 340, 100));

            }

            for (int i = 940; i <= 1040; i += 100)
            {
                _scene.AddObject(new Block(DirectionBlockType.horizontal, i, 445, 100));

            }








            //verticalsssss  |||||

            for (int j = 0; j <= 338; j += 105)
            {
                _scene.AddObject(new Block(DirectionBlockType.vertical, 0, j, 20));
            }

            for (int j = 0; j <= 340; j += 105)
            {
                _scene.AddObject(new Block(DirectionBlockType.vertical, 1200, j, 20));
            }

            for (int j = 75; j <= 100; j += 100)
            {
                _scene.AddObject(new Block(DirectionBlockType.vertical, 70, j, 10));
            }

            for (int j = 80; j <= 130; j += 100)
            {
                _scene.AddObject(new Block(DirectionBlockType.vertical, 260, j, 10));
            }

            for (int j = 80; j <= 130; j += 100)
            {
                _scene.AddObject(new Block(DirectionBlockType.vertical, 940, j, 10));
            }

            for (int j = 75; j <= 100; j += 100)
            {
                _scene.AddObject(new Block(DirectionBlockType.vertical, 1130, j, 10));
            }

            for (int j = 180; j <= 270; j += 10)
            {
                _scene.AddObject(new Block(DirectionBlockType.vertical, 380, j, 10));
            }

            for (int j = 180; j <= 270; j += 10)
            {
                _scene.AddObject(new Block(DirectionBlockType.vertical, 810, j, 10));
            }

            for (int j = 350; j <= 400; j += 100)
            {
                _scene.AddObject(new Block(DirectionBlockType.vertical, 70, j, 10));
            }

            for (int j = 350; j <= 400; j += 100)
            {
                _scene.AddObject(new Block(DirectionBlockType.vertical, 260, j, 10));
            }

            for (int j = 350; j <= 400; j += 100)
            {
                _scene.AddObject(new Block(DirectionBlockType.vertical, 940, j, 10));
            }

            for (int j = 350; j <= 400; j += 100)
            {
                _scene.AddObject(new Block(DirectionBlockType.vertical, 1130, j, 10));
            }



            for (int j = 0; j <= 8; j += 7)
            {
                _scene.AddObject(new Block(DirectionBlockType.vertical, 580, j, 7));
            }

            for (int j = 470; j <= 470; j += 7)
            {
                _scene.AddObject(new Block(DirectionBlockType.vertical, 580, j, 7));
            }

        }
        private void CreateGhots()
        {
            if (Gameuser.Level.CountGhosts == 2) // קל
            {
                _scene.AddObject(new Ghost(_scene, GhostColor.pink, 630, 270, 25));
                _scene.AddObject(new Ghost(_scene, GhostColor.red, 590, 270, 25));
            }
            else if (Gameuser.Level.CountGhosts == 3) // בינוני
            {
                _scene.AddObject(new Ghost(_scene, GhostColor.pink, 630, 270, 30));
                _scene.AddObject(new Ghost(_scene, GhostColor.red, 590, 270, 30));
                _scene.AddObject(new Ghost(_scene, GhostColor.orange, 550, 270, 30));
            }
            else if (Gameuser.Level.CountGhosts == 4) // קשה
            {
                _scene.AddObject(new Ghost(_scene, GhostColor.pink, 630, 270, 35));
                _scene.AddObject(new Ghost(_scene, GhostColor.red, 590, 270, 35));
                _scene.AddObject(new Ghost(_scene, GhostColor.orange, 550, 270, 35));
                _scene.AddObject(new Ghost(_scene, GhostColor.blue, 510, 270, 35));
            }
        }











    }
}
    


    
