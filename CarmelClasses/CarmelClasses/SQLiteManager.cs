using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace CarmelClasses
{
    public class SQLiteManager
    {
        #region Variables
        private const string databaseName = "rctdata.s3db";

        private string databasePath;
        private string dataSource;

        private SQLiteConnection sqliteConnection;
        #endregion

        #region Constructor
        public SQLiteManager(string databasePath)
        {
            this.databasePath = CreateDatabasePath(databasePath);

            dataSource = string.Format("Data Source={0}; FailIfMissing=True", this.databasePath);

            CheckDatabaseExists();
        }
        #endregion

        #region Private Utility Methods
        private string CreateDatabasePath(string databasePath)
        {
            return string.IsNullOrEmpty(databasePath) ? databaseName : string.Format("{0}\\{1}", databasePath, databaseName);    
        }
        #endregion

        #region Private Database Methods
        private void CheckDatabaseExists()
        {
            try
            {
                OpenDatabase();
            }
            catch (SQLiteException)
            {
                CreateNewDatabase();

                #region Create Tables
                string SQL_CreateTable_User =
                    @"CREATE TABLE User 
                    (
                        UserId INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        Username VARCHAR(45) NOT NULL UNIQUE
                    );";
                ExecuteNonQuery(SQL_CreateTable_User);

                string SQL_CreateTable_Template =
                    @"CREATE TABLE Template 
                    (
                        TemplateId INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        TemplateName VARCHAR(45) NOT NULL
                    );";
                ExecuteNonQuery(SQL_CreateTable_Template);

                // Test Data
                Template_Insert("50 Minuten Rooster");
                ////////////

                string SQL_CreateTable_Wing =
                    @"CREATE TABLE Wing 
                    (
                        WingId INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        WingName VARCHAR(5) NOT NULL
                    );";
                ExecuteNonQuery(SQL_CreateTable_Wing);

                // Test Data
                Wing_Insert("B");
                Wing_Insert("S");
                Wing_Insert("BS");
                ////////////

                string SQL_CreateTable_Schedule =
                    @"CREATE TABLE Schedule 
                    (
                        ScheduleId INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        TemplateId INTEGER NOT NULL,
                        ScheduleName VARCHAR(45) NOT NULL
                    );";
                ExecuteNonQuery(SQL_CreateTable_Schedule);

                // Test Data
                Schedule_Insert(1, "Rooster Vleugel: B, BS");
                Schedule_Insert(1, "Rooster Vleugel: S");
                ////////////

                string SQL_CreateTable_ScheduleData =
                    @"CREATE TABLE ScheduleData
                    (
                        ScheduleId INTEGER NOT NULL,
                        Day INTEGER NOT NULL,
                        ClassHour INTEGER NOT NULL,
                        StartingHour INTEGER NOT NULL,
                        StartingMinutes INTEGER NOT NULL,
                        Duration INTEGER NOT NULL,
                        PRIMARY KEY(ScheduleId, Day, ClassHour)
                    );";
                ExecuteNonQuery(SQL_CreateTable_ScheduleData);

                // Test Data
                ScheduleData_Insert(new ScheduleData()
                {
                    ScheduleId = 1,
                    Day = System.DayOfWeek.Friday,
                    ClassHour = 7,
                    StartingHour = 8,
                    StartingMinutes = 30,
                    Duration = 50

                });

                ScheduleData_Insert(new ScheduleData()
                {
                    ScheduleId = 2,
                    Day = System.DayOfWeek.Friday,
                    ClassHour = 3,
                    StartingHour = 16,
                    StartingMinutes = 30,
                    Duration = 50

                });
                ////////////

                string SQL_CreateTable_ScheduleWingData =
                    @"CREATE TABLE ScheduleWingData
                    (
                        ScheduleId INTEGER NOT NULL,
                        WingId INTEGER NOT NULL,
                        PRIMARY KEY(ScheduleId, WingId)
                    );";
                ExecuteNonQuery(SQL_CreateTable_ScheduleWingData);

                // Test Data
                ScheduleWingData_Insert(1, 1);
                ScheduleWingData_Insert(1, 3);
                ScheduleWingData_Insert(2, 2);
                ////////////
                #endregion
            }
            finally
            {
                CloseDatabase();
            }
        }

        private void OpenDatabase()
        {
            sqliteConnection = new SQLiteConnection(dataSource);

            sqliteConnection.Open();
        }

        private void CloseDatabase()
        {
            sqliteConnection.Close();
        }

        private void CreateNewDatabase()
        {
            SQLiteConnection.CreateFile(databasePath);
        }
        #endregion

        #region Private Query Methods
        private int ExecuteNonQuery(string sql)
        {
            OpenDatabase();

            SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection);
            sqliteCommand.CommandText = sql;

            int rowsUpdated = sqliteCommand.ExecuteNonQuery();

            CloseDatabase();

            return rowsUpdated;
        }

        private DataTable GetDataTable(string sql)
        {
            DataTable dataTable = new DataTable();

            OpenDatabase();

            SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection);
            sqliteCommand.CommandText = sql;

            SQLiteDataReader sqliteDataReader = sqliteCommand.ExecuteReader();
            dataTable.Load(sqliteDataReader);

            sqliteDataReader.Close();

            CloseDatabase();

            return dataTable;
        }

        public string ExecuteScalar(string sql)
        {
            OpenDatabase();

            SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection);
            sqliteCommand.CommandText = sql;

            object returnValue = sqliteCommand.ExecuteScalar();

            CloseDatabase();

            if (returnValue != null)
            {
                return returnValue.ToString();
            }
            return "";
        }

        private void Insert(string tableName, Dictionary<string, string> data)
        {
            string columns = "";
            string values = "";

            foreach (var kvp in data)
            {
                columns += string.Format(" {0},", kvp.Key);
                values += string.Format(" '{0}',", kvp.Value);
            }

            columns = columns.Substring(0, columns.Length - 1);
            values = values.Substring(0, values.Length - 1);
            
            string SQL_Insert = string.Format(
                "INSERT INTO {0} ({1}) values({2});",
                tableName,
                columns,
                values);

            ExecuteNonQuery(SQL_Insert);
        }

        private void Update(string table, Dictionary<string, string> data, string where)
        {
            string values = "";

            if (data.Count > 0)
            {
                values = data.Aggregate(values, (current, kvp) =>  current + string.Format(" {0} = '{1}',", kvp.Key, kvp.Value));
                values = values.Substring(0, values.Length - 1);
            }
            
            string SQL_Update = string.Format(
                "UPDATE {0} SET {1} WHERE {2};",
                table,
                values,
                where);

            ExecuteNonQuery(SQL_Update);               
        }

        private void Delete(string tableName, string where)
        {
            string SQL_Delete = string.Format(
                "DELETE FROM {0} WHERE {1};",
                tableName,
                where);

            ExecuteNonQuery(SQL_Delete);             
        }
        #endregion

        #region Public Query Methods

        #region User
        public Dictionary<int, string> User_List()
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>();

            string SQL_Select = string.Format(
                "SELECT UserId, Username FROM User;");

            DataTable dataTable = GetDataTable(SQL_Select);

            foreach (DataRow dataRow in dataTable.Rows)
            {
                dictionary.Add(
                    int.Parse(dataRow["UserId"].ToString()),
                    dataRow["Username"].ToString());
            }

            return dictionary;
        }

        public bool User_Exists(string username)
        {
            string SQL_Select_User = string.Format(
                "SELECT UserId FROM User WHERE Username = '{0}';",
                username);

            return ExecuteScalar(SQL_Select_User) == "" ? false : true; 
        }

        public void User_Insert(string username)
        {
            Insert(
                "User", 
                new Dictionary<string, string>(){{"Username", username}});
        }

        public void User_Update(int userId, string newUsername)
        {
            Update(
                "User", 
                new Dictionary<string, string>()
                {
                    {"Username", newUsername}
                }, 
                string.Format("UserId = '{0}'", userId));
        }

        public void User_Delete(int userId)
        {
            Delete(
                "User", 
                string.Format("UserId = '{0}'", userId));
        }
        #endregion

        #region Template
        public Dictionary<int, string> Template_List(bool addEmptyRow = false)
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>();

            if (addEmptyRow)
            {
                dictionary.Add(-1, "--- Selecteer een Template ---");
            }

            string SQL_Select = string.Format(
                "SELECT TemplateId, TemplateName FROM Template;");

            DataTable dataTable = GetDataTable(SQL_Select);

            foreach (DataRow dataRow in dataTable.Rows)
            {
                dictionary.Add(
                    int.Parse(dataRow["TemplateId"].ToString()),
                    dataRow["TemplateName"].ToString());
            }

            return dictionary;
        }

        public void Template_Insert(string templateName)
        {
            Insert(
                "Template", 
                new Dictionary<string, string>()
                {
                    {"TemplateName", templateName}
                });
        }

        public void Template_Update(int templateId, string newTemplateName)
        {
            Update(
                "Template",
                new Dictionary<string, string>()
                {
                    {"TemplateName", newTemplateName}
                }, 
                string.Format("TemplateId = '{0}'", templateId));
        }

        public void Template_Delete(int templateId)
        {
            Delete(
                "Template", 
                string.Format("TemplateId = '{0}'", templateId));
        }
        #endregion

        #region Wing
        public Dictionary<int, string> Wing_List()
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>();

            string SQL_Select = string.Format(
                "SELECT WingId, WingName FROM Wing;");

            DataTable dataTable = GetDataTable(SQL_Select);

            foreach (DataRow dataRow in dataTable.Rows)
            {
                dictionary.Add(
                    int.Parse(dataRow["WingId"].ToString()),
                    dataRow["WingName"].ToString());
            }

            return dictionary;
        }

        public void Wing_Insert(string wingName)
        {
            Insert(
                "Wing", 
                new Dictionary<string, string>()
                {
                    {"WingName", wingName}
                });
        }

        public void Wing_Update(int wingId, string newWingName)
        {
            Update(
                "Wing",
                new Dictionary<string, string>()
                {
                    {"WingName", newWingName}
                },
                string.Format("WingId = '{0}'", wingId));
        }

        public void Wing_Delete(int wingId)
        {
            Delete(
                "Wing", 
                string.Format("WingId = '{0}'", wingId));
        }
        #endregion

        #region Schedule
        public Dictionary<int, string> Schedule_List(int templateId, bool addEmptyRow = false)
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>();

            if (addEmptyRow)
            {
                dictionary.Add(-1, "--- Selecteer een Rooster ---");
            }

            string SQL_Select = string.Format(
                "SELECT ScheduleId, ScheduleName FROM Schedule WHERE TemplateId = '{0}';", templateId);

            DataTable dataTable = GetDataTable(SQL_Select);

            foreach (DataRow dataRow in dataTable.Rows)
            {
                dictionary.Add(
                    int.Parse(dataRow["ScheduleId"].ToString()),
                    dataRow["ScheduleName"].ToString());
            }

            return dictionary;
        }

        public void Schedule_Insert(int templateId, string scheduleName)
        {
            Insert(
                "Schedule",
                new Dictionary<string, string>()
                {
                    {"TemplateId", templateId.ToString()}, 
                    {"ScheduleName", scheduleName}
                });
        }

        public void Schedule_Update(int scheduleId, string newScheduleName)
        {
            Update(
                "Schedule",
                new Dictionary<string, string>()
                {
                    {"ScheduleName", newScheduleName}
                },
                string.Format("ScheduleId = '{0}'", scheduleId));
        }

        public void Schedule_Delete(int scheduleId)
        {
            Delete(
                "Schedule", 
                string.Format("ScheduleId = '{0}'", scheduleId));
        }
        #endregion

        #region ScheduleData
        public DataTable ScheduleData_GetData(int scheduleId)
        {
            string SQL_Select = string.Format(
                "SELECT ScheduleId, Day, ClassHour, StartingHour, StartingMinutes, Duration FROM ScheduleData WHERE ScheduleId = '{0}';", 
                scheduleId);

            DataTable dataTable = GetDataTable(SQL_Select);

            return dataTable;
        }

        public DataTable ScheduleData_GetDataGridViewData(int scheduleId, int day)
        {
            string SQL_Select = string.Format(
                "SELECT ClassHour, StartingHour, StartingMinutes, Duration FROM ScheduleData WHERE ScheduleId = '{0}' AND Day = '{1}';;",
                scheduleId,
                day);

            DataTable dataTable = GetDataTable(SQL_Select);

            return dataTable;            
        }

        public void ScheduleData_Insert(ScheduleData scheduleData)
        {
            Insert(
                "ScheduleData",
                new Dictionary<string, string>()
                {
                    {"ScheduleId", scheduleData.ScheduleId.ToString()},
                    {"Day", ((int)scheduleData.Day).ToString()},
                    {"ClassHour", scheduleData.ClassHour.ToString()},
                    {"StartingHour", scheduleData.StartingHour.ToString()},
                    {"StartingMinutes", scheduleData.StartingMinutes.ToString()},
                    {"Duration", scheduleData.Duration.ToString()}
                });
        }

        public void ScheduleData_Insert(List<ScheduleData> scheduleDataList)
        {
            foreach (ScheduleData scheduleData in scheduleDataList)
            {
                Insert(
                    "ScheduleData",
                    new Dictionary<string, string>()
                    {
                        {"ScheduleId", scheduleData.ScheduleId.ToString()},
                        {"Day", ((int)scheduleData.Day).ToString()},
                        {"ClassHour", scheduleData.ClassHour.ToString()},
                        {"StartingHour", scheduleData.StartingHour.ToString()},
                        {"StartingMinutes", scheduleData.StartingMinutes.ToString()},
                        {"Duration", scheduleData.Duration.ToString()}
                    });
            }
        }

        public void ScheduleData_Delete(int scheduleId)
        {
            Delete(
                "ScheduleData",
                string.Format("ScheduleId = '{0}'", scheduleId));
        }
        
        #endregion

        #region ScheduleWingData
        public List<int> ScheduleWingData_List(int scheduleId)
        {
            List<int> list = new List<int>();

            string SQL_Select = string.Format(
                "SELECT WingId FROM ScheduleWingData WHERE ScheduleId = '{0}';", scheduleId);

            DataTable dataTable = GetDataTable(SQL_Select);

            foreach (DataRow dataRow in dataTable.Rows)
            {
                list.Add(int.Parse(dataRow["WingId"].ToString()));
            }

            return list;
        }

        public void ScheduleWingData_Insert(int scheduleId, int wingId)
        {
            Insert(
                "ScheduleWingData",
                new Dictionary<string, string>()
                {
                    {"ScheduleId", scheduleId.ToString()},
                    {"WingId", wingId.ToString()}
                });
        }

        public void ScheduleWingData_Delete(int scheduleId, int wingId)
        {
            Delete(
                "ScheduleWingData",
                string.Format("ScheduleId = '{0}' AND WingId = '{1}'", scheduleId, wingId));
        }

        #endregion

        #endregion
    }
}
