using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Collection_Game_Tool.PrizeLevels;
using System.Runtime.Serialization;

namespace Collection_Game_Tool.Divisions
{
    [Serializable]
    public class DivisionModel : IComparable, INotifyPropertyChanged
    {
        [field: NonSerializedAttribute()]
        public event PropertyChangedEventHandler PropertyChanged;

        public String errorID;
        public List<PrizeLevel> selectedPrizes = new List<PrizeLevel>();
        public List<LevelBox> levelBoxes = new List<LevelBox>();
        public const int MAX_PRIZE_BOXES = 12;

        private int _maxPermutations;
        private int _divisionNumber;
        private int _totalPlayerPicks;
        private double _totalPrizeValue;

        public DivisionModel()
        {
            errorID = null;
            DivisionNumber = 0;
            TotalPlayerPicks = 0;
            TotalPrizeValue = 0.00;
        }

        /// <summary>
        /// For divisions, this adds a prize level to the list of selected prize levels
        /// The list represents the prize levels selected within the division
        /// </summary>
        /// <param name="prizeLevelToAdd"></param>
        public void addPrizeLevel(PrizeLevel prizeLevelToAdd)
        {
            selectedPrizes.Add(prizeLevelToAdd);
            selectedPrizes.Sort();
        }

        /// <summary>
        /// Removes a prize level based on the prize level object passed in
        /// </summary>
        /// <param name="prizeLevelToRemove"></param>
        public void removePrizeLevel(PrizeLevel prizeLevelToRemove)
        {
            selectedPrizes.Remove(prizeLevelToRemove);
            selectedPrizes.Sort();
        }

        /// <summary>
        /// Removes a prize level at the specified index
        /// </summary>
        /// <param name="prizeLevelIndex"></param>
        public void removePrizeLevel(int prizeLevelIndex)
        {
            selectedPrizes.RemoveAt(prizeLevelIndex);
            selectedPrizes.Sort();
        }

        /// <summary>
        /// Clears the prize level list of all the selected prize levels; leaves the list empty
        /// </summary>
        public void clearPrizeLevelList()
        {
            selectedPrizes = new List<PrizeLevel>();
        }

        /// <returns>The current list of selected prize levels</returns>
        public List<PrizeLevel> getPrizeLevelsAtDivision()
        {
            return selectedPrizes;
        }

        /// <summary>
        /// Gets a prize level from the list of selected prize levels at the specified index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public PrizeLevel getPrizeLevel(int index)
        {
            return selectedPrizes.ElementAt(index);
        }

        /// <summary>
        /// Calculates the total value of the division based on the combined value of the selected prize levels
        /// </summary>
        /// <returns></returns>
        public double calculateDivisionValue()
        {
            double divisionValue = 0.0f;
            foreach (PrizeLevel p in selectedPrizes)
            {
                divisionValue += p.prizeValue;
            }
            return divisionValue;
        }

        public int calculateTotalCollections()
        {
            int collections = 0;
            foreach (PrizeLevel p in selectedPrizes)
            {
                if (p.isInstantWin)
                    collections += 1;
                else
                    collections += p.numCollections;
            }
            return collections;
        }

        public int DivisionNumber
        {
            get
            {
                return _divisionNumber;
            }

            set
            {
                _divisionNumber = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("DivisionNumber"));
            }
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

        public int MaxPermutations
        {
            get
            {
                return _maxPermutations;
            }

            set
            {
                _maxPermutations = value;
                if(PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("MaxPermutations"));
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
            return (int)Math.Ceiling(dm.calculateDivisionValue() - this.calculateDivisionValue());
        }

        internal List<int> getNeededPicks()
        {
            List<int> picks = new List<int>();

            return picks;
        }
    }
}