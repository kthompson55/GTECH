﻿using System;
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
    public class DivisionModel : IComparable, INotifyPropertyChanged, ISerializable
    {
        [field: NonSerializedAttribute()]
        public event PropertyChangedEventHandler PropertyChanged;

        public List<PrizeLevel> selectedPrizes { get; set; }
        private int _divisionNumber;
        private int _totalPlayerPicks;
        private double _totalPrizeValue;

        private int _id;
        public int id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        public DivisionModel()
        {
            DivisionNumber = 0;
            TotalPlayerPicks = 0;
            TotalPrizeValue = 0.00;
            selectedPrizes = new List<PrizeLevel>();
        }

        public DivisionModel(int playerPicks, double totalValue, List<PrizeLevel> levels)
        {
            TotalPlayerPicks = playerPicks;
            TotalPrizeValue = totalValue;
            selectedPrizes = levels;
        }

        public DivisionModel(SerializationInfo info, StreamingContext context)
        {
            
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }

        public void addPrizeLevel(PrizeLevel prizeLevelToAdd)
        {
            selectedPrizes.Add(prizeLevelToAdd);
            selectedPrizes.Sort();
        }

        public void removePrizeLevel(PrizeLevel prizeLevelToRemove)
        {
            selectedPrizes.Remove(prizeLevelToRemove);
            selectedPrizes.Sort();
        }

        public void removePrizeLevel(int prizeLevelIndex)
        {
            selectedPrizes.RemoveAt(prizeLevelIndex);
            selectedPrizes.Sort();
        }

        public void clearPrizeLevelList()
        {
            selectedPrizes = new List<PrizeLevel>();
        }

        public List<PrizeLevel> getPrizeLevelsAtDivision()
        {
            return selectedPrizes;
        }

        public PrizeLevel getPrizeLevel(int index)
        {
            return selectedPrizes.ElementAt(index);
        }

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