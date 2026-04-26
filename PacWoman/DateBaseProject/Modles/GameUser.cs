using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PacWoman.Modles
{
    public class GameUser
    {
        public int Id { get; set; } = 1;
        public string UserName { get; set; } = "Anonymous";

        public string Password { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
        public GameLevel Level {  get; set; } = new GameLevel();

        public string CurrentCharacter { get; set; } = "Yellow";
        public int MaxLevel { get; set; } = 3;
        public int CollectedCoins { get; set; } = 0 ;

        public int Score { get; set; } = 0;
       
    }
}
