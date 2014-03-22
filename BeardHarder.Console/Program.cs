using BeardHarder.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeardHarder.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var x = new App();
            x.Go();

            System.Console.ReadKey();
        }
    }
}
