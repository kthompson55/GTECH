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
    public partial class Window1 : Window, INotifyPropertyChanged, Listener
    {
        public Window1()
        {
            InitializeComponent();
            canCreate = true;
            CreateButton.DataContext = this;
        }

        private bool _canCreate;
        public bool canCreate
        {
            get
            {
                return _canCreate;
            }
            set
            {
                _canCreate = value;

                if(PropertyChanged!=null)
                    PropertyChanged(this, new PropertyChangedEventArgs("canCreate"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void onListen(object pass)
        {
            if(((String)pass).Equals("validate"))
            {
                canCreate=ServiceValidator.IsValid(this);
            }
        }
    }
}
