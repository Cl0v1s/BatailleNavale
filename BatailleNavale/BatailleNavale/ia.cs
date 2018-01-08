using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatailleNavale
{
    class IA
    {

        public static void PositionIAN0(int joueur, out int x, out int y)
        {
            do
            {
                Random rdm = new Random();
                x = rdm.Next(0, Grille.LargeurGrille);
                y = rdm.Next(0, Grille.HauteurGrille);

            }
            while (Grille.ObtenirGrilleDecouverteJoueur(joueur)[x, y] != (int)Grille.Cases.VIDE);
        }
        public static void PositionIAN1(int joueur, out int x, out int y)
        {
            do
            {
                Random rdm = new Random();
                x = rdm.Next(0, Grille.LargeurGrille);
                y = rdm.Next(0, Grille.HauteurGrille);

            }
            while (Grille.ObtenirGrilleDecouverteJoueur(joueur)[x, y] != (int)Grille.Cases.VIDE);


        }
    }
}
