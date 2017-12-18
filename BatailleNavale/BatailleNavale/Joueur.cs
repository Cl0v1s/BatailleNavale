using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatailleNavale
{
    class Joueur
    {

        public static void DemanderPosition(out int x, out int y)
        {
            Console.WriteLine("Veuillez entrer une position");
            x = -1;
            do
            {
                Console.Write("X:");
                try
                {
                    x = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    x = -1;
                }
            }
            while (x == -1);
            Console.WriteLine("");
            y = -1;
            do
            {
                Console.Write("Y:");
                try
                {
                    y = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    y = -1;
                }
            }
            while (y == -1);
        }

        public static int ObtenirAutreJoueur(int joueur)
        {
            if (joueur == 1)
                return 2;
            else if (joueur == 2)
                return 1;
            throw new Exception("L'index de joueur ne peut être que 1 ou 2.");
        }
    }
}
