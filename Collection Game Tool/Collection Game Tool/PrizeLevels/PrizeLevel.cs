using Collection_Game_Tool.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Runtime.Serialization;

namespace Collection_Game_Tool.PrizeLevels
{
    [Serializable]
    public class PrizeLevel : IComparable, Teller, INotifyPropertyChanged
    {
        [field: NonSerializedAttribute()]
        public event PropertyChangedEventHandler PropertyChanged;

        //This stores the objects that the individual Prize Level can communicate with
        [field: NonSerializedAttribute()]
        List<Listener> audience = new List<Listener>();

        //PrizeLevel is 1,2,3,4,5, etc that coordinates with A,B,C,D,E,F,etc...Use PrizeLevelConverter to get int to string and vice-versa
        private int _prizeLevel;
        public int prizeLevel
        {
            get
            {
                return _prizeLevel;
            }
            set
            {
                _prizeLevel = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("prizeLevel"));
            }
        }

        //The value of the prize
        private double _prizeValue;
        public double prizeValue
        {
            get
            {
                return _prizeValue;
            }
            set
            {
                _prizeValue = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("prizeValue"));
            }
        }

        //The number of times you items you have to collect to win the prize level
        private int _numCollections;
        public int numCollections
        {
            get
            {
                return _numCollections;
            }
            set
            {
                _numCollections = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("numCollections"));
            }
        }

        //Declares the prize level an instant win or not
        private bool _isInstantWin;
        public bool isInstantWin
        {
            get
            {
                return _isInstantWin;
            }
            set
            {
                _isInstantWin = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("isInstantWin"));
            }
        }

        //Comparison of prizelevels, just compares value, this is used for sorting the prize levels
        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;
            PrizeLevel pl = (PrizeLevel)obj;
            return (int)Math.Ceiling(pl.prizeValue - this.prizeValue);
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

        public void initializeListener()
        {
            audience = new List<Listener>();
        }
    }
}
