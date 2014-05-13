using System.DirectoryServices.AccountManagement;

namespace CarmelClasses
{ 
    public class UserManager
    {
        #region Variables
        private SettingsManager settingsManager;
        private SQLiteManager sqliteManager;

        private string activeDirectoryUserName;
        #endregion

        #region Properties
        public string ActiveDirectoryUserName { get { return activeDirectoryUserName; } }
        #endregion

        #region Constructor
        public UserManager(SettingsManager settingsManager, SQLiteManager sqliteManager)
        {
            this.settingsManager = settingsManager;
            this.sqliteManager = sqliteManager;

            GetActiveDirectoryUserName();
        }
        #endregion

        #region Private Methods
        private void GetActiveDirectoryUserName()
        {
            // Figure out username case type (lower/upper case etc)
            // TODO: Add exception cases when AD is unavailable

            // From: http://stackoverflow.com/questions/10877614/getting-current-login-from-active-directory-using-c-sharp-code
            /*
            // Set up domain context
            PrincipalContext ctx = new PrincipalContext(ContextType.Domain);
            // Find current user
            UserPrincipal user = UserPrincipal.Current;

            if (user != null)
            {
                activeDirectoryUserName = user.SamAccountName;
            }   
             */
            activeDirectoryUserName = "";
        }
        #endregion

        #region Public Methods
        public bool GetIsAdministrator()
        {
            return settingsManager.AdministratorUsername == activeDirectoryUserName;
        }

        public bool GetIsUser()
        {
            return GetIsAdministrator() || sqliteManager.User_Exists(activeDirectoryUserName);
        }

        public void AddUser(string userName)
        {
            // TODO: Sanitize username

            if (!sqliteManager.User_Exists(userName))
            {
                sqliteManager.User_Insert(userName);
            }
        }

        public void EditUser(int userId, string newUserName)
        {
            // TODO: Sanitize username

            sqliteManager.User_Update(userId, newUserName);
        }

        public void DeleteUser(int userId)
        {
            sqliteManager.User_Delete(userId);
        }
        #endregion
    }
}
