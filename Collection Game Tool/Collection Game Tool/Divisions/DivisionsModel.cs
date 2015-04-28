using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collection_Game_Tool.Divisions
{
    public class DivisionsModel
    {
        private List<DivisionModel> divisions = new List<DivisionModel>();

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
    }
}