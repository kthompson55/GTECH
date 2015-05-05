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
using System.Windows.Forms;

namespace Collection_Game_Tool.Main
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window, Listener
    {
        private UserControlPrizeLevels pl;
        private GameSetupUC gs;
        private DivisionPanelUC divUC;
        public Window1()
        {
            InitializeComponent();

            //Programmaticaly add UserControls to mainwindow.
            //Did this because couldn't find a way to access the usercontrol from within the xaml.
            UserControlPrizeLevels ucpl = new UserControlPrizeLevels();
            pl = ucpl;
            this.UserControls.Children.Add(ucpl);

            GameSetupUC gsuc = new GameSetupUC();
            gs = gsuc;
            this.UserControls.Children.Add(gsuc);

            divUC = new DivisionPanelUC();
            this.UserControls.Children.Add(divUC);
            divUC.prizes = pl.plsObject;

            
            //Listener stuff between divisions and Prize Levels
            pl.addListener(divUC);

            //Listeners for GameSetup so they can see player picks for validation
            gs.addListener(pl);
            gs.addListener(divUC);
            
            this.Loaded += new RoutedEventHandler(MainWindow_Loaded);

            Screen screen = System.Windows.Forms.Screen.FromHandle(new System.Windows.Interop.WindowInteropHelper(this).Handle);
            this.MaxHeight = screen.WorkingArea.Height;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.MaxWidth = this.Width;
            this.MinWidth = this.Width;
        }

        private void Window_LayoutUpdated_1(object sender, EventArgs e)
        {
            double controlsHeight = this.ActualHeight - windowHeader.ActualHeight - 35;
            if (controlsHeight < 0) controlsHeight = 0;
            pl.Height = controlsHeight;
            gs.Height = controlsHeight;
            divUC.Height = controlsHeight;
            divUC.divisionsScroll.MaxHeight = ((divUC.ActualHeight - 125) > 0) ? divUC.ActualHeight - 125 : 0;
        }

        public void onListen(object pass)
        {
            if (pass is String)
            {
                if (((String)pass).Contains("generate/") && gs != null)
                {
                    String file = ((String)pass).Replace("generate/", "");
                    FileGenerationService fgs = new FileGenerationService();
                    fgs.buildGameData(divUC.divisionsList, pl.plsObject, gs.gsObject, file);
                }
            }
            if (pass is int)
            {
                gs.pickCheck = (int)pass;
                pl.collectionCheck = (int)gs.TotalPicksSlider.Value;
            }
        }
    }
}