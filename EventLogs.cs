using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TimesheetAPI
{
    public class EventLogs
    {
        public static void LogEvent(string logevent)
        {
            string message = DateTime.Now.ToString() + " \t  " + logevent;
            // string path = Application.StartupPath+ @"\EventLog\ErrorLog.txt";
            string date = DateTime.Now.ToString("ddMMMyyyy");
            System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"DebugLog\");
            string path = AppDomain.CurrentDomain.BaseDirectory + @"DebugLog\" + date + ".txt";

            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(message);
                writer.Close();
            }
        }
    }
}
