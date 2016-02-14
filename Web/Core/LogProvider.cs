using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Web.Core
{
    public class LogProvider
    {
        public static string LogFile { private set; get; }

        public LogProvider(string logFile)
        {
            LogFile = Path.Combine(System.Web.HttpContext.Current.Request.PhysicalApplicationPath, logFile);
            FileStream fs = new FileStream(LogFile, FileMode.Append);
            StreamWriterWithTimestamp sw = new StreamWriterWithTimestamp(fs);
            sw.AutoFlush = true;
            Console.SetOut(sw);
            Console.SetError(sw);
        }

        public void LogException(Exception e)
        {
            Console.WriteLine(e);
        }

        public void LogMsg(string msg)
        {
            Console.WriteLine(msg);
        }
    }

    public class StreamWriterWithTimestamp : StreamWriter
    {
        public StreamWriterWithTimestamp(Stream stream)
            : base(stream)
        {
        }

        private string GetTimestamp()
        {
            return "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] ";
        }

        public override void WriteLine(string value)
        {
            base.WriteLine(GetTimestamp() + value);
        }

        public override void Write(string value)
        {
            base.Write(GetTimestamp() + value);
        }
    }
}