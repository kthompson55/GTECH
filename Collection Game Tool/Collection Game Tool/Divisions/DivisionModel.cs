using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collection_Game_Tool.Divisions
{
    public class DivisionModel : IComparable
    {
        private List<PrizeLevels.PrizeLevel> prizesAtDivision = new List<PrizeLevels.PrizeLevel>();
        private int totalPlayerPicks;
        private double totalPrizeValue;

        public void addPrizeLevel(PrizeLevels.PrizeLevel prizeLevelToAdd)
        {
            prizesAtDivision.Add(prizeLevelToAdd);
            prizesAtDivision.Sort();
        }

        public List<PrizeLevels.PrizeLevel> getPrizeLevelsAtDivision()
        {
            return prizesAtDivision;
        }

        public PrizeLevels.PrizeLevel getPrizeLevel(int index)
        {
            return prizesAtDivision.ElementAt(index);
        }

        public double getDivisionValue()
        {
            double divisionValue = 0.0f;
            foreach(PrizeLevels.PrizeLevel p in prizesAtDivision)
            {
                divisionValue += p.prizeValue;
            }
            return divisionValue;
        }

        public int CompareTo(object obj)
        {
            DivisionModel dm = (DivisionModel)obj;
            return (int)Math.Ceiling(this.getDivisionValue() - dm.getDivisionValue());
        }

        internal List<int> getNeededPicks()
        {
            List<int> picks = new List<int>();

            return picks;
        }
    }
}
