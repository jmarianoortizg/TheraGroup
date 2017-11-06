using GrupoThera.Core.WorkingEnvironment;
using log4net;
using log4net.Appender;
using log4net.Config;
using System;
using System.IO;

namespace GrupoThera.Core.Logging
{
    public static class Logger
    {
        #region Fields

        private static readonly ILog FileLogger = LogManager.GetLogger(typeof(FileAppender));

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes static members of the <see cref="Logger"/> class.
        /// </summary>
        static Logger()
        {
            XmlConfigurator.Configure();
            var appenders = FileLogger.Logger.Repository.GetAppenders();
            if (appenders != null)
            {
                foreach (var appender in appenders)
                {
                    var fileAppender = appender as FileAppender;
                    if (fileAppender != null)
                    {
                        var fileName = Path.GetFileName(fileAppender.File);
                        if (string.IsNullOrEmpty(fileName))
                        {
                            fileName = string.Format("{0}.log", ApplicationInformation.Instance.ApplicationName);
                        }

                        // Overwrite file path and use the application log directory
                        fileAppender.File = Path.Combine(fileName);
                        fileAppender.ActivateOptions();
                    }
                }
            }
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Writes the exception and the given log message if the error log level is enabled.
        /// </summary>
        /// <param name="log">The message to be passed to the logger.</param>
        /// <param name="exception">The exception to log.</param>
        public static void WriteErrorLog(string log, Exception exception)
        {
            FileLogger.Error(log, exception);
        }

        /// <summary>
        /// Writes the message to logger if the given log level is enabled.
        /// The message is built from the format and args. The advantage of using
        /// this method is that strings are not being built if the given log level is not
        /// enabled.
        /// </summary>
        /// <param name="logLevel">The Log level for the message.</param>
        /// <param name="format">The format.</param>
        /// <param name="args">The args.</param>
        public static void WriteFormatLog(LogLevel logLevel, string format, params object[] args)
        {
            switch (logLevel)
            {
                case LogLevel.DEBUG:
                    if (!FileLogger.IsDebugEnabled)
                    {
                        break;
                    }
                    FileLogger.DebugFormat(format, args);
                    break;

                case LogLevel.ERROR:
                    if (!FileLogger.IsErrorEnabled)
                    {
                        break;
                    }
                    FileLogger.ErrorFormat(format, args);
                    break;

                case LogLevel.FATAL:
                    if (!FileLogger.IsFatalEnabled)
                    {
                        break;
                    }
                    FileLogger.FatalFormat(format, args);
                    break;

                case LogLevel.INFO:
                    if (!FileLogger.IsInfoEnabled)
                    {
                        break;
                    }
                    FileLogger.InfoFormat(format, args);
                    break;

                case LogLevel.WARN:
                    if (!FileLogger.IsWarnEnabled)
                    {
                        break;
                    }
                    FileLogger.WarnFormat(format, args);
                    break;
            }
        }

        /// <summary>
        /// Writes the log string to logger if the given log level is enabled.
        /// </summary>
        /// <param name="logLevel">The Log level for the message.</param>
        /// <param name="log">The message to be passed to the logger.</param>
        public static void WriteLog(LogLevel logLevel, string log)
        {
            switch (logLevel)
            {
                case LogLevel.DEBUG:
                    if (!FileLogger.IsDebugEnabled)
                    {
                        break;
                    }
                    FileLogger.Debug(log);
                    break;

                case LogLevel.ERROR:
                    if (!FileLogger.IsErrorEnabled)
                    {
                        break;
                    }
                    FileLogger.Error(log);
                    break;

                case LogLevel.FATAL:
                    if (!FileLogger.IsFatalEnabled)
                    {
                        break;
                    }
                    FileLogger.Fatal(log);
                    break;

                case LogLevel.INFO:
                    if (!FileLogger.IsInfoEnabled)
                    {
                        break;
                    }
                    FileLogger.Info(log);
                    break;

                case LogLevel.WARN:
                    if (!FileLogger.IsWarnEnabled)
                    {
                        break;
                    }
                    FileLogger.Warn(log);
                    break;
            }
        }

        #endregion Methods
    }
}
