using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Collection_Game_Tool.Services;
using System.Runtime.Serialization;

namespace Collection_Game_Tool.GameSetup
{
    [Serializable]
    public class GameSetupModel : INotifyPropertyChanged, Teller//, ISerializable
    {
        [field: NonSerializedAttribute()]
        public event PropertyChangedEventHandler PropertyChanged;

        [field: NonSerializedAttribute()]
        List<Listener> audience = new List<Listener>();

        public GameSetupModel() { }

        public void initializeListener()
        {
            audience = new List<Listener>();
        }

        private short tp=2;
        public short totalPicks
        {
            get
            {
                return tp;
            }
            set
            {
                tp = value; //Max 20
            } 
        }
        private bool inw;
        public bool isNearWin 
        {
            get
            {
                return inw;
            }
            set
            {
                inw = value;
            }
        }
        private short nw = 2;
        public short nearWins 
        {
            get
            {
                return nw;
            }
            set
            {
                nw = value;
            }
        } //Max 12

        private uint mp = 1;
        public uint maxPermutations 
        {
            get
            {
                return mp;
            }
            set
            {
                mp = value;
            }
        }

        private bool _canCreate;
        public bool canCreate
        {
            get
            {
                return _canCreate;
            }
            set
            {
                _canCreate = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("canCreate"));
            }
        }

        public void toggleNearWin()
        {
            isNearWin = !isNearWin;
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
    }
}
