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
    public partial class DivisionUC : UserControl, Listener, IComparable
    {
        public DivisionModel DivModel { get; set; }
        public PrizeLevels.PrizeLevels Prizes { get; set; }
        public DivisionPanelUC SectionContainer { get; set; }
        public List<PrizeLevelBox> PrizeBoxes { get; set; }
        public const int MAX_PRIZE_BOXES = 12;

        public DivisionUC(PrizeLevels.PrizeLevels initialPrizeLevels, int number)
        {
            InitializeComponent();
            DivModel = new DivisionModel();
            Prizes = initialPrizeLevels;
            PrizeBoxes = new List<PrizeLevelBox>();
            totalPicksLabel.DataContext = DivModel;
            totalValueLabel.DataContext = DivModel;
            divisionNumberLabel.DataContext = DivModel;
            DivModel.DivisionNumber = number;

            for (int i = 0; i < MAX_PRIZE_BOXES; i++)
            {
                PrizeLevelBox box = new PrizeLevelBox(this, false, i+1);
                PrizeBoxes.Add(box);
                prizeLevelsGrid.Children.Add(PrizeBoxes[i]);
            }

            for (int i = 0; i < Prizes.getNumPrizeLevels(); i++)
            {
                PrizeBoxes[i].IsAvailable = true;
                PrizeBoxes[i].IsSelected = false;
            }
        }

        private void clearDivisionButton_Click(object sender, RoutedEventArgs e)
        {
            DivModel.clearPrizeLevelList();
            for (int i = 0; i < MAX_PRIZE_BOXES; i++)
            {
                PrizeBoxes[i].IsSelected = false;
            }

            DivModel.TotalPlayerPicks = DivModel.calculateTotalCollections();
            DivModel.TotalPrizeValue = DivModel.calculateDivisionValue();
        }

        private void deleteDivisionButton_Click(object sender, RoutedEventArgs e)
        {
            int index = getIndex();
            SectionContainer.removeDivision(index);
        }

        public void updateInfo()
        {
            if (Prizes.getNumPrizeLevels() > 0)
            {
                DivModel.clearPrizeLevelList();
                for (int i = 0; i < Prizes.getNumPrizeLevels(); i++)
                {
                    if (PrizeBoxes[i].IsSelected)
                    {
                        DivModel.addPrizeLevel(Prizes.getPrizeLevel(i));
                    }
                }

                DivModel.TotalPlayerPicks = DivModel.calculateTotalCollections();
                DivModel.TotalPrizeValue = DivModel.calculateDivisionValue();
            }

            SectionContainer.validateDivision(this);
        }

        public void updateDivision()
        {
            if (Prizes.getNumPrizeLevels() > 0)
            {
                for (int i = 0; i < MAX_PRIZE_BOXES; i++)
                {
                    if (PrizeBoxes[i].IsAvailable && PrizeBoxes[i].IsSelected)
                    {
                        DivModel.addPrizeLevel(Prizes.getPrizeLevel(i));
                    }
                    else
                    {
                        PrizeBoxes[i].IsSelected = false;
                    }
                }

                DivModel.TotalPlayerPicks = DivModel.calculateTotalCollections();
                DivModel.TotalPrizeValue = DivModel.calculateDivisionValue();
            }
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

                for (int i = 0; i < MAX_PRIZE_BOXES; i++)
                {
                    PrizeBoxes[i].IsAvailable = false;
                }

                for (int i = 0; i < Prizes.getNumPrizeLevels(); i++)
                {
                    PrizeBoxes[i].IsAvailable = true;
                }

                DivModel.clearPrizeLevelList();
                updateDivision();
            }
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            DivisionUC div = (DivisionUC)obj;

            return div.DivModel.CompareTo(this.DivModel);
        }
    }
}