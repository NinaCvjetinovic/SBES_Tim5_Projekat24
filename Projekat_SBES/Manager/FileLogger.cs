using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Manager
{
    public class FileLogger
    {
        private readonly string _textFilePath;
        private readonly string _xmlFilePath;
        public FileLogger(string textFilePath, string xmlFilePath)
        {
            _textFilePath = textFilePath;
            _xmlFilePath = xmlFilePath;
        }
        public void LogToTextFile(LogEntry logEntry) //logovanje u tekstualnu datoteku
        {

            using (StreamWriter writer = new StreamWriter(_textFilePath, true))
            {
                writer.WriteLine($"{logEntry.Timestamp},{logEntry.Username},{logEntry.Action},{logEntry.Result}");
            }
        }
        public void LogToXmlFile(LogEntry logEntry) //logovanje u xml datoteku
        {
            XElement logElement = new XElement("logEntry", new XAttribute("timestamp", logEntry.Timestamp),
                                                           new XAttribute("username", logEntry.Username),
                                                           new XAttribute("action", logEntry.Action),
                                                           new XAttribute("result", logEntry.Result)
                                                );
            XDocument doc = XDocument.Load(_xmlFilePath);
            doc.Root.Add(logElement);
            doc.Save(_xmlFilePath);
        }
    }
}