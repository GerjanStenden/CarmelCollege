using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace CarmelClasses
{
    public class ConversionManager
    {
        #region Variables
        private SettingsManager settingsManager;
        private SQLiteManager sqliteManager;

        // ScheduleId
        private List<int> templateScheduleList;
        // WingId, WingName
        private Dictionary<int, string> wingDictionary;
        // WingName, Scheduled
        private Dictionary<string, int> wingScheduleDictionary;
        // ScheduleId, <Data>
        private Dictionary<int, DataTable> scheduleDataDictionary;

        #endregion

        #region Constructor
        public ConversionManager(SettingsManager settingsManager, SQLiteManager sqliteManager)
        {
            this.settingsManager = settingsManager;
            this.sqliteManager = sqliteManager;
        }
        #endregion

        #region Private Methods

        #region Conversion Rule Data Methods
        private void ClearConversionRules()
        {
            templateScheduleList = new List<int>();
            wingDictionary = new Dictionary<int, string>();
            wingScheduleDictionary = new Dictionary<string, int>();
            scheduleDataDictionary = new Dictionary<int, DataTable>();
        }

        private void LoadConversionRules(int templateId)
        {
            ClearConversionRules();
            
            LoadTemplateScheduleList(templateId);

            LoadWingDictionary();

            LoadWingScheduleDictionary();

            LoadScheduleData();
        }

        private void LoadTemplateScheduleList(int templateId)
        {// Fill a list with all the ScheduleIds contained in TemplateId            
            Dictionary<int, string> scheduleList = sqliteManager.Schedule_List(templateId);

            foreach (var kvp in scheduleList)
            {
                templateScheduleList.Add(kvp.Key);
            }
        }

        private void LoadWingDictionary()
        {// Fill a dictionary with all WingIds and WingNames
            wingDictionary = sqliteManager.Wing_List();    
        }

        private void LoadWingScheduleDictionary()
        {// Link WingNames to ScheduleIds
            foreach (int scheduleId in templateScheduleList)
            {
                List<int> wingIdList = sqliteManager.ScheduleWingData_List(scheduleId);

                foreach (int wingId in wingIdList)
                {
                    string wingName = wingDictionary[wingId];

                    wingScheduleDictionary.Add(wingName, scheduleId);
                }
            }
        }

        private void LoadScheduleData()
        {// Fill a dictionary with ScheduleId as key and ScheduleData as value            
            foreach (int scheduleId in templateScheduleList)
            {
                DataTable scheduleDataTable = sqliteManager.ScheduleData_GetData(scheduleId);

                scheduleDataDictionary.Add(scheduleId, scheduleDataTable);
            }
        }
        #endregion

        #region Conversion Helper Methods
        private int GetScheduleIdFromWingName(string wingName)
        {
            int scheduleId;

            if (!wingScheduleDictionary.TryGetValue(wingName, out scheduleId))
            {
                scheduleId = -1;
            }

            return scheduleId;
        }

        private string GetAdjustedTime(int scheduleId, DayOfWeek day, int classHour)
        {
            string adjustedTime = "";
            DataTable scheduleData;

            if (scheduleDataDictionary.TryGetValue(scheduleId, out scheduleData))
            {
                DataRow[] result = scheduleData.Select(
                    string.Format(
                    "ScheduleId = '{0}' AND Day = '{1}' AND ClassHour = '{2}'",
                    scheduleId,
                    ((int)day).ToString(),
                    classHour));

                // /Should/ only have one row as result
                adjustedTime = result[0]["StartingHour"].ToString() + result[0]["StartingMinutes"].ToString();
            }

            return adjustedTime;
        }
        #endregion

        #endregion

        #region Public Methods
        public void ConvertCSVFile(string input, string output, int templateId)
        {
            int lineNumber = 0;
            Regex splitRegex = new Regex(string.Format(@"({0})(?=(?:[^""]|""[^""]*"")*$)", settingsManager.CSVFileDelimiter));
            
            /*  Pseudocode
             *  <LoadConversionRules>
             *  
             *  <Read Line>
             *  <Get WingName>
             *  <Get ScheduleId From WingName>
             *  <Get ScheduleData From ScheduleId>
             *  <Get Day>
             *  <Get ClassHour>
             *  
             *  <Get Time from ScheduleData using Day/ClassHour>
             *  
             *  <Update Line>
             *  <Write Line>
            */

            LoadConversionRules(templateId);

            using (StreamWriter streamWriter = new StreamWriter(output))
            {
                using(StreamReader streamReader = new StreamReader(input))
                {
                    string currentLine = null;
                    string[] headers = null;
                    

                    if (settingsManager.CSVFileHasHeader)
                    {
                        currentLine = streamReader.ReadLine();
                        lineNumber++;

                        if (string.IsNullOrEmpty(currentLine)) return;

                        headers = splitRegex.Split(currentLine).Where(s => s != settingsManager.CSVFileDelimiter).ToArray();

                        streamWriter.WriteLine(currentLine);
                    }

                    while ((currentLine = streamReader.ReadLine()) != null)
                    {
                        lineNumber++;

                        string[] columns = splitRegex.Split(currentLine).Where(s => s != settingsManager.CSVFileDelimiter).ToArray();

                        // SanityCheck: Make sure we always have the same amount of columns
                        if (headers == null)
                        {
                            headers = new string[columns.Length];
                        }

                        if (columns.Length != headers.Length)
                        {
                            // TODO: Handle this better
                            throw new InvalidOperationException(
                                string.Format("Line {0} is missing one or more columns.", lineNumber));
                        }
                        ///////////////////////////////////////////////////////////////////

                        /* Column Keys
                         * 0 -> ???
                         * 1 -> Year
                         * 2 -> Month
                         * 3 -> Day
                         * 4 -> ClassHour
                         * 5 -> ???
                         * 6 -> ???
                         * 7 -> WingName + Room
                         * 8 -> ??? (Empty)
                         * 9 -> ??? (Empty)
                         * 10 -> ???
                         * 11 -> Time (hhmm)
                         * 12 -> Duration
                         */

                        string wingName = Utility.GetLettersOnly(columns[7]);

                        int scheduleId = GetScheduleIdFromWingName(wingName);

                        if (scheduleId != -1)
                        {
                            DayOfWeek day = new DateTime(int.Parse(columns[1]), int.Parse(columns[2]), int.Parse(columns[3])).DayOfWeek;

                            int classHour = int.Parse(columns[4]);

                            string adjustedTime = GetAdjustedTime(scheduleId, day, classHour);

                            columns[11] = string.IsNullOrEmpty(adjustedTime) ? columns[11] : adjustedTime;
                        }

                        streamWriter.WriteLine(string.Join(settingsManager.CSVFileDelimiter, columns));
                    }
                }
            }
        }
        #endregion
    }
}
