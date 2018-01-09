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

        /// <summary>
        /// Nombre de type de bateaux différents 
        /// </summary>
        public const int NombreTypesBateaux = 5;

        /// <summary>
        /// Enumération des différents types de bateaux existants
        /// </summary>
        public enum TYPES
        {
            CONTRETORPILLEUR = 0,
            CROISEUR = 1,
            SOUSMARIN = 2,
            CUIRASSE = 3,
            PORTEAVION = 4,
        };

        /// <summary>
        /// Tableau contenant la longueur des différents types de bateaux 
        /// </summary>
        public static int[] LongueurBateaux =
        {
            2,
            3,
            3,
            4,
            5
        };

        // Vie et position des bateaux du joueur 1
        public static int[,] PositionBateauxJ1 = new int[Bateau.NombreTypesBateaux, 4];
        public static int[] VieBateauxJ1 = new int[Bateau.NombreTypesBateaux];

        // Vie et position des bateaux du joueur 2
        public static int[,] PositionBateauxJ2 = new int[Bateau.NombreTypesBateaux, 4];
        public static int[] VieBateauxJ2 = new int[Bateau.NombreTypesBateaux];


        public static int[,] ObtenirPositionBateauxJoueur(int joueur)
        {
            if (joueur == 1)
                return Bateau.PositionBateauxJ1;
            else if (joueur == 2)
                return Bateau.PositionBateauxJ2;
            throw new Exception("L'index de joueur ne peut être que 1 ou 2.");
        }

        public static int[] ObtenirVieBateauxJoueur(int joueur)
        {
            if (joueur == 1)
                return Bateau.VieBateauxJ1;
            else if (joueur == 2)
                return Bateau.VieBateauxJ2;
            throw new Exception("L'index de joueur ne peut être que 1 ou 2.");
        }

        /// <summary>
        /// Retourne l'alignement du bateau de type type du joueur joueur 
        /// </summary>
        /// <param name="joueur">Joueur dont on doit retourner l'alignement d'un bateau</param>
        /// <param name="type">Type de bateau dont on doit retourner l'alignement</param>
        /// <returns>L'alignement du bateau</returns>
        public static ALIGNEMENT ObtenirAlignementBateau(int joueur, Bateau.TYPES type)
        {
            int[,] positionbateaux = Bateau.ObtenirPositionBateauxJoueur(joueur);
            if (positionbateaux[(int)type, 0] == positionbateaux[(int)type, 2])
                return ALIGNEMENT.COLONNE;
            else
                return ALIGNEMENT.LIGNE;
        }

        /// <summary>
        /// Place les bateaux du joueur passé en paramètre de manière aléatoire
        /// </summary>
        /// <param name="joueur">Joueur dont on doit placer les bateaux</param>
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
                int essais = 0;
                do
                {
                    alignement = (Bateau.ALIGNEMENT)rnd.Next(0, 2);
                    x = rnd.Next(0, Grille.LargeurGrille);
                    y = rnd.Next(0, Grille.HauteurGrille);

                    if (alignement == Bateau.ALIGNEMENT.LIGNE)
                    {
                        if (x + Bateau.LongueurBateaux[i] >= Grille.LargeurGrille)
                            x = x - (Grille.LargeurGrille - Bateau.LongueurBateaux[i]);
                        x1 = x + Bateau.LongueurBateaux[i];
                        y1 = y;
                    }
                    if (alignement == Bateau.ALIGNEMENT.COLONNE)
                    {
                        if(y + Bateau.LongueurBateaux[i] >= Grille.HauteurGrille)
                            y = y - (Grille.LargeurGrille - Bateau.LongueurBateaux[i]);
                        y1 = y + Bateau.LongueurBateaux[i];
                        x1 = x;
                    }
                    essais++;

                }
                while (Bateau.VerifierColisionBateaux(joueur, x, y, x1, y1, (Bateau.TYPES)i) == false && essais < 200);
                if (essais >= 200)
                    throw new Exception("Impossible de placer tous les bateaux sur la grille associée au joueur J" + joueur);
                Bateau.PlacerBateau(joueur, (Bateau.TYPES)i, x, y, alignement);
            }
        }

        /// <summary>
        /// Détermine si les positions passées en paramètres sont libres ou occupées par un bateau
        /// </summary>
        /// <param name="joueur">Joueur dont on doit tester les bateaux</param>
        /// <param name="x1">Position x du premier point de la droite à tester</param>
        /// <param name="y1">Position y du premier point de la droite à tester</param>
        /// <param name="x2">Position x du deuxième point de la droite à tester</param>
        /// <param name="y2">Position y du deuxième point de la droite à tester</param>
        /// <param name="type">Type de bateau associé aux points à tester</param>
        /// <returns>Vrai si position libre, Faux si position occupée</returns>
        public static bool VerifierColisionBateaux(int joueur, int x1, int y1, int x2, int y2, Bateau.TYPES type)
        {
            int[,] grille = Grille.ObtenirGrilleJoueur(joueur);
            for (int o = 0; o < Bateau.LongueurBateaux[(int)type]; o++)
            {
                // Si le bateau est aligné en colonne
                if (x1 == x2)
                {
                    if (grille[x1, y1 + o] != (int)Grille.Cases.VIDE)
                        return false;
                }
                else //sinon le bateau est aligné en ligne
                {
                    if (grille[x1 + o, y1] != (int)Grille.Cases.VIDE)
                        return false;
                }

            }
            return true;
        }

        /// <summary>
        /// Paramètre les positions du bateau passé en paramètre dans la liste des positions de bateau
        /// </summary>
        /// <param name="joueur">Joueur associé au bateau à placer</param>
        /// <param name="type">Type de bateau à placer</param>
        /// <param name="x">Position x du premier point de la droite associée au bateau à placer</param>
        /// <param name="y">Position y du premier point de la droite associée au bateau à placer</param>
        /// <param name="alignement">Alignement du bateau à placer</param>
        public static void PlacerBateau(int joueur, Bateau.TYPES type, int x, int y, Bateau.ALIGNEMENT alignement)
        {
            int[,] positionBateaux = Bateau.ObtenirPositionBateauxJoueur(joueur);
            int[] vieBateaux = Bateau.ObtenirVieBateauxJoueur(joueur);
            int x1;
            int y1;
            

            if (alignement == ALIGNEMENT.LIGNE)
            {
                x1 = x + Bateau.LongueurBateaux[(int)type] - 1;
                y1 = y;
            }
            else
            {
                y1 = y + Bateau.LongueurBateaux[(int)type] - 1;
                x1 = x;
            }

            if (x < 0 || y < 0 || x1 >= Grille.LargeurGrille || y1 >= Grille.LargeurGrille)
                throw new Exception("Impossible de placer le bateau. Il se trouve en dehors de la grille.");

            vieBateaux[(int)type] = Bateau.LongueurBateaux[(int)type];
            positionBateaux[(int)type,0] = x;
            positionBateaux[(int)type,1] = y;
            positionBateaux[(int)type, 2] = x1;
            positionBateaux[(int)type, 3] = y1;

            Grille.mettreaJourGrilleUnBateau(Grille.ObtenirGrilleJoueur(joueur), x, y, x1, y1);
        }



        /// <summary>
        /// Modifie la vie du bateau passé en paramètre associé au joueur passé en paramètres
        /// </summary>
        /// <param name="joueur">Joueur dont on doit modifier un bateau</param>
        /// <param name="type">Type de bateau dont on doit modifier la vie </param>
        /// <param name="x">Position x à la quelle le bateau a été touché</param>
        /// <param name="y">Position y à laquelle le bateau a été touché</param>
        /// <returns>Vrai si le bateau est coulé, Faux sinon</returns>
        public static bool ToucherBateau(int joueur, Bateau.TYPES type, int x, int y)
        {
            int[,] positionbateau = Bateau.ObtenirPositionBateauxJoueur(joueur);
            int[] vieBateaux = Bateau.ObtenirVieBateauxJoueur(joueur);
            int[,] grille = Grille.ObtenirGrilleJoueur(joueur);
            grille[x, y] = (int)Grille.Cases.TOUCHE;
            int[,] decouverte = Grille.ObtenirGrilleDecouverteJoueur(Joueur.ObtenirAutreJoueur(joueur));
            decouverte[x, y] = (int)Grille.Cases.TOUCHE;
            //vieBateaux[(int)type] = vieBateaux[(int)type] - 1;
            vieBateaux[(int)type] = 0;

            if (vieBateaux[(int)type] <= 0)
            {
                // On marque la bateau comme coulé sur la grille
                for (int o = 0; o < Bateau.LongueurBateaux[(int)type]; o++)
                {
                    if (Bateau.ObtenirAlignementBateau(joueur, type) == ALIGNEMENT.COLONNE)
                    {
                        grille[positionbateau[(int)type, 0], positionbateau[(int)type, 1] + o] = (int)Grille.Cases.COULE;
                        decouverte[positionbateau[(int)type, 0], positionbateau[(int)type, 1] + o] = (int)Grille.Cases.COULE;
                    }
                    else
                    {
                        grille[positionbateau[(int)type, 0] + o, positionbateau[(int)type, 1]] = (int)Grille.Cases.COULE;
                        decouverte[positionbateau[(int)type, 0] + o, positionbateau[(int)type, 1]] = (int)Grille.Cases.COULE;
                    }
                }
                Console.WriteLine("Un bateau a été coulé.");
                return true;
            }
            return false;
        }

        public static bool Tirer(int joueur, int x, int y, out bool coule)
        {
            coule = false;
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
                
                trouve = positionBateaux[i, 0] <= x && x <= positionBateaux[i, 2] && positionBateaux[i, 1] <= y && y <= positionBateaux[i, 3];
                i++;
            }

            if (trouve == true)
                coule = Bateau.ToucherBateau(Joueur.ObtenirAutreJoueur(joueur), (Bateau.TYPES)(i - 1), x, y);
            else
            {
                int[,] decouverte = Grille.ObtenirGrilleDecouverteJoueur(joueur);
                decouverte[x, y] = (int)Grille.Cases.DECOUVERT_VIDE;
            }
            return trouve;

        }
    }
}
