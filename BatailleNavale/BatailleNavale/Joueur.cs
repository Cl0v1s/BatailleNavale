using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatailleNavale
{
    class Joueur
    {

        public static int TailleSalveJ1 = Bateau.NombreTypesBateaux;
        public static int TailleSalveJ2 = Bateau.NombreTypesBateaux;

        public static void Start()
        {
            Joueur.TailleSalveJ1 = Bateau.NombreTypesBateaux;
            Joueur.TailleSalveJ2 = Bateau.NombreTypesBateaux;
    }

        public static int ObtenirTailleSalve(int joueur)
        {
            if (joueur == 1)
                return TailleSalveJ1;
            else if (joueur == 2)
                return TailleSalveJ2;
            throw new Exception("L'index de joueur ne peut être que 1 ou 2.");
        }

        public static void ReglerTailleSalve(int joueur, int valeur)
        {
            if (joueur == 1)
                TailleSalveJ1 = valeur;
            else if (joueur == 2)
                TailleSalveJ2 = valeur;
            else 
                throw new Exception("L'index de joueur ne peut être que 1 ou 2.");
        }


        /// <summary>
        /// Demande une position à l'utilisateur
        /// </summary>
        /// <param name="x">Après exécution, contient la position X entrée par l'utilisateur</param>
        /// <param name="y">Après exécution, contient la position Y entrée par l'utilisateur</param>
        public static void DemanderPosition(int joueur, out int x, out int y)
        {
            x = -1;
            y = -1;
            do
            {
                Console.WriteLine("Veuillez entrer une position valide (Exemple --> C7) ");
                try
                {
                    string value = Console.ReadLine();
                    string lettre = value.Substring(0, 1);
                    
                    y = 0;
                    while (Grille.Lettres[y].ToUpper()!=lettre.ToUpper())
                    {
                        y++;
                    }
                    
                    string chiffre = value.Replace(lettre, "");
                    x = (Convert.ToInt32(chiffre)-1);

                }
                catch
                {
                    x = -1;
                    y = -1;
                }
            }
            while ((x<0 || x>9) || (y<0||y>9));

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
            Console.WriteLine("==TOUR DU JOUEUR "+joueur+"=============");
            // A faire évoluer si de deux joueurs 
            if (joueur == 1)
                Joueur.JouerHumain(joueur);
            else
                Joueur.JouerIA(joueur);
            Console.WriteLine("==FIN DU TOUR DU JOUEUR "+joueur+ "=============");
        }

        public static bool aPerdu(int joueur)
        {
            int vieRestante = 0;
            for (int i = 0; i < Bateau.NombreTypesBateaux; i++)
            {
                vieRestante += Bateau.ObtenirVieBateauxJoueur(joueur)[i];
            }
            if (vieRestante == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="joueur"></param>
        public static void JouerHumain(int joueur)
        {
            int x, y;
            int[,] salves = new int[Joueur.ObtenirTailleSalve(joueur),2];
            /*Console.WriteLine("Votre Grille:");
            Grille.AfficherGrille(Grille.ObtenirGrilleJoueur(joueur));
            Console.WriteLine("Ce que vous savez de la Grille de l'adversaire:");
            Grille.AfficherGrille(Grille.ObtenirGrilleDecouverteJoueur(joueur));*/
            for (int i = 0; i < salves.GetLength(0); i++)
            {
                Console.Clear();
                Console.WriteLine("==TOUR DU JOUEUR " + joueur + "=============");
                string infoGrilles = "Votre Grille:                          Ce que vous savez de la grille de votre adversaire:";
                Console.WriteLine(infoGrilles);
                Grille.AfficherDeuxGrillesCoteACote(Grille.ObtenirGrilleJoueur(joueur), Grille.ObtenirGrilleDecouverteJoueur(joueur));
                Console.WriteLine("Paramétrage du canon " + (i + 1) + "/" + salves.GetLength(0));
                DemanderPosition(joueur, out x, out y);
                salves[i, 0] = x;
                salves[i, 1] = y;
                Console.WriteLine("Un canon a été dirigé vers la cellule " + Grille.Lettres[y] + "" + (x + 1) + " ...");
            }
            Console.Clear();
            Console.WriteLine("------------------------");
            Console.WriteLine("Mise à feu des canons...");
            for (int i = 0; i < salves.GetLength(0); i++)
            {
                x = salves[i, 0];
                y = salves[i, 1];
                Console.Write("Tir sur la cellule " + Grille.Lettres[y] + "" + (x + 1) + " ...");
                bool coule = false;
                if (Bateau.Tirer(joueur, x, y, out coule) == true) // le joueur a touché
                {
                    if (coule == false)
                        Console.WriteLine("Vous avez touché un navire !");
                    else
                    {
                        Console.WriteLine("Un bateau a été coulé.");
                        Joueur.ReglerTailleSalve(joueur, Joueur.ObtenirTailleSalve(joueur) - 1);
                    }
                }
                else // le joueur n'a pas touché 
                {
                    Console.WriteLine("C'est un coup dans l'eau...");
                }
            }
        }

        public static void JouerIA(int joueur)
        {
            int x, y;
            int[,] salves = new int[Joueur.ObtenirTailleSalve(joueur), 2];
            for (int i = 0; i < salves.GetLength(0); i++)
            {
                IA.PositionIA(joueur, out x,out y, i);
                salves[i, 0] = x;
                salves[i, 1] = y;
            }

            for (int i = 0; i < salves.GetLength(0) ; i++)
            {
                x = salves[i, 0];
                y = salves[i, 1];
                Console.Write("L'ordinateur tire sur la cellule " + Grille.Lettres[y] + "" + (x + 1) + " ...");
                int[,] decouverte;
                bool coule = false;
                if (Bateau.Tirer(joueur, x, y, out coule) == true) // IA a touché
                {
                    IA.SignalerTouche(i);
                    if (coule)
                    {
                        Console.WriteLine("L'ordinateur a coulé un navire !");
                        IA.SignalerCoule(joueur, i);
                        Joueur.ReglerTailleSalve(joueur, Joueur.ObtenirTailleSalve(joueur) - 1);
                    }
                    else
                        Console.WriteLine("L'ordinateur a touché un navire !");
                }
                else // IA n'a pas touché 
                {
                    IA.SignalerRate(i);
                    Console.WriteLine("C'est un coup dans l'eau...");
                    decouverte = Grille.ObtenirGrilleDecouverteJoueur(joueur);
                    decouverte[x, y] = (int)Grille.Cases.DECOUVERT_VIDE;
                }
            }
        }
    }
}
