using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoThera.Core.WorkingEnvironment
{
    public class PathConstants
    {
        #region Fields

        private const string ClientConfigDirectory = "config";
        private const string ClientLogsDirectory = "logs";
        private const string ClientTempDirectory = "temp";
        private const string DefaultLogFilename = "application.log";
        private const string DefaultUserSettingsFilename = "UserSettings.config";

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes static members of the <see cref="PathConstants"/> class.
        /// </summary>
        static PathConstants()
        {
            var projectDirectoryName = GetLocalApplicationDirectoryName();
            var applicationDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            UserLocalAppDirectoryPath = Path.Combine(applicationDataFolder, projectDirectoryName);
            UserLogsDirectoryPath = Path.Combine(UserLocalAppDirectoryPath, ClientLogsDirectory);
            UserConfigDirectoryPath = Path.Combine(UserLocalAppDirectoryPath, ClientConfigDirectory);
            UserTempDirectoryPath = Path.Combine(UserLocalAppDirectoryPath, ClientTempDirectory);
            EnsureDirectoriesExist();
            DefaultLogFileName = Path.Combine(UserLogsDirectoryPath, DefaultLogFilename);
            DefaultUserSettingsFileName = Path.Combine(UserConfigDirectoryPath, DefaultUserSettingsFilename);
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the default name of the log file.
        /// </summary>
        /// <value>
        /// The default name of the log file.
        /// </value>
        public static string DefaultLogFileName
        {
            get; private set;
        }

        /// <summary>
        /// Gets the default name of the user settings file.
        /// </summary>
        /// <value>
        /// The default name of the user settings file.
        /// </value>
        public static string DefaultUserSettingsFileName
        {
            get; private set;
        }

        /// <summary>
        /// Gets the user config directory path. This is the root directory for configuration data.
        /// </summary>
        public static string UserConfigDirectoryPath
        {
            get; private set;
        }

        /// <summary>
        /// Gets the user local directory path. This is the root directory of the user
        /// local app data.
        /// </summary>
        public static string UserLocalAppDirectoryPath
        {
            get; private set;
        }

        /// <summary>
        /// Gets the user logs directory path.
        /// </summary>
        public static string UserLogsDirectoryPath
        {
            get; private set;
        }

        /// <summary>
        /// Gets the user temp storage directory path.This is the root directory for temporary saved database files.
        /// </summary>
        public static string UserTempDirectoryPath
        {
            get; set;
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Gets the name of the local application directory.
        /// </summary>
        /// <returns>The name of the local application directory</returns>
        public static string GetLocalApplicationDirectoryName()
        {
            var applicationName = ApplicationInformation.Instance.ApplicationName;
            if (string.IsNullOrEmpty(applicationName) || applicationName == "PROD")
            {
                return ApplicationInformation.Instance.ApplicationName;
            }
            return string.Format("{0}.{1}", ApplicationInformation.Instance.ApplicationName, ApplicationInformation.Instance.ApplicationEnvironment);
        }

        /// <summary>
        /// Creates the directory.
        /// </summary>
        /// <param name="directoryPath">The directory path.</param>
        private static void CreateDirectory(string directoryPath)
        {
            try
            {
                Directory.CreateDirectory(directoryPath);
            }
            catch (Exception e)
            {
                Logging.Logger.WriteErrorLog(string.Format("Can not create directory {0}", directoryPath), e);
            }
        }

        /// <summary>
        /// Ensures the directories exist.
        /// </summary>
        private static void EnsureDirectoriesExist()
        {
            CreateDirectory(UserLocalAppDirectoryPath);
            CreateDirectory(UserConfigDirectoryPath);
            CreateDirectory(UserLogsDirectoryPath);
            CreateDirectory(UserTempDirectoryPath);
        }

        #endregion Methods
    }
}
