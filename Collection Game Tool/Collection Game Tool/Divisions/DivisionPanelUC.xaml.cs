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
        public DivisionsModel divisionsList;
        private double marginAmount;
        public PrizeLevels.PrizeLevels prizes { get; set; }

        public DivisionPanelUC()
        {
            InitializeComponent();
            divisionsList = new DivisionsModel();
            marginAmount = 10;
        }

        private void addDivision()
        {
            DivisionUC divUC = new DivisionUC();
            divUC.divisionNumber.Content = divisionsList.getSize() + 1;
            divUC.Margin = new Thickness(marginAmount, marginAmount, 0, 0);
            divUC.SectionContainer = this;
            
            divisionsHolderPanel.Children.Add(divUC);
            divisionsList.addDivision(divUC.Division);
            this.addListener(divUC);
        }

        public void removeDivision(int index)
        {
            for (int i = index; i < divisionsList.getSize(); i++)
            {
                DivisionUC div = (DivisionUC)divisionsHolderPanel.Children[i];
                div.divisionNumber.Content = (int)div.divisionNumber.Content - 1;
            }

            listenerList.Remove((DivisionUC)divisionsHolderPanel.Children[index]);
            divisionsList.removeDivision(index);
            divisionsHolderPanel.Children.RemoveAt(index);
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
