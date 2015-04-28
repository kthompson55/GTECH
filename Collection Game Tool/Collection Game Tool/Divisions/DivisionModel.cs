using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Collection_Game_Tool.PrizeLevels;

namespace Collection_Game_Tool.Divisions
{
    [Serializable]
    public class DivisionModel : IComparable, INotifyPropertyChanged
    {
        [field: NonSerializedAttribute()]
        public event PropertyChangedEventHandler PropertyChanged;

        public List<PrizeLevel> prizesInDivision { get; set; }
        private int _totalPlayerPicks;
        private double _totalPrizeValue;

        public DivisionModel()
        {
            TotalPlayerPicks = 0;
            TotalPrizeValue = 0.00;
            prizesInDivision = new List<PrizeLevel>();
        }

        public DivisionModel(int playerPicks, double totalValue, List<PrizeLevel> levels)
        {
            TotalPlayerPicks = playerPicks;
            TotalPrizeValue = totalValue;
            prizesInDivision = levels;
        }
        
        public void addPrizeLevel(PrizeLevel prizeLevelToAdd)
        {
            prizesInDivision.Add(prizeLevelToAdd);
            prizesInDivision.Sort();
        }

        public void removePrizeLevel(PrizeLevel prizeLevelToRemove)
        {
            prizesInDivision.Remove(prizeLevelToRemove);
            prizesInDivision.Sort();
        }

        public void removePrizeLevel(int prizeLevelIndex)
        {
            prizesInDivision.RemoveAt(prizeLevelIndex);
            prizesInDivision.Sort();
        }

        public void clearPrizeLevelList()
        {
            prizesInDivision = new List<PrizeLevel>();
        }

        public List<PrizeLevel> getPrizeLevelsAtDivision()
        {
            return prizesInDivision;
        }

        public PrizeLevel getPrizeLevel(int index)
        {
            return prizesInDivision.ElementAt(index);
        }
        
        public double calculateDivisionValue()
        {
            double divisionValue = 0.0f;
            foreach(PrizeLevel p in prizesInDivision)
            {
                divisionValue += p.prizeValue;
            }
            return divisionValue;
        }

        public int TotalPlayerPicks
        {
            get
            {
                return _totalPlayerPicks;
            }

            set
            {
                _totalPlayerPicks = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("TotalPlayerPicks"));
            }
        }

        public double TotalPrizeValue
        {
            get
            {
                return _totalPrizeValue;
            }

            set
            {
                _totalPrizeValue = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("TotalPrizeValue"));
            }
        }

        public int CompareTo(object obj)
        {
            DivisionModel dm = (DivisionModel)obj;
            return (int)Math.Ceiling(this.calculateDivisionValue() - dm.calculateDivisionValue());
        }

        internal List<int> getNeededPicks()
        {
            List<int> picks = new List<int>();

            return picks;
        }
    }
}
