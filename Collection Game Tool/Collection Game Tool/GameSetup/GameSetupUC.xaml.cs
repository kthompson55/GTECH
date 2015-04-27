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
using Collection_Game_Tool.Services;

namespace Collection_Game_Tool.GameSetup
{
    /// <summary>
    /// Interaction logic for GameSetupUC.xaml
    /// </summary>
    public partial class GameSetupUC : UserControl
    {

        public GameSetupModel gsObject;
        List<Listener> listenerList = new List<Listener>();
        private string lastAcceptableMaxPermutationValue = 0 + "";

        public GameSetupUC()
        {
            InitializeComponent();
            gsObject = new GameSetupModel();
            gsObject.canCreate = true;
            CreateButton.DataContext = gsObject;
        }

        //When Create is clicked, validates data and creates a text file
        public void createButton_Click(object sender, RoutedEventArgs e)
        {
            //validate data
            //open save dialog
            openSaveWindow();

        }

        private void openSaveWindow()
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "CollectionGameFile"; // Default file name
            dlg.DefaultExt = ".txt"; // Default file extension
            dlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string filename = dlg.FileName;

            }
        }

        private void TotalPicksSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (gsObject != null)
            {
                Slider slider = sender as Slider;
                gsObject.totalPicks = Convert.ToInt16(slider.Value);
            }
        }

        private void NumNearWinsSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (gsObject != null)
            {
                Slider slider = sender as Slider;
                gsObject.nearWins = Convert.ToInt16(slider.Value);
            }
        }

        private void NearWinCheckbox_Click(object sender, RoutedEventArgs e)
        {
            gsObject.toggleNearWin();
        }

        private void MaxPermutationsTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (gsObject != null)
            {
                TextBox textBox = sender as TextBox;
                if (textBox.Text == "")
                {
                    textBox.Text = 0 +"";
                }
                else if (!WithinPermutationRange(textBox.Text))
                {
                    textBox.Text = lastAcceptableMaxPermutationValue;
                }
                else
                {
                    gsObject.maxPermutations = Convert.ToUInt32(textBox.Text);
                }
            }
        }

        private void MaxPermutationsTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            textBox.SelectAll();
        }

        private void MaxPermutationsTextBox_GotMouseCapture(object sender, MouseEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            textBox.SelectAll();
        }

        private void MaxPermutationsTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox tb = sender as TextBox;
            lastAcceptableMaxPermutationValue = tb.Text;
           
        }

        private bool WithinPermutationRange(string s)
        {
            uint philTheOrphan;
            return UInt32.TryParse(s, out philTheOrphan);
        }
    }
}
