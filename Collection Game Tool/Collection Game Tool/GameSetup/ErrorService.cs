using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Collection_Game_Tool.Services;

namespace Collection_Game_Tool.GameSetup
{
    public class ErrorService: INotifyPropertyChanged
    {
        private static ErrorService instance;
        
        private ErrorService() { }

        private static int currentId = 0;

        public static ErrorService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ErrorService();
                }
                return instance;
            }
        }

        //The possible errors the application can have
        private Dictionary<string, string> errorTemplates = new Dictionary<string, string>
        {
            {"001","{0} dun goofed. Fix it.\n"},
            {"002","{0} and {1} dun goofed. Fix it.\n"},
            {"004", "Prize Level {0} currently has a higher collection than Game Setup picks allows. ({1})\n"},
            {"005", "Prize Level {0} has illegal characters found in its collection text box!\n"},
            {"006", "Prize Level {0}'s collection text box is out of range! ({1}-{2})\n"},
            {"007", "Number of near win prizes is greater than the amount of Prize Levels.\n"},
            {"008", "Prize Level {0}'s collection text box cannot be empty.\n"},
            {"009", "Division {0} is not a unique Division.\n"},
            {"010", "The collections field in Division {0} needs to be less than or equal to the set player picks in the Game Setup.\n"},
            {"011", "The collection in Division {0} is invalid, the possible collection must be higher so that the division cannot win other prizes.\n"},
            {"012", "With the current setup the player cannot lose. Either decrease the amount of player picks, or increase the amount of collections one of the prize levels has."}
        };

        //The possible warnings the application can have
        private Dictionary<string, string> warningTemplates = new Dictionary<string, string>
        {
            {"001","{0} has no prize levels.\n"},
            {"002","{0} is empty.\n"},
            {"003","{0} is identical to {1}.\n"},
            {"004", "Prize Level {0} and Prize Level {1} are the same.\n"},
            {"005", "Division {0} has no selected prize levels.\n"},
            {"006", "There are no divisions in this project.\n"},
            {"007", "Division {0} will never have an instant win even though it has a Prize Level that can be an instant win."}
        };

        private Dictionary<Error,string> unresolvedErrors = new Dictionary<Error, string>();
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

        //Reports and error where errorCode is the unique key of the error from the dictionary, illegalObjects is the list of strings placed in the error string, and senderId is the unique Id from the object that is sending it, if senderId is null this will generate senderId
        public string reportError(string errorCode, List<string> illegalObjects, string senderId)
        {
            if(senderId == null) senderId = currentId++ + "";
            string theErrorMessage = String.Format(errorTemplates[errorCode], illegalObjects.ToArray());
            Error theError = new Error(senderId, errorCode);
            if (unresolvedErrors.ContainsKey(theError))
            {
                unresolvedErrors.Remove(theError);
            }
            unresolvedErrors.Add(theError, theErrorMessage);
            updateErrorText();
            
            return senderId;
        }

        //Resolves any error given the errorCode, which is the unique key of the error from the dictionary, and the senderId which is the unique Id from the object that is sending it, illegalObjects can be null (this is an artifact from an old build and should be removed in future builds)
        public void resolveError(string errorCode, List<string> illegalObjects, string senderId)
        {
            Error theError = new Error(senderId, errorCode);
            //string theErrorMessage = String.Format(errorTemplates[errorCode], illegalObjects);
            if (unresolvedErrors.ContainsKey(theError))
            {
                unresolvedErrors.Remove(theError);
                updateErrorText();
            }

        }

        private void updateErrorText()
        {
            string updatedErrorText = "";
            foreach(KeyValuePair<Error,string> entry in unresolvedErrors)
            {
                updatedErrorText += entry.Value;
                updatedErrorText += System.Environment.NewLine;
            }
            errorText = updatedErrorText;
        }


        private Dictionary<Warning, string> unresolvedWarnings = new Dictionary<Warning, string>();
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

        //Reports and error where warningCode is the unique key of the warning from the dictionary, illegalObjects is the list of strings placed in the warning string, and senderId is the unique Id from the object that is sending it, if senderId is null this will generate senderId
        public string reportWarning(string warningCode, List<string> illegalObjects, string senderId)
        {
            if (senderId == null) senderId = currentId++ + "";
            string theWarningMessage = String.Format(warningTemplates[warningCode], illegalObjects.ToArray());
            Warning theWarning = new Warning(senderId, warningCode);
            if (unresolvedWarnings.ContainsKey(theWarning))
            {
                unresolvedWarnings.Remove(theWarning);
            }
            unresolvedWarnings.Add(theWarning, theWarningMessage);
            updateWarningText();
            return senderId;
        }

        //Resolves any warning given the warningCode, which is the unique key of the warning from the dictionary, and the senderId which is the unique Id from the object that is sending it, illegalObjects can be null (this is an artifact from an old build and should be removed in future builds)
        public void resolveWarning(string warningCode, List<string> illegalObjects, string senderId)
        {
            Warning theWarning = new Warning(senderId, warningCode);
            //string theWarningMessage = String.Format(warningTemplates[warningCode], illegalObjects);
            if (unresolvedWarnings.ContainsKey(theWarning))
            {
                unresolvedWarnings.Remove(theWarning);
                updateWarningText();
            }

        }

        private void updateWarningText()
        {
            string updatedWarningText = "";
            foreach (KeyValuePair<Warning, string> entry in unresolvedWarnings)
            {
                updatedWarningText += entry.Value;
                updatedWarningText += System.Environment.NewLine;
            }
            warningText = updatedWarningText;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
