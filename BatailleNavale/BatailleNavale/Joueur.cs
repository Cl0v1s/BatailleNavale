using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatailleNavale
{
    class Joueur
    {
        /// <summary>
        /// Demande une position à l'utilisateur
        /// </summary>
        /// <param name="x">Après exécution, contient la position X entrée par l'utilisateur</param>
        /// <param name="y">Après exécution, contient la position Y entrée par l'utilisateur</param>
        public static void DemanderPosition(int joueur, out int x, out int y)
        {
            Console.WriteLine("Veuillez entrer une position (Exemple --> C7) ");
            x = -1;
            do
            {
                Console.WriteLine();
                try
                {
                    string value = Console.ReadLine();
                    string lettre = value.Substring(0, 1);
                    for 
                    string chiffre = value.Replace(lettre, "");
                    y = Convert.ToInt32(chiffre);


                }
                catch
                {
                    x = -1;
                }
            }
            while (x == -1 || x<0 || x>9);

        }

        /// <summary>
        /// Demande à l'utilisateur s'il veut continuer
        /// </summary>
        /// <returns>Vrai si il faut continuer, faux sinon</returns>
        public static bool DemanderContinuer()
        {
            ConsoleKey reponse;
            do
            {
                Console.WriteLine("Voulez-vous continuer à jouer ? (O/N)");
                reponse = Console.ReadKey().Key;
            }
            while (reponse != ConsoleKey.O && reponse != ConsoleKey.N);
            if (reponse == ConsoleKey.O)
                return true;
            return false;
        }

        /// <summary>
        /// A joueur donné, retourne l'index de l'autre joueur 
        /// </summary>
        /// <param name="joueur">Index du joueur courant</param>
        /// <returns>Retourne l'index de l'autre joueur</returns>
        public static int ObtenirAutreJoueur(int joueur)
        {
            if (joueur == 1)
                return 2;
            else if (joueur == 2)
                return 1;
            throw new Exception("L'index de joueur ne peut être que 1 ou 2.");
        }

        /// <summary>
        /// Lance le tour du joueur passé en paramètre
        /// </summary>
        /// <param name="joueur">Joueur dont c'est le tour de jouer</param>
        public static void Jouer(int joueur)
        {
            Console.WriteLine("==NOUVEAU TOUR DU JOUEUR "+joueur+"=============");
            // A faire évoluer si de deux joueurs 
            if (joueur == 1)
                Joueur.JouerHumain(joueur);
            else
                Joueur.JouerIA(joueur);
            Console.WriteLine("==FIN DU TOUR DU JOUEUR "+joueur+ "=============");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="joueur"></param>
        public static void JouerHumain(int joueur)
        {
            int x, y;
            Console.WriteLine("Votre Grille:");
            Grille.AfficherGrille(Grille.ObtenirGrilleJoueur(joueur));
            Console.WriteLine("Ce que vous savez de la Grille de l'adversaire:");
            Grille.AfficherGrille(Grille.ObtenirGrilleDecouverteJoueur(joueur));
            DemanderPosition(1,out x,out y);
            Console.WriteLine("Tire sur la cellule ["+(x+1)+", "+(y+1)+"] ...");
            int[,] decouverte;
            if (Bateau.Tirer(joueur, x, y) == true) // le joueur a touché
            {
                decouverte = Grille.ObtenirGrilleDecouverteJoueur(joueur);
                decouverte[x, y] = (int)Grille.Cases.TOUCHE;
                int[,] grille_autre = Grille.ObtenirGrilleJoueur(Joueur.ObtenirAutreJoueur(joueur));
                grille_autre[x, y] = (int)Grille.Cases.TOUCHE;
                Console.WriteLine("Vous avez touché un navire !");
            }
            else // le joueur n'a pas touché 
            {
                Console.WriteLine("C'est un coup dans l'eau...");
                decouverte = Grille.ObtenirGrilleDecouverteJoueur(joueur);
                decouverte[x, y] = (int)Grille.Cases.DECOUVERT_VIDE;
            }
        }

        public static void JouerIA(int joueur)
        {
            int x, y;
            IA.PositionIA(joueur, out x, out y);
            Console.WriteLine("Il tire sur la cellule [" + (x+1) + ", " + (y+1) + "] ...");
            int[,] decouverte;
            if (Bateau.Tirer(joueur, x, y) == true) // IA a touché
            {
                decouverte = Grille.ObtenirGrilleDecouverteJoueur(joueur);
                decouverte[x, y] = (int)Grille.Cases.TOUCHE;
                int[,] grille_autre = Grille.ObtenirGrilleJoueur(Joueur.ObtenirAutreJoueur(joueur));
                grille_autre[x, y] = (int)Grille.Cases.TOUCHE;
                Console.WriteLine("Il a touché un navire !");
            }
            else // IA n'a pas touché 
            {
                Console.WriteLine("C'est un coup dans l'eau...");
                decouverte = Grille.ObtenirGrilleDecouverteJoueur(joueur);
                decouverte[x, y] = (int)Grille.Cases.DECOUVERT_VIDE;
            }
        }
    }
}
