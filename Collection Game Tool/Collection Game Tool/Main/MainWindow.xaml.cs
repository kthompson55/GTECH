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

            DivisionPanelUC divUC = new DivisionPanelUC();
            this.UserControls.Children.Add(divUC);
            divUC.prizes = pl.plsObject;

            
            //Listener stuff between divisions and Prize Levels
            pl.addListener(divUC);

            //Listener from GameSetup for player picks validation
            pl.addListener(gs);
            divUC.addListener(gs);

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

        public void onListen(object pass)
        {
            //Validate is used when something changes from any usercontrol, this is so it can check the validity of the variables within those controls
            //Also sets the create file button to invalid if anything the ServiceValidator finds is invalid (ex. CollectionNumber in PrizeLevels is greater then 20)
            if(((String)pass).Equals("validate") && gs!=null)
            {
                gs.gsObject.canCreate=ServiceValidator.IsValid(this);
            }   //Error is used when something that can't be easily validated with the above statement is invalid
            else if (((String)pass).Equals("error") && gs!=null)
            {
                gs.gsObject.canCreate = false;
            }   //Valid is used when something that can't be easily validated with the above statement is valid
            else if (((String)pass).Equals("valid") && gs != null)
            {
                gs.gsObject.canCreate = true;
            }   //generate/ is used to generate the final output file, this calls up the FileGenerationService to make the file
            else if (((String)pass).Contains("generate/") && gs != null)
            {
                String file = ((String)pass).Replace("generate/", "");
                FileGenerationService fgs = new FileGenerationService();
                fgs.buildGameData(null, pl.plsObject, gs.gsObject, file);
            }
        }
    }
}
