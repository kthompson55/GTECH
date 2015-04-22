using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collection_Game_Tool.PrizeLevels
{
    public class PrizeLevels
    {
        private List<PrizeLevel> prizeLevels = new List<PrizeLevel>();

        public PrizeLevel getPrizeLevel(int index)
        {
            return prizeLevels.ElementAt(index);
        }

        public void addPrizeLevel(PrizeLevel obj)
        {
            prizeLevels.Add(obj);
        }

        public void removePrizeLevel(int index)
        {
            prizeLevels.RemoveAt(index);
        }

        public void addPrizeLevelAt(PrizeLevel obj, int index)
        {
            prizeLevels.Insert(index, obj);
        }

        public int getIndexOfPrizeLevel(PrizeLevel pl)
        {
            return prizeLevels.IndexOf(pl);
        }
    }
}
