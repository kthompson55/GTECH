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
    public partial class UserControlPrizeLevel : UserControl, Teller, Listener, IComparable
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
            shout(this);
        }

        private void instantWinChangedEventHandler(object sender, RoutedEventArgs args)
        {
            plObject.isInstantWin = (bool)InstantWinCheckBox.IsChecked;

            shout("Instant Win");

            boxSelected();
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
            }
        }

        private void valueChangedEventHandler(object sender, TextChangedEventArgs args)
        {
            double tryDub = 0;
            if (Double.TryParse(TextBoxValue.Text, out tryDub))
            {
                if (tryDub > 0)
                {
                    plObject.prizeValue = tryDub;
                }
                else
                {
                    plObject.prizeValue = 1;
                    TextBoxValue.Text = "1";
                }

                shout("Value");

                boxSelected();
            }
        }

        private void collectionsChangedEventHandler(object sender, TextChangedEventArgs args)
        {
            int tryColl = 0;
            if (int.TryParse(CollectionBoxValue.Text, out tryColl))
            {
                if (tryColl > 0)
                {
                    plObject.numCollections = tryColl;
                }
                else
                {
                    plObject.numCollections = 1;
                    CollectionBoxValue.Text = "1";
                }

                shout("Collection");

                boxSelected();
            }
        }

        private void boxSelected()
        {
            LevelGrid.Background = Brushes.Orange;
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
