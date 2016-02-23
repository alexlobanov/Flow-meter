using System;
using System.Threading;

namespace FlowMeterConnect
{
    public static class ExceptionHandler
    {
        public static void ApplicationOnThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Program.log.Fatal("EXCEPTION!! Msg: " + e.Exception.Message + "\n Stack trace: \n" + e.Exception.StackTrace +
                              "\n sourse: \n" + e.Exception.Source);
        }

        public static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                var errorMsg = "An application error occurred. Please contact the adminstrator " +
                               "with the following information:\n\n";
                var ex = (Exception) e.ExceptionObject;
                Program.log.Fatal(errorMsg + ex.Message + "\n\nStack Trace:\n" + ex.StackTrace
                                  + "\n sourse: \n" + ex.Source);
            }
            catch (Exception exc)
            {
                var errorMsg = "Fatal Non-UI Error. Could not write the error to the event log. Reason: "
                               + exc.Message;
                Program.log.Fatal(errorMsg);
            }
        }
    }
}