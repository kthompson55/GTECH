using Collection_Game_Tool.Divisions;
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

namespace Collection_Game_Tool.PrizeLevels
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControlPrizeLevels : UserControl, Listener, Teller
    {
        List<Listener> listenerList = new List<Listener>();
        public PrizeLevels plsObject;

        public UserControlPrizeLevels()
        {
            InitializeComponent();
            plsObject = new PrizeLevels();

            UserControlPrizeLevel ucpl = new UserControlPrizeLevel();
            ucpl.addListener(this);
            Prizes.Children.Add(ucpl);
            plsObject.addPrizeLevel(ucpl.plObject);
            ucpl.plObject.prizeLevel=1;
            ucpl.CloseButton.IsEnabled = false;
            ucpl.CloseButton.Opacity = 0.0f;

            UserControlPrizeLevel ucpl2 = new UserControlPrizeLevel();
            ucpl2.Margin = new Thickness(0, Prizes.Children.Count * 50, 0, 0);
            ucpl2.addListener(this);
            Prizes.Children.Add(ucpl2);
            plsObject.addPrizeLevel(ucpl2.plObject);
            ucpl2.plObject.prizeLevel = Prizes.Children.Count;
            ucpl2.CloseButton.IsEnabled = false;
            ucpl2.CloseButton.Opacity = 0.0f;
        }

        public void Add_Prize_Level(object sender, RoutedEventArgs e)
        {
            if (plsObject.getNumPrizeLevels() < 12)
            {
                UserControlPrizeLevel ucpl = new UserControlPrizeLevel();
                ucpl.Margin = new Thickness(0, Prizes.Children.Count * 50, 0, 0);

                ucpl.addListener(this);
                Prizes.Children.Add(ucpl);
                plsObject.addPrizeLevel(ucpl.plObject);
                ucpl.plObject.prizeLevel = Prizes.Children.Count;
            }

            for (int i = 0; i < Prizes.Children.Count; i++)
            {
                UserControlPrizeLevel ucpl = (UserControlPrizeLevel)Prizes.Children[i];
                ucpl.LevelGrid.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#858585"));
                ucpl.Margin = new Thickness(0, i * 50, 0, 0);
                ucpl.plObject.prizeLevel = (i + 1);

                ucpl.CloseButton.IsEnabled = true;
                ucpl.CloseButton.Opacity = 1;
            }

            if (plsObject.getNumPrizeLevels() == 12)
            {
                AddButton.IsEnabled = false;
                AddButton.Opacity = 0.3;
            }
        }

        public void onListen(object pass)
        {
            if (pass is string)
            {
                String parse=(String)pass;
                if (parse.Equals("Update"))
                {
                    List<UserControlPrizeLevel> ucplList = new List<UserControlPrizeLevel>();
                    ucplList = Prizes.Children.Cast<UserControlPrizeLevel>().ToList<UserControlPrizeLevel>();
                    Prizes.Children.Clear();

                    ucplList.Sort();

                    for (int i = 0; i < ucplList.Count; i++ )
                    {
                        ucplList[i].LevelGrid.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#858585"));
                        ucplList[i].Margin = new Thickness(0, i*50, 0, 0);
                        ucplList[i].plObject.prizeLevel = (i + 1);
                        Prizes.Children.Add(ucplList[i]);
                    }
                }
            }
            else if(pass is UserControlPrizeLevel)
            {
                if (plsObject.getNumPrizeLevels() > 2)
                {
                    UserControlPrizeLevel rem = (UserControlPrizeLevel)pass;

                    int index = -1;
                    for (int i = 0; i < Prizes.Children.Count && index < 0; i++)
                    {
                        UserControlPrizeLevel ucpl = (UserControlPrizeLevel)Prizes.Children[i];
                        if (ucpl == rem)
                            index = i;
                    }

                    rem.plObject = null;
                    Prizes.Children.Remove(rem);
                    plsObject.removePrizeLevel(index);

                    rem = null;

                    for (int i = 0; i < Prizes.Children.Count; i++)
                    {
                        UserControlPrizeLevel ucpl = (UserControlPrizeLevel)Prizes.Children[i];
                        ucpl.LevelGrid.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#858585"));
                        ucpl.Margin = new Thickness(0, i * 50, 0, 0);
                        ucpl.plObject.prizeLevel = (i + 1);

                        if (plsObject.getNumPrizeLevels() == 2)
                        {
                            ucpl.CloseButton.IsEnabled = false;
                            ucpl.CloseButton.Opacity = 0.0f;
                        }
                        else
                        {
                            ucpl.CloseButton.IsEnabled = true;
                            ucpl.CloseButton.Opacity = 1;
                        }
                    }

                    AddButton.IsEnabled = true;
                    AddButton.Opacity = 1;
                }
            }
            shout(this);
        }

        public void shout(object pass)
        {
            foreach (Listener l in listenerList)
            {
                l.onListen(pass);
            }
        }

        public void addListener(Listener list)
        {
            listenerList.Add(list);
        }
    }
}
