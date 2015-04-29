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
        public DivisionPanelUC SectionContainer { get; set; }

        public DivisionUC()
        {
            InitializeComponent();
            Division = new DivisionModel();
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

            }
        }
    }
}
