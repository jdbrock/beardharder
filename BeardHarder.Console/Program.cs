using BeardHarder.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BeardHarder.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var app = new App();
            var lastSabnzbdRestart = DateTime.MinValue;

            while (true)
            {
                System.Console.WriteLine("Looking for failed episodes...");

                app.RetryFailedEpisodes();

                System.Console.WriteLine("Sleeping for 30 minutes...");
                System.Console.WriteLine();
                System.Console.WriteLine();

                Thread.Sleep(1000 * 60 * 30);

                if (lastSabnzbdRestart.AddHours(4) < DateTime.Now)
                {
                    System.Console.WriteLine("Restarting SABnzbd to fix bug with hung jobs...");

                    app.RestartSabnzbd();
                    lastSabnzbdRestart = DateTime.Now;

                    System.Console.WriteLine("Sleeping for 5 minutes...");

                    Thread.Sleep(1000 * 60 * 5);
                }
            }
        }
    }
}
