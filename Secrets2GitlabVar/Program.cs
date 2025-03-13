namespace Secrets2GitlabVar
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

            Mutex mutex = new Mutex(true, "AlexVirlan.Secrets2GitlabVar", out bool newInstance);
            if (newInstance)
            {
                ApplicationConfiguration.Initialize();
                frmMain frmMain = new(args);
                frmMain.Show();
                Application.Run();
            }
            else
            {
                Environment.Exit(0);
            }
            mutex.ReleaseMutex();
        }
    }
}
