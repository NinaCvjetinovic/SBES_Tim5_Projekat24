using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    public class LogEntry
    {
        public LogEntry(DateTime timestamp, string username, string action, string result)
        {
            Timestamp = timestamp;
            Username = username;
            Action = action;
            Result = result;
        }

        public LogEntry()
        {

        }

        public DateTime Timestamp { get; set; }
        public string Username { get; set; }
        public string Action { get; set; }
        public string Result { get; set; }

        
    }
}
