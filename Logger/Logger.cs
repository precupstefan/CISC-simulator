using System;
using System.Windows.Controls;

namespace Logger
{
    public class Logger
    {
        
        public static Logger Instance { get; }=new Logger();
        
        private TextBlock Console;

        public void SetConsole(TextBlock console)
        {
            Console = console;
        }
        
        public void Log(string message)
        {
            var time = DateTime.Now.ToLongTimeString();
            Console.Inlines.Add(time + " : " + message + Environment.NewLine);
        }
    }
}