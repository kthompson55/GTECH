using Collection_Game_Tool.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collection_Game_Tool.PrizeLevels
{
    public class PrizeLevel : IComparable, Teller
    {
        List<Listener> audience =new List<Listener>();
        static List<String> levels = new List<String>()
        {
            "A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T"
        };

        private int pl;
        public int prizeLevel
        {
            get
            {
                return pl;
            }
            set
            {
                pl = value;
                shout("Level");
            }
        }

        private double pv;
        public double prizeValue
        {
            get
            {
                return pv;
            }
            set
            {
                pv = value;
            }
        }

        public String currentLevel()
        {
            return levels[prizeLevel-1];
        }

        public int numCollections{ get; set; }

        public bool isInstantWin { get; set; }

        public int CompareTo(object obj)
        {
            PrizeLevel pl = (PrizeLevel)obj;
            return (int)Math.Ceiling(this.prizeValue - pl.prizeValue);
        }

        public void toggleIsInstantWin()
        {
            isInstantWin = !isInstantWin;
        }

        public void shout(object pass)
        {
            foreach (Listener fans in audience)
            {
                fans.onListen(pass);
            }
        }

        public void addListener(Listener list)
        {
            audience.Add(list);
        }
    }
}
