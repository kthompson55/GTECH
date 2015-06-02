using Collection_Game_Tool.GameSetup;
using Collection_Game_Tool.Main;
using Collection_Game_Tool.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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
    public partial class UserControlPrizeLevel : UserControl, Teller, IComparable
    {
        public PrizeLevel plObject { get; set; }
        List<Listener> listenerList = new List<Listener>();
        private string ucplID = null;

        public UserControlPrizeLevel()
        {
            InitializeComponent();
            //Sets up the model object and the data context for the binding in the xaml
            plObject = new PrizeLevel();
            setDataContext();
            plObject.isInstantWin = false;
            plObject.numCollections = 2;
            plObject.prizeValue = 0;

            this.Loaded += new RoutedEventHandler(MainView_Loaded);
        }

        //Setups up datacontext stuff
        public void setDataContext()
        {
            Level.DataContext = plObject;
            TextBoxValue.DataContext = plObject;
            CollectionBoxValue.DataContext = plObject;
            InstantWinCheckBox.DataContext = plObject;
        }

        //After view is loaded
        void MainView_Loaded(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this.Parent);
            //Listens to the PrizeLevels window
            plObject.addListener((Window1)parentWindow);
        }

        public void Close_Prize_Level(object sender, RoutedEventArgs e)
        {
            //Shouts itself to PrizeLevels so PrizeLevels can close the individual PrizeLevel
            shout(this);
            shout("Update");
        }

        private void boxChangedEventHandler(object sender, RoutedEventArgs args)
        {
            //Shouts update to PrizeLevels so PrizeLevels can update the order of individual PrizeLevel if needed
            boxSelected();
        }

        private void TextBox_Focus(object sender, RoutedEventArgs e)
        {
            //Focuses on the textbox that was selected in the prize level user control
            TextBox textBox = sender as TextBox;
            textBox.SelectAll();
            boxSelected();
        }

        private void TextBox_MouseCapture(object sender, MouseEventArgs e)
        {
            //Basic selecting
            TextBox textBox = sender as TextBox;
            textBox.SelectAll();
        }

        public void shout(object pass)
        {
            foreach (Listener ucpls in listenerList)
            {
                ucpls.onListen(pass);
            }
        }

        //Highlights box so that user can see it is currently being used
        private void boxSelected()
        {
            validateMyself();
            shout("Update");
            LevelGrid.Background = Brushes.Yellow;
        }

        public void addListener(Listener list)
        {
            listenerList.Add(list);
        }

        //Makes sure text entered is okay
        private void textBoxValue_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            shout("Update");
            e.Handled = !TextBoxTextAllowed(e.Text);
        }

        //Makes sure text entered is okay
        private void textBoxCollection_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            shout("Update");
            e.Handled = !CollectionBoxTextAllowed(e.Text);
        }

        //Makes sure text entered is okay
        private bool TextBoxTextAllowed(string p)
        {
            shout("Update");
            return Array.TrueForAll<Char>(p.ToCharArray(), delegate(Char c) { return Char.IsDigit(c) || Char.IsControl(c) || c.Equals('.'); });
        }

        //Makes sure text entered is okay
        private bool CollectionBoxTextAllowed(string p)
        {
            shout("Update");
            return Array.TrueForAll<Char>(p.ToCharArray(), delegate(Char c) { return Char.IsDigit(c) || Char.IsControl(c); });
        }

        //Compares User Control Prize Level based of Prize Level value
        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;

            UserControlPrizeLevel compare = obj as UserControlPrizeLevel;

            return compare.plObject.prizeValue.CompareTo(plObject.prizeValue);
        }

        //Highlights box after text is changed in a prize level
        private void Text_Changed(object sender, TextChangedEventArgs e)
        {
            boxSelected();
        }

        private void validateMyself()
        {
            if (Validation.GetHasError(CollectionBoxValue))
            {
                RangeRule rr = new RangeRule();
                ValidationResult vr = rr.Validate(CollectionBoxValue.Text, new CultureInfo("en-US", false));

                PrizeLevelConverter plc = new PrizeLevelConverter();
                //Checks if illegal characters were found in a text box
                if (vr.Equals(new ValidationResult(false, "Illegal characters")))
                {
                    ucplID = ErrorService.Instance.reportError("005", new List<string>{
                        (string)plc.Convert(plObject.prizeLevel,typeof(string), null, new System.Globalization.CultureInfo("en-us"))
                    }, ucplID);
                }
                else if (vr.Equals(new ValidationResult(false, "Please enter a number in the given range.")))
                {   //Check if number in a textbox is out of range
                    ucplID = ErrorService.Instance.reportError("006", new List<string>{
                        (string)plc.Convert(plObject.prizeLevel,typeof(string), null, new System.Globalization.CultureInfo("en-us")),
                        "0",
                        "20"
                    }, ucplID);
                }
                else if(vr.Equals(new ValidationResult(false, "Cannot be nothing")))
                {   //Check is nothing was entered into the textbox
                    ucplID = ErrorService.Instance.reportError("008", new List<string>{
                        (string)plc.Convert(plObject.prizeLevel, typeof(string),null, new System.Globalization.CultureInfo("en-us"))
                    },
                    ucplID);
                }
            }
            else
            {
                //Resolves all errors if none were found
                ErrorService.Instance.resolveError("005", null, ucplID);
                ErrorService.Instance.resolveError("006", null, ucplID);
                ErrorService.Instance.resolveError("008", null, ucplID);
            }

            //Shouts update to sort the prize levels
            shout("Update");
        }
    }
}
