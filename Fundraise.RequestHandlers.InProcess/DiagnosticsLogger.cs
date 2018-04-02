using EasyNetQ;
using System;

namespace Fundraise.RequestHandlers.InProcess
{
    public class DiagnosticsLogger : IEasyNetQLogger
    {
        public bool Debug { get; set; }
        public bool Info { get; set; }
        public bool Error { get; set; }

        public DiagnosticsLogger()
        {
            Debug = true;
            Info = true;
            Error = true;
        }

        public void DebugWrite(string format, params object[] args)
        {
            if (!Debug) return;
            System.Diagnostics.Debug.WriteLine("DEBUG: " + format, args);
        }

        public void InfoWrite(string format, params object[] args)
        {
            if (!Info) return;
            System.Diagnostics.Debug.WriteLine("INFO: " + format, args);
        }

        public void ErrorWrite(string format, params object[] args)
        {
            if (!Error) return;
            System.Diagnostics.Debug.WriteLine("ERROR: " + format, args);
        }

        public void ErrorWrite(Exception exception)
        {
            System.Diagnostics.Debug.WriteLine(exception.ToString());
        }
    }
}