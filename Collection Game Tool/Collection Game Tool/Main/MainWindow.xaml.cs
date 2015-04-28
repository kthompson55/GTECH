using Collection_Game_Tool.Divisions;
using Collection_Game_Tool.GameSetup;
using Collection_Game_Tool.PrizeLevels;
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
using System.Windows.Shapes;

namespace Collection_Game_Tool.Main
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window, Listener
    {
        private GameSetupUC gs;
        public Window1()
        {
            InitializeComponent();
            UserControlPrizeLevels ucpl = new UserControlPrizeLevels();
            this.UserControls.Children.Add(ucpl);

            GameSetupUC gsuc = new GameSetupUC();
            gs = gsuc;
            this.UserControls.Children.Add(gsuc);

            DivisionPanelUC divUC = new DivisionPanelUC();
            this.UserControls.Children.Add(divUC);
        }

        public void onListen(object pass)
        {
            if(((String)pass).Equals("validate") && gs!=null)
            {
                gs.gsObject.canCreate=ServiceValidator.IsValid(this);
            }
        }
    }
}
