using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiApp.Domain
{
    public class SamuraiBattle
    {
        public int SamuraiID { get; set; }
        public int BattleID { get; set; }
        public Samurai Samurai { get; set; }
        public Battle Battle { get; set; }
    }
}
