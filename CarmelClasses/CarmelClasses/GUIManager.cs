using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace CarmelClasses
{
    public class GUIManager
    {
        #region Variables
        private Form1 form1;
        private SettingsManager settingsManager;
        private SQLiteManager sqliteManager;
        private UserManager userManager;
        private ConversionManager conversionManager;

        private int selectedTemplate;
        private string inputCSVFilePath;
        #endregion

        #region Constructor
        public GUIManager(Form1 form1, SettingsManager settingsManager, SQLiteManager sqliteManager, UserManager userManager, ConversionManager conversionManager)
        {
            this.form1 = form1;
            this.settingsManager = settingsManager;
            this.sqliteManager = sqliteManager;
            this.userManager = userManager;
            this.conversionManager = conversionManager;
        }
        #endregion
        
        #region Private Methods

        #region Helper Methods
        public string GetSelectedTemplateName()
        {
            return ((KeyValuePair<int, string>)form1.ComboBoxTemplates.SelectedItem).Value;
        }

        private void BindComboboxToDictionary(ComboBox comboBox, Dictionary<int, string> dictionary)
        {
            if (dictionary.Count > 0)
            {
                comboBox.DataSource = new BindingSource(dictionary, null);
                comboBox.DisplayMember = "Value";
                comboBox.ValueMember = "Key";
            }
        }

        private string GetSingleEditBoxPopupValue(string popupTitle, string labelText, string currentValue)
        {
            string newValue = "";

            SingleEditBoxPopup singleEditBoxPopup = new SingleEditBoxPopup();
            singleEditBoxPopup.Setup(popupTitle, labelText, currentValue);

            DialogResult dialogResult = singleEditBoxPopup.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                newValue = singleEditBoxPopup.EditedValue;
            }
            singleEditBoxPopup.Dispose();

            return newValue;
        }
        #endregion

        private void PopulateTemplateComboBox()
        {
            Dictionary<int, string> dictionary = sqliteManager.Template_List(true);

            BindComboboxToDictionary(form1.ComboBoxTemplates, dictionary);
        }

        private void PopulateScheduleComboBox()
        {
            Dictionary<int, string> dictionary = sqliteManager.Schedule_List(selectedTemplate, true);

            BindComboboxToDictionary(form1.ComboBoxSchedules, dictionary);
        }

        private void PopulateWingComboBox()
        {
            Dictionary<int, string> dictionary = sqliteManager.Wing_List();

            BindComboboxToDictionary(form1.ComboBoxWings, dictionary);            
        }
        #endregion

        #region Public Methods

        #region General Methods
        public void RefreshGUI()
        {
            PopulateTemplateComboBox();
            PopulateWingComboBox();
        }
        #endregion

        #region Event Methods
        public void TemplateComboBox_SelectionChanged()
        {
            selectedTemplate = ((KeyValuePair<int, string>)form1.ComboBoxTemplates.SelectedItem).Key;

            PopulateScheduleComboBox();
        }

        public void ConvertButton_Click()
        {
            if (selectedTemplate != -1 &&
                !string.IsNullOrEmpty(inputCSVFilePath) && 
                File.Exists(inputCSVFilePath))
            {
                conversionManager.ConvertCSVFile(
                    inputCSVFilePath,
                    string.Format("{0}//{1}{2}{3}", 
                        Path.GetDirectoryName(inputCSVFilePath), 
                        Path.GetFileNameWithoutExtension(inputCSVFilePath),
                        "_Converted",
                        Path.GetExtension(inputCSVFilePath)),
                    selectedTemplate);
            }
        }

        public void OpenCSVFileButton_Click()
        {
            DialogResult result = form1.OpenCSVFileDialog.ShowDialog(); 

            if (result == DialogResult.OK)
            {
                inputCSVFilePath = form1.OpenCSVFileDialog.FileName;               

                form1.TextBoxInputCSVFile.Text = inputCSVFilePath;
            }                
        }

        public void EditTemplateButton_Click()
        {
            if (selectedTemplate == -1) return;

            string currentTemplateName = GetSelectedTemplateName();

            string newTemplateName = GetSingleEditBoxPopupValue(
                "Template Aanpassen",
                "Template Naam:",
                currentTemplateName);

            if (!string.IsNullOrEmpty(newTemplateName) &&
                currentTemplateName != newTemplateName)
            {
                sqliteManager.Template_Update(selectedTemplate, newTemplateName);

                int currentIndex = form1.ComboBoxTemplates.SelectedIndex;
                RefreshGUI();
                form1.ComboBoxTemplates.SelectedIndex = currentIndex;
            }
        }

        public void DeleteTemplateButton_Click()
        {
            if (selectedTemplate == -1) return;

            DialogResult result = MessageBox.Show(
                string.Format("Weet u zeker dat u de template '{0}' wilt verwijderen?", GetSelectedTemplateName()),
                "Template Verwijderen", 
                MessageBoxButtons.OKCancel, 
                MessageBoxIcon.Warning);

            if (result == DialogResult.OK)
            {
                Dictionary<int, string> scheduleListDictionary = sqliteManager.Schedule_List(selectedTemplate);

                foreach (var kvp in scheduleListDictionary)
                {
                    int scheduleId = kvp.Key;

                    sqliteManager.ScheduleData_Delete(scheduleId);

                    List<int> wingList = sqliteManager.ScheduleWingData_List(scheduleId);

                    foreach (int wingId in wingList)
                    {
                        sqliteManager.ScheduleWingData_Delete(scheduleId, wingId);
                    }

                    sqliteManager.Schedule_Delete(scheduleId);
                }

                sqliteManager.Template_Delete(selectedTemplate);

                RefreshGUI();
            }
        }

        public void AddTemplateButton_Click()
        {
            string newTemplateName = GetSingleEditBoxPopupValue(
                "Template Toevoegen",
                "Template Naam:",
                "");

            if (!string.IsNullOrEmpty(newTemplateName) )
            {
                sqliteManager.Template_Insert(newTemplateName);        

                RefreshGUI();

                form1.ComboBoxTemplates.SelectedIndex = form1.ComboBoxTemplates.Items.Count - 1;
            }            
        }
        #endregion

        #endregion
    }
}
