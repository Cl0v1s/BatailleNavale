using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatailleNavale
{
    class Grille
    {

        /// <summary>
        /// Enumération des états possibles pour les cases des grilles
        /// </summary>
        public enum Cases
        {
            VIDE = 0,
            PLEIN = 1,
            TOUCHE = 2,
            COULE = 3, 
            DECOUVERT_VIDE = 4
        };

        public const int LargeurGrille = 10;
        public const int HauteurGrille = 10;

        /// <summary>
        /// Grille de jeu du joueur 1
        /// </summary>
        public static int[,] GrilleJ1 = new int[LargeurGrille, HauteurGrille];

        /// <summary>
        /// Grille contenant les positions et les coups joués du joueur 1 sur le joueur 2
        /// </summary>
        public static int[,] GrilleDecouverteJ1 = new int[LargeurGrille, HauteurGrille];

        /// <summary>
        /// Grille de jeu du joueur 2
        /// </summary>
        public static int[,] GrilleJ2 = new int[LargeurGrille, HauteurGrille];

        /// <summary>
        /// Grille contenant les positions et les coups joués du joueur 2 sur le joueur 1
        /// </summary>
        public static int[,] GrilleDecouverteJ2 = new int[LargeurGrille, HauteurGrille];

        /// <summary>
        /// Retourne la grille du joueur passé en paramètre
        /// </summary>
        /// <param name="joueur">Joueur dont la grille doit être retournée</param>
        /// <returns>La grille du joueur passé en paramètre</returns>
        public static int[,] ObtenirGrilleJoueur(int joueur)
        {
            if (joueur == 1)
                return Grille.GrilleJ1;
            else if (joueur == 2)
                return Grille.GrilleJ2;
            throw new Exception("L'index de joueur ne peut être que 1 ou 2.");
        }

        /// <summary>
        /// Retourne la grille du joueur passé en paramètre
        /// </summary>
        /// <param name="joueur">Joueur dont la grille doit être retournée</param>
        /// <returns>La grille du joueur passé en paramètre</returns>
        public static int[,] ObtenirGrilleDecouverteJoueur(int joueur)
        {
            if (joueur == 1)
                return Grille.GrilleDecouverteJ1;
            else if (joueur == 2)
                return Grille.GrilleDecouverteJ2;
            throw new Exception("L'index de joueur ne peut être que 1 ou 2.");
        }


        /// <summary>
        /// Affiche la grille passée en paramètre
        /// </summary>
        /// <param name="grille">grille à afficher</param>
        public static void AfficherGrille(int[,] grille)
        {
            Console.WriteLine("     A  B  C  D  E  F  G  H  I  J");
            for (int i = 0; i < 10; i++)
            {
                Console.Write("   ");
                for (int j = 0; j < 10; j++)
                {
                    Console.Write("+--");
                }
                Console.WriteLine("+");
                if (i < 9)
                {
                    Console.Write((i + 1) + "  |");
                }
                else
                {
                    Console.Write((i + 1) + " |");
                }
                for (int j = 0; j < 10; j++)
                {
                    if(grille[i, j] == (int)Grille.Cases.PLEIN)
                        Console.Write("B");
                    else if (grille[i, j] == (int)Grille.Cases.VIDE || grille[i, j] == (int)Grille.Cases.DECOUVERT_VIDE)
                        Console.Write(" ");
                    else if (grille[i, j] == (int)Grille.Cases.TOUCHE)
                        Console.Write("O");
                    if (j != 9)
                    {
                        Console.Write(" |");
                    }
                }
                Console.WriteLine(" |");
            }
            Console.WriteLine("   +--+--+--+--+--+--+--+--+--+--+");

        }

        /// <summary>
        /// Met à jour la grille à partir de la position de plusieurs bateaux
        /// </summary>
        /// <param name="grille">Grille à mettre à jour</param>
        /// <param name="positionbateaux">Tableau en 2 dimensions contenant les positions de plusieurs bateaux</param>
        public static void mettreaJourGrillePlusieursBateaux(int[,]grille,int [,]positionbateaux)
        {
           
            for (int j = 0; j < (Bateau.NombreTypesBateaux); j++)
            {
                int x1 = positionbateaux[j,0];
                int y1 = positionbateaux[j,1];
                int x2 = positionbateaux[j,2];
                int y2 = positionbateaux[j,3];
                Grille.mettreaJourGrilleUnBateau(grille, x1, y1, x2, y2);
            }
        }

        /// <summary>
        /// Met à jour la grille à partir de la position d'un bateau
        /// </summary>
        /// <param name="grille">Grille à mettre à jour</param>
        /// <param name="x1">Position x du premier point de la droite</param>
        /// <param name="y1">Position y du premier point de la droite</param>
        /// <param name="x2">Position x du deuxième point de la droite</param>
        /// <param name="y2">Position y du deuxième point de la droite</param>
        public static void mettreaJourGrilleUnBateau(int[,] grille, int x1, int y1, int x2, int y2)
        {

            if (x1 == x2)
            {
                for (int i = y1; i < y2; i++)
                {
                    grille[x1, i] = (int)Grille.Cases.PLEIN;
                }
            }
            if (y1 == y2)
            {

                for (int i = x1; i < x2; i++)
                {
                    grille[i, y1] = (int)Grille.Cases.PLEIN;
                }
            }
        }
    }


}
