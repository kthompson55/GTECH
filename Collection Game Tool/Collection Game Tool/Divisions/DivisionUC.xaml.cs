using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Collection_Game_Tool.PrizeLevels;
using Collection_Game_Tool.Services;

namespace Collection_Game_Tool.Divisions
{
    /// <summary>
    /// Interaction logic for DivisionUC.xaml
    /// </summary>
    public partial class DivisionUC : UserControl, Listener
    {
        public DivisionModel Division { get; set; }
        public PrizeLevels.PrizeLevels Prizes { get; set; }
        public DivisionPanelUC SectionContainer { get; set; }
        private List<PrizeLevelBox> PrizeBoxes { get; set; }

        public DivisionUC()
        {
            InitializeComponent();
            Division = new DivisionModel();
            PrizeBoxes = new List<PrizeLevelBox>();

            for (int i = 0; i < 12; i++)
            {
                PrizeLevelBox box = new PrizeLevelBox(this, false, i+1);
                if (i < 2) { box.IsAvailable = true; }
                PrizeBoxes.Add(box);
                prizeLevelsGrid.Children.Add(PrizeBoxes[i]);
            }

            for (int i = 0; i < 2; i++)
            {
                PrizeBoxes[i].IsAvailable = true;
            }
        }

        private void deleteDivisionButton_Click(object sender, RoutedEventArgs e)
        {
            int index = getIndex();
            SectionContainer.removeDivision(index);
        }

        public int getIndex()
        {
            StackPanel divisionsPanel = (StackPanel)this.Parent;
            return divisionsPanel.Children.IndexOf(this);
        }

        public void onListen(object pass)
        {
            if (pass is PrizeLevels.PrizeLevels)
            {
                Prizes = (PrizeLevels.PrizeLevels)pass;
            }
            else
            {
                Console.WriteLine("ERROR: PrizeLevels was not passed into the Division User Control");
            }
        }
    }
}
