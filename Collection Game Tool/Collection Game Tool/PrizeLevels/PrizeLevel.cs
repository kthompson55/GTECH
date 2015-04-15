using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collection_Game_Tool.PrizeLevels
{
    class PrizeLevel : IComparable
    {
        public double prizeValue{ get; set; }

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
    }
}
