using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatailleNavale
{
    /// <summary>
    /// Classe statique permettant de réalier des opérations sur les grilles
    /// </summary>
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

        /// <summary>
        /// Lettre associées aux cellules
        /// </summary>
        public static string[] Lettres = new string[]
        {
            "A", "B", "C", "D", "E", "F", "G", "H", "I", "J"
        };

        /// <summary>
        /// Largeur de la grille
        /// </summary>
        public const int LargeurGrille = 10;

        /// <summary>
        /// Hauteur de la grille
        /// </summary>
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
        /// Permet d'afficher deux représentations de grilles côtes à cotes
        /// </summary>
        /// <param name="grille1">Grille à afficher à gauche</param>
        /// <param name="grille2">Grille à afficher à droite</param>
        public static void AfficherDeuxGrillesCoteACote(int[,] grille1, int[,] grille2)
        {
            string[] g1 = Grille.ConvertirGrilleVersTexte(grille1).Split("\n".ToCharArray());
            string[] g2 = Grille.ConvertirGrilleVersTexte(grille2).Split("\n".ToCharArray());

            for(int i = 0; i < g1.Length && i < g2.Length; i++)
            {
                string ligne = g1[i] + "    " + g2[i];
                Grille.AfficherLigneCouleur(ligne);
                if (i < g1.Length - 1 && i < g2.Length)
                    Console.Write("\n");
            }

        }

        /// <summary>
        /// Affiche une ligne d'une grille en gérant les couleurs des éléments qui y figurent 
        /// Appelé dans le cadre d'une grille, ralenti le programme mais améliore la lisibilité
        /// </summary>
        /// <param name="ligne">chaine de caractères représentant une ligne d'une grille</param>
        public static void AfficherLigneCouleur(string ligne)
        {
            if (ligne.Length <= 0)
                return;
            Console.Write(ligne[0]);
            for(int i = 1; i < ligne.Length;i++)
            {
                string cellule = ligne[i - 1] +""+ ligne[i];
                switch(cellule)
                {
                    case "|B":
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                    case "|X":
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    case "|O":
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    case "|0":
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        break;
                }
                Console.Write(ligne[i]);
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Affiche la grille passée en paramètre dans la console
        /// </summary>
        /// <param name="grille">Grille à afficher</param>
        public static void AfficherGrille(int[,] grille)
        {
            string[] lignes = Grille.ConvertirGrilleVersTexte(grille).Split("\n".ToCharArray());
            for(int i = 0; i < lignes.Length; i++)
            {
                Grille.AfficherLigneCouleur(lignes[i]);
                Console.WriteLine("");
            }
        }

       /// <summary>
       /// Retourne une représentation textuelle d'une grille (en vue de l'afficher) 
       /// </summary>
       /// <param name="grille">Grille à convertir</param>
       /// <returns>Une chaine de caractères contenant la représentation de la grille</returns>
        public static string ConvertirGrilleVersTexte(int[,] grille)
        {
            string res = "";
            res += ("     ");
            for (int i = 0; i < Grille.LargeurGrille; i++)
            {
                res += (Lettres[i]);
                if(i < Grille.LargeurGrille - 1)
                    res += ("  ");
            }
            res += ("\n");
            for (int i = 0; i < 10; i++)
            {
                res += ("   ");
                for (int j = 0; j < Grille.LargeurGrille; j++)
                {
                    res += ("+--");
                }
                res += ("+\n");
                if (i < 9)
                {
                    res += ((i + 1) + "  |");
                }
                else
                {
                    res += ((i + 1) + " |");
                }
                for (int j = 0; j < 10; j++)
                {
                    if (grille[i, j] == (int)Grille.Cases.PLEIN)
                        res += ("B");
                    else if (grille[i, j] == (int)Grille.Cases.VIDE)
                        res += (" ");
                    else if (grille[i, j] == (int)Grille.Cases.DECOUVERT_VIDE)
                        res += ("X");
                    else if (grille[i, j] == (int)Grille.Cases.TOUCHE)
                        res += ("O");
                    else if (grille[i, j] == (int)Grille.Cases.COULE)
                        res += ("0");
                    if (j != 9)
                    {
                        res += (" |");
                    }
                }
                res += (" |\n");
            }
            res += ("   +--+--+--+--+--+--+--+--+--+--+\n");
            return res;
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
                for (int i = y1; i <= y2; i++)
                {
                    grille[x1, i] = (int)Grille.Cases.PLEIN;
                }
            }
            if (y1 == y2)
            {

                for (int i = x1; i <= x2; i++)
                {
                    grille[i, y1] = (int)Grille.Cases.PLEIN;
                }
            }
        }
    }


}
