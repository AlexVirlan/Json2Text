namespace Json2Text
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            List<string> args = Environment.GetCommandLineArgs().Skip(1).ToList();
            args.ForEach(arg => arg = arg.Replace("-", string.Empty));

            Mutex mutex = new Mutex(true, "AlexVirlan.Json2Text", out bool newInstance);
            if (newInstance)
            {
                ApplicationConfiguration.Initialize();
                frmMain frmMain = new(args);
                Application.Run(frmMain);
            }
            else
            {
                Environment.Exit(0);
            }
            mutex.ReleaseMutex();
        }
    }
}
