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
    public partial class UserControlPrizeLevels : UserControl, Listener
    {
        PrizeLevels plsObject;

        public UserControlPrizeLevels()
        {
            InitializeComponent();
            plsObject = new PrizeLevels();

            UserControlPrizeLevel ucpl = new UserControlPrizeLevel();
            ucpl.addListener(this);
            Prizes.Children.Add(ucpl);
            plsObject.addPrizeLevel(ucpl.plObject);
            ucpl.plObject.prizeLevel=1;
        }

        public void Add_Prize_Level(object sender, RoutedEventArgs e)
        {
            UserControlPrizeLevel ucpl = new UserControlPrizeLevel();
            ucpl.Margin = new Thickness(0,Prizes.Children.Count * 90,0,0);

            ucpl.addListener(this);
            Prizes.Children.Add(ucpl);
            plsObject.addPrizeLevel(ucpl.plObject);
            ucpl.plObject.prizeLevel = Prizes.Children.Count;
        }

        public void onListen(object pass)
        {
            if (pass is string)
            {
                String parse=(String)pass;
                if (parse.Equals("Value"))
                {
                    List<UserControlPrizeLevel> ucplList = new List<UserControlPrizeLevel>();
                    ucplList = Prizes.Children.Cast<UserControlPrizeLevel>().ToList<UserControlPrizeLevel>();
                    Prizes.Children.Clear();

                    ucplList.Sort();

                    for (int i = 0; i < ucplList.Count; i++ )
                    {
                        ucplList[i].Margin = new Thickness(0, i*90, 0, 0);
                        ucplList[i].plObject.prizeLevel = (i + 1);
                        Prizes.Children.Add(ucplList[i]);
                    }
                }
            }
            else
            {
                UserControlPrizeLevel rem = (UserControlPrizeLevel)pass;

                int index = -1;
                for (int i = 0; i < Prizes.Children.Count && index < 0; i++)
                {
                    UserControlPrizeLevel ucpl = (UserControlPrizeLevel)Prizes.Children[i];
                    if (ucpl == rem)
                        index = i;
                }

                Prizes.Children.Remove(rem);
                plsObject.removePrizeLevel(index);

                rem = null;

                for (int i = 0; i < Prizes.Children.Count; i++)
                {
                    UserControlPrizeLevel ucpl = (UserControlPrizeLevel)Prizes.Children[i];
                    ucpl.Margin = new Thickness(0, i * 90, 0, 0);
                    ucpl.plObject.prizeLevel = (i + 1);
                }
            }
        }
    }
}
