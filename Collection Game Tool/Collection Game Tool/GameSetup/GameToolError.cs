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

        private Dictionary<int, string> warningTemplates = new Dictionary<int, string>
        {
            {001,"{0} has no prize levels."},
            {002,"{0} is empty."},
            {003,"{0} is identical to {1}."},
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






        private List<string> unresolvedWarnings = new List<string>();
        private string _warningText;
        public string warningText
        {
            get
            {
                return _warningText;
            }
            set
            {
                _warningText = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("warningText"));
                }
            }
        }

        public void reportWarning(int warningCode, List<string> illegalObjects)
        {
            string theWarning = String.Format(warningTemplates[warningCode], illegalObjects.ToArray());
            if (!unresolvedWarnings.Contains(theWarning))
            {
                unresolvedWarnings.Add(theWarning);
                updateWarningText();
            }

        }

        public void resolveWarning(int warningCode, List<string> illegalObjects)
        {
            string theWarning = String.Format(warningTemplates[warningCode], illegalObjects);
            if (!unresolvedWarnings.Contains(theWarning))
            {
                unresolvedWarnings.Remove(theWarning);
                updateWarningText();
            }

        }

        private void updateWarningText()
        {
            string updatedWarningText = "";
            foreach (String s in unresolvedWarnings)
            {
                updatedWarningText += s;
                updatedWarningText += System.Environment.NewLine;
            }
            warningText = updatedWarningText;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
