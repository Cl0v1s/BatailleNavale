using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatailleNavale
{
    class IA
    {

        public static void PositionIA(int joueur, out int x, out int y)
        {
            x = 0; y = 0;
            if (Jeu.NiveauJeu == Jeu.Niveau.FACILE)
                PositionIAFacile(joueur, out x, out y);
            else if (Jeu.NiveauJeu == Jeu.Niveau.NORMAL)
                PositionIANormal(joueur, out x, out y);
        }

        public static void PositionIAFacile(int joueur, out int x, out int y)
        {
            do
            {
                Random rdm = new Random();
                x = rdm.Next(0, Grille.LargeurGrille);
                y = rdm.Next(0, Grille.HauteurGrille);

            }
            while (Grille.ObtenirGrilleDecouverteJoueur(joueur)[x, y] != (int)Grille.Cases.VIDE);
        }

        public static void PositionIANormal(int joueur, out int x, out int y)
        {
            x = 1;
            y = 1;
        }
    }
}
