using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Reflection;

namespace CarmelClasses
{
    public class SettingsManager
    {
        #region Imported INI File Methods
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        #endregion

        #region Variables
        private const string SettingsFileName = @"\settings.ini";
        private string settingsFilePath;

        // Sections and Keys
        private const string DatabaseSection = "Database";
        private const string DatabasePathKey = "Path";

        private const string CSVFileSection = "CSVFile";
        private const string CSVFileDelimiterKey = "Delimiter";
        private const string CSVFileHasHeaderKey = "HasHeader";

        private const string AdministratorSection = "Administrator";
        private const string AdministratorUsernameKey = "Username";

        #endregion

        #region Properties
        public string DatabasePath { get; private set; }
        public string AdministratorUsername { get; private set; }
        public string CSVFileDelimiter { get; private set; }
        public bool CSVFileHasHeader { get; private set; }
        #endregion

        #region Constructor
        public SettingsManager()
        {
            settingsFilePath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + SettingsFileName;

            CreateIni();

            LoadIniValues();
        }
        #endregion

        #region Private Methods

        #region Write/Read Ini Values
        private void WriteIniValue(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, settingsFilePath);
        }

        private string ReadIniValue(string section, string key)
        {
            StringBuilder stringBuilder = new StringBuilder(255);

            int i = GetPrivateProfileString(section, key, "", stringBuilder, 255, settingsFilePath);

            return stringBuilder.ToString();
        }
        #endregion

        #region Load Ini Values
        private void LoadIniValues()
        {
            DatabasePath = ReadIniValue(DatabaseSection, DatabasePathKey);
            AdministratorUsername = ReadIniValue(AdministratorSection, AdministratorUsernameKey);
            CSVFileDelimiter = ReadIniValue(CSVFileSection, CSVFileDelimiterKey);
            CSVFileHasHeader = ReadIniValue(CSVFileSection, CSVFileHasHeaderKey).ToLower() == "true";
        }
        #endregion

        #endregion

        #region Public Methods

        #endregion

        #region Temp Methods
        public void CreateIni()
        {
            if (!File.Exists(settingsFilePath))
            {
                File.Create(settingsFilePath).Dispose();

                WriteIniValue(DatabaseSection, DatabasePathKey, "");
                WriteIniValue(AdministratorSection, AdministratorUsernameKey, "");
                WriteIniValue(CSVFileSection, CSVFileDelimiterKey, ",");
                WriteIniValue(CSVFileSection, CSVFileHasHeaderKey, "false");
            }
        }
        #endregion
    }
}
