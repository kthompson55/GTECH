using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collection_Game_Tool.GameSetup
{
    public class GameSetupModel
    {
        public short picks { get; set; } //Max 20
        public bool isNearWin { get; set; }
        public int nearWins { get; set; } 
        public int maxPermutations { get; set; }

        public void toggleNearWin()
        {
            isNearWin = !isNearWin;
        }
    }
}
