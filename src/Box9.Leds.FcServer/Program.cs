using System;
using System.Diagnostics;

namespace Box9.Leds.FcBinaries
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Windows Fadecandy server...");

            var processStartInfo = new ProcessStartInfo(".\\fcserver.exe")
            {
                RedirectStandardOutput = true,
                UseShellExecute = false
            };

            var process = Process.Start(processStartInfo);
            process.WaitForExit();
        }
    }
}
