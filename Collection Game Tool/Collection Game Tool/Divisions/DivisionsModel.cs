using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Collection_Game_Tool.Divisions
{
    [Serializable()]
    public class DivisionsModel : ISerializable
    {
        public List<DivisionModel> divisions { get; set; }

        public DivisionsModel() { divisions = new List<DivisionModel>(); }

        public DivisionsModel(SerializationInfo info, StreamingContext context)
        {
            divisions = (List<DivisionModel>)info.GetValue("Divisions", typeof(List<DivisionModel>));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Divisions", divisions);
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
