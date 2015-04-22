using Collection_Game_Tool.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Collection_Game_Tool.PrizeLevels
{
    /// <summary>
    /// Interaction logic for UserControlPrizeLevel.xaml
    /// </summary>
    public partial class UserControlPrizeLevel : UserControl, Teller, Listener, INotifyPropertyChanged, IComparable
    {
        public PrizeLevel plObject;
        List<Listener> listenerList = new List<Listener>();


        public UserControlPrizeLevel()
        {
            InitializeComponent();
            plObject = new PrizeLevel();
            TextBoxValue.DataContext = plObject;
            plObject.addListener(this);
            plObject.isInstantWin = false;
            plObject.numCollections = 0;
            plObject.prizeValue = 0;
        }

        public void Close_Prize_Level(object sender, RoutedEventArgs e)
        {
            plObject = null;
            shout(this);
        }

        public void shout(object pass)
        {
            foreach (UserControlPrizeLevels ucpls in listenerList)
            {
                ucpls.onListen(pass);
            }
        }

        public void addListener(Listener list)
        {
            listenerList.Add(list);
        }

        public void onListen(object pass)
        {
            String parse = (String)pass;

            if (parse.Equals("Level"))
                Level.Content = plObject.currentLevel();
            else if (parse.Equals("Value"))
            {
                shout("Value");
                NotifyPropertyChanged("Value");
            }
        }

        private void valueChangedEventHandler(object sender, TextChangedEventArgs args)
        {
            double tryDub = 0;
            if (Double.TryParse(TextBoxValue.Text, out tryDub))
            {
                plObject.prizeValue = tryDub;
                shout("Value");
                NotifyPropertyChanged("Value");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;

            UserControlPrizeLevel compare = obj as UserControlPrizeLevel;

            return compare.plObject.prizeValue.CompareTo(plObject.prizeValue);
        }
    }
}
