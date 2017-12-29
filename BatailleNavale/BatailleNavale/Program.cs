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
            Bateau.PositionBateauxJ1[0, 0] = 1;
            Sauvegarde.Charger();
            /*int[,] positionbateau = new int[,] { { 0, 0, 0, 3 }, { 3, 4, 6, 4 }, { 2, 6, 5, 6 }, {9,9,9,9} };
            int[,] grille1 = new int[10, 10];
            Grille.mettreaJourGrillePlusieursBateaux(grille1, positionbateau);
            Console.ReadKey();*/
        }
    }
}
