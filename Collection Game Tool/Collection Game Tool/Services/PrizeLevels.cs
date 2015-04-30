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
            if (index >= prizeLevels.Count || index < 0)
                return null;
            return prizeLevels.ElementAt(index);
        }

        public void addPrizeLevel(PrizeLevel obj)
        {
            if(obj!=null)
                prizeLevels.Add(obj);
        }

        public void removePrizeLevel(int index)
        {
            if (!(index >= prizeLevels.Count || index < 0))
                prizeLevels.RemoveAt(index);
        }

        public void addPrizeLevelAt(PrizeLevel obj, int index)
        {
            if (!(index >= prizeLevels.Count || index < 0) && obj!=null)
                prizeLevels.Insert(index, obj);
        }

        public int getNumPrizeLevels()
        {
            return prizeLevels.Count;
        }

        public void sortPrizeLevels()
        {
            prizeLevels.Sort();
        }

        public int getLevelOfPrize(PrizeLevel obj)
        {
            for (int i = 0; i < prizeLevels.Count; i++)
            {
                if (prizeLevels[i].Equals(obj))
                    return i;
            }

            return -1;
        }
    }
}
