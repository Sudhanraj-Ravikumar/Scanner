using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ScannerDisplay
{
    public enum Category
    {
        Undef = 0,
        Info = 1, 
        Error = 2,
        Warning = 3
    }

    public class Logger
    {
        private static object Locker = new object();

        private StreamWriter LogWriter
        { get; set; }

        private string LogDirectory
        { get; set; }

        private DateTime Ndt
        { get; set; }

        private string Name
        { get; set; }

        public Logger(string name)
        {
            Name = name;

            string sLogDir = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);

            LogDirectory = sLogDir?.Trim();

            if (LogDirectory?[LogDirectory.Length - 1] != '\\')
            {
                LogDirectory += '\\';
            }

            LogDirectory = LogDirectory + "Log\\";

            if (!Directory.Exists(LogDirectory))
            {
                Directory.CreateDirectory(LogDirectory);
            }

            LogWriter = CreateFileStreamWriter();

            if (EventLog.Exists(LogDirectory))
            {
                EventLog.CreateEventSource(LogDirectory, null);
            }
        }

        private StreamWriter CreateFileStreamWriter()
        {
            string sExe = ConfigurationManager.AppSettings["AppName"];
            StreamWriter sw = File.AppendText(LogDirectory + Name + "_" + DateTime.Now.ToString("yyyyMMdd") + ".log");
            return sw;
        }

        public void Log(Category eCat, string sMessage)
        {
            try
            {
                lock (Locker)
                {
                    DateTime dt = DateTime.Now;

                    if (Math.Abs(dt.DayOfYear - Ndt.DayOfYear) > 0)
                    {
                        if (LogWriter != null)
                        {
                            LogWriter.Close();
                            LogWriter.Dispose();
                        }

                        LogWriter = CreateFileStreamWriter();
                    }

                    if (LogWriter != null)
                    {
                        Ndt = dt;

                        string s = dt.ToString("yyyy-MM-dd HH:mm:ss:fff") + " " + eCat + ": > " + sMessage;

                        LogWriter.BaseStream.Seek(0, SeekOrigin.End);
                        LogWriter.WriteLine(s);
                        LogWriter.Flush();
                    }
                }
            }
            catch (Exception) { }
        }

        public void Close()
        {
            if (LogWriter != null)
            {
                LogWriter.Close();
                LogWriter.Dispose();
                LogWriter = null;
            }
        }
    }
}
