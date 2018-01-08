using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatailleNavale
{
    class IA
    {

        private static int[] DerniersTirsX = new int[0];
        private static int[] DerniersTirsY = new int[0];
        private static int DernierTirTouche = -1;
        private static int Direction = 0;
        private static int Combo = 0;

        public static void PositionIA(int joueur, out int x, out int y)
        {
            x = 0; y = 0;
            if (Jeu.NiveauJeu == Jeu.Niveau.FACILE)
                PositionIAFacile(joueur, out x, out y);
            else if (Jeu.NiveauJeu == Jeu.Niveau.NORMAL)
                PositionIANormal(joueur, out x, out y);
        }

        private static void PositionIAFacile(int joueur, out int x, out int y)
        {
            do
            {
                Random rdm = new Random();
                x = rdm.Next(0, Grille.LargeurGrille);
                y = rdm.Next(0, Grille.HauteurGrille);

            }
            while (Grille.ObtenirGrilleDecouverteJoueur(joueur)[x, y] != (int)Grille.Cases.VIDE);
        }

        public static void SignalerTouche()
        {
            IA.DernierTirTouche = IA.DerniersTirsX.Length - 1;
            IA.Combo++;
        }

        public static void SignalerCoule()
        {
            IA.Combo = 0;
        }

        private static void PositionAuHasard(int joueur, out int x, out int y)
        {
            bool positionValide = true;
            do
            {
                Random rdm = new Random();
                x = rdm.Next(0, Grille.LargeurGrille);
                y = rdm.Next(0, Grille.HauteurGrille);
                positionValide = Grille.ObtenirGrilleDecouverteJoueur(joueur)[x, y] != (int)Grille.Cases.VIDE;
                int i = 0;
                while (i < IA.DerniersTirsX.Length && positionValide == true)
                {
                    if (x == IA.DerniersTirsX[i] && y == IA.DerniersTirsY[i])
                    {
                        positionValide = false;
                    }
                    i++;
                }
            }
            while (positionValide == false);
        }

        private static void PositionIANormal(int joueur, out int x, out int y)
        {
            x = 0; y = 0;
            if(IA.DerniersTirsX.Length <= 0 || IA.Combo <= 0 || IA.Combo > 5)
                IA.PositionAuHasard(joueur, out x, out y);
            else
            {
                if(IA.DernierTirTouche < IA.DerniersTirsX.Length - 1)
                {
                    IA.Direction++;
                }
                switch(IA.Direction)
                {
                    case 0:
                        x = IA.DerniersTirsX[IA.DernierTirTouche] - 1;
                        y = IA.DerniersTirsY[IA.DernierTirTouche];
                        break;
                    case 1:
                        x = IA.DerniersTirsX[IA.DernierTirTouche];
                        y = IA.DerniersTirsY[IA.DernierTirTouche] - 1;
                        break;
                    case 2:
                        x = IA.DerniersTirsX[IA.DernierTirTouche] + 1;
                        y = IA.DerniersTirsY[IA.DernierTirTouche];
                        break;
                    case 3:
                        x = IA.DerniersTirsX[IA.DernierTirTouche];
                        y = IA.DerniersTirsY[IA.DernierTirTouche] + 1;
                        break;
                    default:
                        IA.Combo = 0;
                        IA.PositionIANormal(joueur, out x, out y);
                        break;
                }
            }

            IA.DerniersTirsX = IA.DerniersTirsX.Concat(new int[] { x }).ToArray();
            IA.DerniersTirsY = IA.DerniersTirsY.Concat(new int[] { y }).ToArray();






        }
    }
}
