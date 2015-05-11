using Collection_Game_Tool.Main;
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
    [Serializable]
    public partial class DivisionPanelUC : UserControl, Listener, Teller
    {
        List<Listener> listenerList = new List<Listener>();
        public DivisionsModel divisionsList;
        private double marginAmount;
        public PrizeLevels.PrizeLevels prizes { get; set; }
        private const int MAX_DIVISIONS = 30;

        public DivisionPanelUC()
        {
            InitializeComponent();
            divisionsList = new DivisionsModel();
            marginAmount = 10;
            divisionsScroll.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            this.Loaded += new RoutedEventHandler(DivisionPanelUC_Loaded);
        }

        private void DivisionPanelUC_Loaded(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this.Parent);
            addListener((Window1)parentWindow);
        }

        public void addDivision()
        {
            if (divisionsList.getSize() < MAX_DIVISIONS)
            {
                DivisionUC divUC = new DivisionUC(prizes, divisionsList.getSize() + 1);
                divUC.DivModel.DivisionNumber = divisionsList.getSize() + 1;
                divUC.updateDivision();
                divUC.Margin = new Thickness(marginAmount, marginAmount, 0, 0);
                divUC.SectionContainer = this;

                divisionsHolderPanel.Children.Add(divUC);
                divisionsList.addDivision(divUC.DivModel);
                this.addListener(divUC);
            }

            if (divisionsList.getSize() >= MAX_DIVISIONS)
            {
                addDivisionButton.IsEnabled = false;
            }
        }

        public void loadInDivision(int number, DivisionModel div)
        {
            if (divisionsList.getSize() < MAX_DIVISIONS)
            {
                DivisionUC division = new DivisionUC(prizes, number);
                division.DivModel = div;
                division.setDataContextToModel();
                division.DivModel.DivisionNumber = number;
                division.DivModel.levelBoxes = div.levelBoxes;
                division.Margin = new Thickness(marginAmount, marginAmount, 0, 0);
                division.SectionContainer = this;

                division.setupLoadedDivision();
                division.updateDivision();

                divisionsHolderPanel.Children.Add(division);
                this.addListener(division);
            }

            if (divisionsList.getSize() >= MAX_DIVISIONS)
            {
                addDivisionButton.IsEnabled = false;
            }
        }

        public void removeDivision(int index)
        {
            for (int i = index; i < divisionsList.getSize(); i++)
            {
                DivisionUC div = (DivisionUC)divisionsHolderPanel.Children[i];
                div.DivModel.DivisionNumber = (int)div.DivModel.DivisionNumber - 1;
            }

            listenerList.Remove((DivisionUC)divisionsHolderPanel.Children[index]);
            divisionsList.removeDivision(index);
            divisionsHolderPanel.Children.RemoveAt(index);

            if (divisionsList.getSize() < MAX_DIVISIONS)
            {
                addDivisionButton.IsEnabled = true;
            }
        }

        private void addDivisionButton_Click(object sender, RoutedEventArgs e)
        {
            addDivision();
            divisionsScroll.ScrollToBottom();
        }

        //private void divisionHolder_SizeChanged(object sender, SizeChangedEventArgs e)
        //{
        //    determineScrollVisibility();
        //}

        public void validateDivision(DivisionUC divToCompare)
        {
            bool valid = true;
            for (int i = 0; i < divisionsHolderPanel.Children.Count && valid; i++)
            {
                DivisionUC div = (DivisionUC)divisionsHolderPanel.Children[i];
                if (divToCompare.DivModel.DivisionNumber != div.DivModel.DivisionNumber)
                {
                    bool isUnique = false;
                    for (int prizeIndex = 0; prizeIndex < prizes.getNumPrizeLevels() && !isUnique; prizeIndex++)
                    {
                        if (divToCompare.DivModel.levelBoxes[prizeIndex].IsSelected != div.DivModel.levelBoxes[prizeIndex].IsSelected)
                        {
                            isUnique = true;
                        }
                    }

                    if (!isUnique)
                    {
                        valid = false;
                    }
                }
            }

            if (valid)
            {
                shout("valid");
            }
            else
            {
                shout("error");
            }
        }

        public void onListen(object pass)
        {
            if (pass is PrizeLevels.PrizeLevels)
            {
                prizes = (PrizeLevels.PrizeLevels)pass;
            }
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
