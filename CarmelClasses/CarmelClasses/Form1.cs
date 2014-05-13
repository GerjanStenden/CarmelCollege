using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace CarmelClasses
{
    public partial class Form1 : Form
    {
        private SettingsManager settingsManager;
        private SQLiteManager sqliteManager;
        private UserManager userManager;
        private ConversionManager conversionManager;
        private GUIManager guiManager;

        #region Properties
        public ComboBox ComboBoxTemplates { get { return comboBox_Templates; } }
        public ComboBox ComboBoxSchedules { get { return comboBox_Schedules; } }
        public ComboBox ComboBoxWings { get { return comboBox_Wings; } }

        public TextBox TextBoxInputCSVFile { get { return textBox_InputFile; } }
        public OpenFileDialog OpenCSVFileDialog { get { return openFileDialog; } }

        public DataGridView ScheduleDataGrid { get{ return dataGridView; } }
        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            settingsManager = new SettingsManager();

            sqliteManager = new SQLiteManager(settingsManager.DatabasePath);

            userManager = new UserManager(settingsManager, sqliteManager);

            conversionManager = new ConversionManager(settingsManager, sqliteManager);

            guiManager = new GUIManager(this, settingsManager, sqliteManager, userManager, conversionManager);
            guiManager.RefreshGUI();
        }

        private void btn_Convert_Click(object sender, EventArgs e)
        {
            guiManager.ConvertButton_Click();
        }

        private void comboBox_Templates_SelectedIndexChanged(object sender, EventArgs e)
        {
            guiManager.TemplateComboBox_SelectionChanged();
        }

        private void textBox_InputFile_Click(object sender, EventArgs e)
        {
            guiManager.OpenCSVFileButton_Click();
        }

        private void btn_EditTemplate_Click(object sender, EventArgs e)
        {
            guiManager.EditTemplateButton_Click();
        }

        private void btn_DeleteTemplate_Click(object sender, EventArgs e)
        {
            guiManager.DeleteTemplateButton_Click();
        }

        private void btn_AddTemplate_Click(object sender, EventArgs e)
        {
            guiManager.AddTemplateButton_Click();
        }
    }
}
