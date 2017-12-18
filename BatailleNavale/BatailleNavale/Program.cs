using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatailleNavale
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] grille1 = new int[10, 10];
            Grille.AfficherGrille(grille1);
            Console.ReadKey();
        }
    }
}
