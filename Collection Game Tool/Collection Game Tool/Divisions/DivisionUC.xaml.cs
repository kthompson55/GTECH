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
        public PrizeLevels.PrizeLevels prizes { get; set; }

        public DivisionUC()
        {
            InitializeComponent();
            Division = new DivisionModel();
        }

        private void deleteDivisionButton_Click(object sender, RoutedEventArgs e)
        {
            DivisionPanelUC mainPanel = getPanel();
            int index = getIndex();
            mainPanel.removeDivision(index);
        }

        private DivisionPanelUC getPanel()
        {
            Grid divisionsGrid = (Grid)this.Parent;
            Grid mainGrid = (Grid)divisionsGrid.Parent;
            return (DivisionPanelUC)mainGrid.Parent;
        }

        public int getIndex()
        {
            Grid divisionsGrid = (Grid)this.Parent;
            return divisionsGrid.Children.IndexOf(this);
        }

        public void onListen(object pass)
        {
            //This listens to DivisionPanel
            throw new NotImplementedException();
        }
    }
}
