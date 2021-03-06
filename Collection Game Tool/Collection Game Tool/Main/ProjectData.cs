﻿using Collection_Game_Tool.Divisions;
using Collection_Game_Tool.GameSetup;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Collection_Game_Tool.Main
{
    [Serializable]
    class ProjectData
    {
        public PrizeLevels.PrizeLevels savedPrizeLevels;
        public GameSetupModel savedGameSetup;
        public DivisionsModel savedDivisions;

        [NonSerialized]
        private String projectFileName;
        [NonSerialized]
        public bool isProjectSaved;
        [NonSerialized]
        private const string DEFAULT_EXT = ".cggproj";
        [NonSerialized]
        private const string FILTER = "Collection Game Generator Project (" + DEFAULT_EXT + ")|*" + DEFAULT_EXT;

        public ProjectData()
        {
            projectFileName = null;
            isProjectSaved = false;
        }

        public void SaveProject(GameSetupModel gsObject, PrizeLevels.PrizeLevels plsObject, DivisionsModel divisionsList)
        {
            if (isProjectSaved)
            {
                savedGameSetup = gsObject;
                savedPrizeLevels = plsObject;
                savedDivisions = divisionsList;

                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(projectFileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
                formatter.Serialize(stream, this);
                stream.Close();
            }
            else
            {
                SaveProjectAs(gsObject, plsObject, divisionsList);
            }
        }

        public void SaveProjectAs(GameSetupModel gsObject, PrizeLevels.PrizeLevels plsObject, DivisionsModel divisionsList)
        {
            Microsoft.Win32.SaveFileDialog dialog = new Microsoft.Win32.SaveFileDialog();
            if (String.IsNullOrEmpty(projectFileName))
            {
                dialog.FileName = "CollectionGameGeneratorProject" + DEFAULT_EXT;
            }
            else
            {
                dialog.FileName = projectFileName;
            }
            dialog.DefaultExt = DEFAULT_EXT;
            dialog.Filter = FILTER;
            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                projectFileName = dialog.FileName;
                isProjectSaved = true;
                SaveProject(gsObject, plsObject, divisionsList);
            }
        }

        public bool OpenProject()
        {
            bool loadSuccessful = true;
            Microsoft.Win32.OpenFileDialog openDialog = new Microsoft.Win32.OpenFileDialog();
            openDialog.DefaultExt = DEFAULT_EXT;
            openDialog.Filter = FILTER;
            bool? result = openDialog.ShowDialog();
            bool isCorrectFileType = System.Text.RegularExpressions.Regex.IsMatch(openDialog.FileName, DEFAULT_EXT);

            if (result == true && isCorrectFileType) //User pressed OK and the extension is correct
            {
                loadSuccessful = true;
                projectFileName = openDialog.FileName;

                IFormatter format = new BinaryFormatter();
                Stream stream = new FileStream(projectFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                ProjectData loadedProject = (ProjectData)format.Deserialize(stream);
                savedPrizeLevels = loadedProject.savedPrizeLevels;
                savedGameSetup = loadedProject.savedGameSetup;
                savedDivisions = loadedProject.savedDivisions;
            }
            else if (result == true && !isCorrectFileType) //User pressed OK, but the extension is incorrect
            {
                System.Windows.MessageBox.Show("The file must be of type " + DEFAULT_EXT);
                loadSuccessful = this.OpenProject();
            }
            else if (result == false) //User pressed Cancel or closed the dialog box
            {
                loadSuccessful = false;
            }

            return loadSuccessful;
        }
    }
}
