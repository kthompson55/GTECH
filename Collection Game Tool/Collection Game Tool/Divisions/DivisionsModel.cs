using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Collection_Game_Tool.Divisions
{
    [Serializable]
    public class DivisionsModel: INotifyPropertyChanged
    {
        public List<DivisionModel> divisions = new List<DivisionModel>();
        [field: NonSerializedAttribute()]
        public event PropertyChangedEventHandler PropertyChanged;
        private int _lossPermutations;
        public int LossMaxPermutations
        {
            get
            {
                return _lossPermutations;
            }

            set
            {
                _lossPermutations = value;
				if ( PropertyChanged != null ) PropertyChanged( this, new PropertyChangedEventArgs( "LossMaxPermutationsTextbox" ) );
            }
        }

		public string LossMaxPermutationsTextbox
		{
			get { return LossMaxPermutations.ToString(); }
			set
			{
				int number;
				if ( int.TryParse( value, out number ) )
				{
					LossMaxPermutations = number;
				}
			}
		}

        public int getNumberOfDivisions()
        {
            return divisions.Count;
        }

        public void addDivision(DivisionModel newDivision)
        {
            divisions.Add(newDivision);
            divisions.Sort();
        }

        public void removeDivision(DivisionModel divisionToRemove)
        {
            divisions.Remove(divisionToRemove);
        }

        public void removeDivision(int index)
        {
            divisions.RemoveAt(index);
        }

        public void clearDivisions()
        {
            divisions.Clear();
        }

        public DivisionModel getDivision(int index)
        {
            return divisions.ElementAt(index);
        }

        public int getSize()
        {
            return divisions.Count();
        }
    }
}
