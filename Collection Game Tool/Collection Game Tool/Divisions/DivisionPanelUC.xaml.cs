using Collection_Game_Tool.Services;
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

namespace Collection_Game_Tool.Divisions
{
    /// <summary>
    /// Interaction logic for DivisionPanelUC.xaml
    /// </summary>
    public partial class DivisionPanelUC : UserControl, Listener, Teller
    {
        List<Listener> listenerList = new List<Listener>();
        public DivisionsModel divisionsModel;
        private double marginAmount;
        public PrizeLevels.PrizeLevels prizes { get; set; }

        public DivisionPanelUC()
        {
            InitializeComponent();
            divisionsModel = new DivisionsModel();
            marginAmount = 10;

            //DivisionUC firstDiv = new DivisionUC();
            //firstDiv.Margin = new Thickness(leftMarginAmount, topMarginAmount, 0, 0);
            //divisionsHolderPanel.Children.Add(firstDiv);
            //divisionsModel.addDivision(firstDiv.Division);
        }

        private void addDivision()
        {
            divisionsHolderPanel.RowDefinitions.Add(new RowDefinition());

            DivisionUC divUC = new DivisionUC();
            divUC.divisionNumber.Content = divisionsModel.getSize() + 1;
            divUC.Margin = new Thickness(marginAmount, marginAmount, 0, 0);
            divUC.SetValue(Grid.RowProperty, divisionsModel.getSize());

            divisionsHolderPanel.Children.Add(divUC);
            divisionsModel.addDivision(divUC.Division);

            //Adds Children Listeners
            this.addListener(divUC);
        }

        public void removeDivision(int index)
        {
            for (int i = index; i < divisionsModel.getSize(); i++)
            {
                //DivisionUC div = divisionsHolderPanel.Children.
            }

            divisionsModel.removeDivision(index);
            divisionsHolderPanel.Children.RemoveAt(index);

            //You will need to remove listeners
            // listenerList.remove(DivisionUC);
        }

        private void addDivisionButton_Click(object sender, RoutedEventArgs e)
        {
            addDivision();
        }

        public void onListen(object pass)
        {
            shout(pass);
        }

        public void shout(object pass)
        {
            foreach (Listener list in listenerList)
            {
                list.onListen(pass);
            }
        }

        public void addListener(Listener list)
        {
            listenerList.Add(list);
        }
    }
}
