using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collection_Game_Tool.GameSetup
{
    public class GameSetupModel
    {
        private short tp;
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
        private short nw;
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

        private uint mp;
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

        public void toggleNearWin()
        {
            isNearWin = !isNearWin;
        }
    }
}
