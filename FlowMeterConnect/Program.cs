using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net;

namespace FlowMeterConnect
{
    static class Program
    {
        public static readonly ILog log = LogManager.GetLogger("FlowMeter");
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            log4net.Config.XmlConfigurator.Configure();
            log.Debug("App started");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());

            Application.ThreadException += ExceptionHandler.ApplicationOnThreadException;
            AppDomain.CurrentDomain.UnhandledException += ExceptionHandler.CurrentDomain_UnhandledException;
            //
        }
    }
}
