using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collection_Game_Tool.PrizeLevels
{
    [Serializable]
    public class PrizeLevels
    {
        //Stores the number of prize levels, is used elsewhere for GameSetup reasons, is static because you will only ever have 1 PrizeLevels class
        static public int numPrizeLevels;
        //Stores each individual prize level
        public List<PrizeLevel> prizeLevels = new List<PrizeLevel>();

        //Gets a prize level from the index given
        public PrizeLevel getPrizeLevel(int index)
        {
            if (index >= prizeLevels.Count || index < 0)
                return null;
            return prizeLevels.ElementAt(index);
        }

        //Adds a prize level object
        public void addPrizeLevel(PrizeLevel obj)
        {
            if(obj!=null)
                prizeLevels.Add(obj);

            numPrizeLevels = prizeLevels.Count;
        }

        //Removes a prize level from the index given
        public void removePrizeLevel(int index)
        {
            if (!(index >= prizeLevels.Count || index < 0))
                prizeLevels.RemoveAt(index);

            numPrizeLevels = prizeLevels.Count;
        }

        //Adds a prize level at a specific index (this is never used anywhere, maybe used for future versions?)
        public void addPrizeLevelAt(PrizeLevel obj, int index)
        {
            if (!(index >= prizeLevels.Count || index < 0) && obj!=null)
                prizeLevels.Insert(index, obj);

            numPrizeLevels = prizeLevels.Count;
        }

        //Gets the number of prize levels, a safer option than using the static num
        public int getNumPrizeLevels()
        {
            return prizeLevels.Count;
        }

        //Sorts the prize levels based off value
        public void sortPrizeLevels()
        {
            prizeLevels.Sort();
        }

        //Tells in what position a prize level is based off the order of the prize levels
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
