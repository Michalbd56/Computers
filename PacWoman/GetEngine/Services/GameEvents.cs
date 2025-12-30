using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;

namespace GameEngine.Services
{
   
    public class GameEvents
    {
        public Action OnRun;       //האירוע שבזכותו כל מי שיירשם אליו ינוע
        public Action<VirtualKey> OnKeyDown; //האירוע שיתרחש אם המ-שתמש ילחץ על מקש כלשהו
        public Action<VirtualKey> OnKeyUp;      //האירוע שיתרחש אם המשתמש יעזוב על מקש כלשהו
        public Action<int> OnUpdateScore; //האירוע שבאמצעותו ניתן להציג את הישג המעודכן על המסך
        public Action<int> OnRemoveLifes;//האירוע שיאפשר עדכון מספר חיים

        public GameEvents()
        {

        }
    }
}
