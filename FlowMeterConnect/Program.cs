using System;
using System.Threading;
using System.Windows.Forms;
using log4net;
using log4net.Config;

namespace FlowMeterConnect
{
    internal static class Program
    {
        public static readonly ILog log = LogManager.GetLogger("FlowMeter");
        private const string AppGuid = "c0a76b5a-12ab-45c5-b9d9-d693faa6e7b9";

        /// <summary>
        ///     Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            using (Mutex mutex = new Mutex(false, "Global\\" + AppGuid))
            {
                if (!mutex.WaitOne(0, false))
                {
                    MessageBox.Show("Приложение уже запущенно.");
                    return;
                }
                XmlConfigurator.Configure();
                log.Debug("App started");
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
        }
        /*Application.ThreadException += ExceptionHandler.ApplicationOnThreadException;
        AppDomain.CurrentDomain.UnhandledException += ExceptionHandler.CurrentDomain_UnhandledException;*/
        //
    }
}