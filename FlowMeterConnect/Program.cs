using System;
using System.Windows.Forms;
using log4net;
using log4net.Config;

namespace FlowMeterConnect
{
    internal static class Program
    {
        public static readonly ILog log = LogManager.GetLogger("FlowMeter");

        /// <summary>
        ///     Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            XmlConfigurator.Configure();
            log.Debug("App started");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());

            /*Application.ThreadException += ExceptionHandler.ApplicationOnThreadException;
            AppDomain.CurrentDomain.UnhandledException += ExceptionHandler.CurrentDomain_UnhandledException;*/
            //
        }
    }
}