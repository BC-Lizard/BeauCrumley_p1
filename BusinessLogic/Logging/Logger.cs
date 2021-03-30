using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Logging
{
    public class ConsoleLogger : IProcessLogger
    {
        public void Log(string message, bool logMessage)
        {
            if (logMessage)
            {
                Console.WriteLine($"LOG: {message} was created.");
            }
        }
    }

    public class FileLogger : IProcessLogger
    {
        public void Log(string message, bool logMessage)
        {
            if (logMessage)
            {
                //code for writing log to file
            }
        }
    }
}
