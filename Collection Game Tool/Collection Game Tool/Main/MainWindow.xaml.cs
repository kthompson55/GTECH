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
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;

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
        private ProjectData project;
        private string projectFileName;
        private bool isProjectSaved;
        private const string DEFAULT_EXT = ".cggproj";

        public Window1()
        {
            InitializeComponent();

            projectFileName = null;
            isProjectSaved = false;
            project = new ProjectData();

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
            toolMenu.Width = this.ActualWidth - 10;
        }

        private void Window_LayoutUpdated_1(object sender, EventArgs e)
        {
            double controlsHeight = this.ActualHeight - toolMenu.ActualHeight - windowHeader.ActualHeight - 35;
            if (controlsHeight < 0) controlsHeight = 0;
            pl.Height = controlsHeight;
            gs.Height = controlsHeight;
            divUC.Height = controlsHeight;
            divUC.divisionsScroll.MaxHeight = ((controlsHeight - 130) > 0) ? controlsHeight - 130 : 0;
            toolMenu.Width = this.ActualWidth - 10;
            divUC.determineScrollVisibility();
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

        private void SaveItem_Clicked(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("SaveItem_Clicked");
            SaveProject();
        }

        private void SaveAsItem_Clicked(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("SaveAsItem_Clicked");
            SaveProjectAs();
        }

        private void OpenItem_Clicked(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("OpenItem_Clicked");
            OpenProject();
        }

        private void SaveProject()
        {
            if (isProjectSaved)
            {
                project.currentGameSetup = gs.gsObject;
                project.currentPrizeLevels = pl.plsObject;
                project.currentDivisions = divUC.divisionsList;

                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(projectFileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
                formatter.Serialize(stream, project);
                stream.Close();
            }
            else
            {
                SaveProjectAs();
            }
        }

        private void SaveProjectAs()
        {
            Microsoft.Win32.SaveFileDialog dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.DefaultExt = DEFAULT_EXT;
            dialog.Filter = "Collection Game Generator Project (" + DEFAULT_EXT + ")|*"+DEFAULT_EXT;
            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                projectFileName = dialog.FileName;
                isProjectSaved = true;
                SaveProject();
            }
        }

        private void OpenProject()
        {
            Microsoft.Win32.OpenFileDialog openDialog = new Microsoft.Win32.OpenFileDialog();
            openDialog.DefaultExt = DEFAULT_EXT;
            openDialog.Filter = "Collection Game Generator Project (" + DEFAULT_EXT + ")|*"+DEFAULT_EXT;
            Nullable<bool> result = openDialog.ShowDialog();
            bool isCorrectFileType = System.Text.RegularExpressions.Regex.IsMatch(openDialog.FileName, DEFAULT_EXT);

            if (result == true && isCorrectFileType)
            {
                isProjectSaved = true;
                projectFileName = openDialog.FileName;

                IFormatter format = new BinaryFormatter();
                Stream stream = new FileStream(projectFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                project = (ProjectData)format.Deserialize(stream);

                PrizeLevels.PrizeLevels prizes = project.currentPrizeLevels;
                for (int i = pl.plsObject.getNumPrizeLevels(); i < prizes.getNumPrizeLevels(); i++)
                {
                    pl.Add_Prize_Level(null, null);
                }
                for (int i = 0; i < prizes.getNumPrizeLevels(); i++)
                {
                    pl.plsObject.getPrizeLevel(i).prizeValue = prizes.getPrizeLevel(i).prizeValue;
                    pl.plsObject.getPrizeLevel(i).numCollections = prizes.getPrizeLevel(i).numCollections;
                    pl.plsObject.getPrizeLevel(i).isInstantWin = prizes.getPrizeLevel(i).isInstantWin;
                }

                GameSetupModel setup = project.currentGameSetup;
                gs.TotalPicksSlider.Value = setup.totalPicks;
                gs.gsObject.totalPicks = setup.totalPicks;
                gs.NearWinCheckbox.IsChecked = setup.isNearWin;
                gs.gsObject.isNearWin = setup.isNearWin;
                gs.NumNearWinsSlider.Value = setup.nearWins;
                gs.gsObject.nearWins = setup.nearWins;
                gs.MaxPermutationsTextBox.Text = setup.maxPermutations.ToString();
                gs.gsObject.maxPermutations = setup.maxPermutations;
                gs.gsObject.canCreate = setup.canCreate;

                divUC.divisionsList = project.currentDivisions;
                divUC.prizes = project.currentPrizeLevels;
                divUC.divisionsHolderPanel.Children.Clear();

                for (int i = 0; i < divUC.divisionsList.getSize(); i++)
                {
                    divUC.loadInDivision(i + 1, divUC.divisionsList.divisions[i]);
                    ((DivisionUC)divUC.divisionsHolderPanel.Children[i]).updateDivision();
                }

            }
            else if (result == true && !isCorrectFileType)
            {
                System.Windows.MessageBox.Show("The file must be of type .cggproj");
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = System.Windows.MessageBox.Show("Would you like to save the project's data before exiting?", "Exiting Application", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                SaveProject();
            }
            else if (result == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
        }
    }
}