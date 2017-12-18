using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatailleNavale
{
    class Bateau
    {
        public enum ALIGNEMENT
        {
            LIGNE = 0,
            COLONNE = 1
        }

        public const int NombreTypesBateaux = 5;
        public enum TYPES
        {
            CONTRETORPILLEUR = 0,
            CROISEUR = 1,
            SOUSMARIN = 2,
            CUIRASSE = 3,
            PORTEAVION = 4,
        };
        public static int[] LongueurBateaux =
        {
            2,
            3,
            3,
            4,
            5
        };


        public static int[,] PositionBateauxJ1 = new int[Bateau.NombreTypesBateaux, 4];
        public static int[] VieBateauxJ1 = new int[Bateau.NombreTypesBateaux];

        public static int[,] PositionBateauxJ2 = new int[Bateau.NombreTypesBateaux, 4];
        public static int[] VieBateauxJ2 = new int[Bateau.NombreTypesBateaux];

        public static void PlacerBateauxAuHasard(int joueur)
        {
            Random rnd = new Random();
            Bateau.ALIGNEMENT alignement = ALIGNEMENT.COLONNE;
            int x = 0;
            int y = 0;
            int x1 = 0;
            int y1 = 0;
            for(int i = 0; i < Bateau.NombreTypesBateaux; i++)
            {
                alignement = (Bateau.ALIGNEMENT)rnd.Next(0, 2);
                x = rnd.Next(0, Grille.LargeurGrille);
                y = rnd.Next(0, Grille.HauteurGrille);

                if (alignement == Bateau.ALIGNEMENT.LIGNE)
                {
                    if (x + Bateau.LongueurBateaux[i] > Grille.LargeurGrille)
                        x = x - Bateau.LongueurBateaux[i];
                    y1 = y;
                    x1 = x + Bateau.LongueurBateaux[i];
                }
                if (alignement == Bateau.ALIGNEMENT.COLONNE && x + Bateau.LongueurBateaux[i] > Grille.HauteurGrille)
                    y = y - Bateau.LongueurBateaux[i];

                for(int j = 0; j < i; j++)
                {

                }
            }
        }

        public static bool VerifierColisionBateaux(int joueur, Bateau.TYPES type1, Bateau.TYPES type2)
        {
            int[,] positionBateaux = null;
            if (joueur == 1)
            {
                positionBateaux = Bateau.PositionBateauxJ1;
            }
            else if (joueur == 2)
            {
                positionBateaux = Bateau.PositionBateauxJ2;
            }
            
            //TODO: faire ce truc

            return false;
        }

        public static bool PlacerBateau(int joueur, Bateau.TYPES type, int x, int y, Bateau.ALIGNEMENT alignement)
        {
            int[,] positionBateaux = null;
            int[] vieBateaux = null;
            int x1;
            int y1;
            if(joueur == 1)
            {
                positionBateaux = Bateau.PositionBateauxJ1;
                vieBateaux = Bateau.VieBateauxJ1;
            }
            else if(joueur == 2)
            {
                positionBateaux = Bateau.PositionBateauxJ2;
                vieBateaux = Bateau.VieBateauxJ2;
            }

            //TODO: vérifier que le bateau rentre dans la grille return false sinon
            if (alignement == ALIGNEMENT.LIGNE)
            {
                x1 = x + Bateau.LongueurBateaux[(int)type];
                y1 = y;
            }
            else
            {
                y1 = y + Bateau.LongueurBateaux[(int)type];
                x1 = x;
            }

            vieBateaux[(int)type] = Bateau.LongueurBateaux[(int)type];
            positionBateaux[(int)type,0] = x;
            positionBateaux[(int)type,1] = y;
            positionBateaux[(int)type, 2] = x1;
            positionBateaux[(int)type, 3] = y1;
            return true;
        }

        public static void ToucherBateau(int joueur, Bateau.TYPES type)
        {
            int[] vieBateaux = null;
            if (joueur == 1)
            {
                vieBateaux = Bateau.VieBateauxJ1;
            }
            else if (joueur == 2)
            {
                vieBateaux = Bateau.VieBateauxJ2;
            }
            vieBateaux[(int)type] = vieBateaux[(int)type] - 1;
            if (vieBateaux[(int)type] <= 0)
                Console.WriteLine("Un bateau a été coulé.");
        }

        public static bool Tirer(int joueur, int x, int y)
        {
            int[,] positionBateaux = null;
            int[] vieBateaux = null;
            if (joueur == 1)
            {
                positionBateaux = Bateau.PositionBateauxJ2;
                vieBateaux = Bateau.VieBateauxJ2;
            }
            else if (joueur == 2)
            {
                positionBateaux = Bateau.PositionBateauxJ1;
                vieBateaux = Bateau.VieBateauxJ1;
            }
            bool trouve = false;
            int i = 0;
            while(trouve == false && i < Bateau.NombreTypesBateaux)
            {
                // On voit si le point est sur la même droite que le bateau
                int dxc = x - positionBateaux[i,0];
                int dyc = y - positionBateaux[i, 1];

                int dxl = positionBateaux[i, 2] - positionBateaux[i, 0];
                int dyl = positionBateaux[i, 3] - positionBateaux[i, 1];

                int cross = dxc * dyl - dyc * dxl;
                if (cross == 0)
                {

                    // On vérifie si le point est compris sur le segment du bateau
                    if (Math.Abs(dxl) >= Math.Abs(dyl))
                    {
                        if (dxl > 0)
                            trouve = positionBateaux[i, 0] <= x && x <= positionBateaux[i, 2];
                        else
                            trouve = positionBateaux[i, 2] <= x && x <= positionBateaux[i, 0];
                    }
                    else
                    {
                        if (dyl > 0)
                            trouve = positionBateaux[i, 1] <= y && y <= positionBateaux[i, 3];
                        else
                            trouve = positionBateaux[i, 3] <= y && y <= positionBateaux[i, 1];
                    }
                }
                i++;
            }

            if (trouve == true)
                Bateau.ToucherBateau(Joueur.ObtenirAutreJoueur(joueur), (Bateau.TYPES)(i - 1));
            return trouve;

        }
    }
}
