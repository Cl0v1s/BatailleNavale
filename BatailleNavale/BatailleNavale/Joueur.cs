using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatailleNavale
{
    class Joueur
    {

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
