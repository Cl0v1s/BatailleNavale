using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatailleNavale
{
    class Program
    {
        public static Random random = new Random();

        public static void ClearInputBuffer()
        {
            while (Console.KeyAvailable)
            {
                Console.ReadKey(false);
            }
        }


        static void Main(string[] args)
        {
            Jeu.MenuPrincipal();
        }
    }
}
