using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Collection_Game_Tool.GameSetup
{
    public class GameToolError: INotifyPropertyChanged
    {
        private static GameToolError instance;

        private GameToolError() { }

        public static GameToolError Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameToolError();
                }
                return instance;
            }
        }

        private Dictionary<int, string> errorTemplates = new Dictionary<int, string>
        {
            {001,"{0} dun goofed. Fix it."},
            {002,"{0} and {1} dun goofed. Fix it."},
            {420, "{0} is blazing it. #YOLO"}

        };

        private List<string> unresolvedErrors = new List<string>();
        private string _errorText;
        public string errorText
        {
            get
            {
                return _errorText;
            }
            set
            {
                _errorText = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("errorText"));
                }
            }
        }

        public void reportError(int errorCode, List<string> illegalObjects)
        {
            string theError = String.Format(errorTemplates[errorCode], illegalObjects.ToArray());
            if(!unresolvedErrors.Contains(theError))
            {
                unresolvedErrors.Add(theError);
                updateErrorText();
            }

        }

        public void resolveError(int errorCode, List<string> illegalObjects)
        {
            string theError = String.Format(errorTemplates[errorCode], illegalObjects);
            if (!unresolvedErrors.Contains(theError))
            {
                unresolvedErrors.Remove(theError);
                updateErrorText();
            }

        }

        private void updateErrorText()
        {
            string updatedErrorText = "";
            foreach(String s in unresolvedErrors)
            {
                updatedErrorText += s;
                updatedErrorText += System.Environment.NewLine;
            }
            errorText = updatedErrorText;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
