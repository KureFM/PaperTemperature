using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Core
{
    public static class Log
    {
        private static LogProvider logExceptionFile;

        public static void Init()
        {
            logExceptionFile = new LogProvider("exc.log");
        }

        public static void LogException(Exception e)
        {
            logExceptionFile.LogException(e);
        }
    }
}