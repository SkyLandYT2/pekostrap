using PekoraStrap;
using System;
using System.Windows.Forms;

namespace PekoStrap
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Check if the program was launched with a pekora-player URI
            if (args.Length > 0 && args[0].StartsWith("pekora-player:", StringComparison.OrdinalIgnoreCase))
            {
                // Instantiate Pekostrap directly without launching the GUI
                var pekostrap = new Pekostrap(args);
            }
            else
            {
                // Launch the GUI as usual
                Application.Run(new MainForm());
            }
        }
    }
}