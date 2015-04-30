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
using System.ComponentModel;

namespace Collection_Game_Tool.Divisions
{
    /// <summary>
    /// Interaction logic for PrizeLevelBox.xaml
    /// </summary>
    public partial class PrizeLevelBox : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public DivisionUC division;
        private bool _isSelected;
        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                _isSelected = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("IsSelected"));
            }
        }

        private bool _isAvailable;
        public bool IsAvailable
        {
            get
            {
                return _isAvailable;
            }
            set
            {
                _isAvailable = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("IsAvailable"));
            }
        }

        private int _prizeBoxLevel;
        public int PrizeBoxLevel 
        {
            get
            {
                return _prizeBoxLevel;
            }
            set
            {
                _prizeBoxLevel = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("PrizeBoxLevel"));
            }
        }

        public PrizeLevelBox(DivisionUC div, bool available, int level)
        {
            InitializeComponent();
            levelBox.DataContext = this;
            prizeLevelLabel.DataContext = this;

            this.division = div;
            IsSelected = false;
            IsAvailable = available;
            PrizeBoxLevel = level;
        }

        private void levelBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            IsSelected = !IsSelected;
            division.updateInfo();
        }

    }
}
