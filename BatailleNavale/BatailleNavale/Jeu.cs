using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace BatailleNavale
{
    class Jeu
    {
        public enum Niveau
        {
            FACILE = 1, 
            NORMAL = 2
        };

        public static Niveau NiveauJeu = Niveau.FACILE;


        public static void MenuPrincipal()
        {
            Console.Clear();
            Console.WriteLine("======= Bataille Navale =======");
            Console.WriteLine("Version 1.0 dévelopée par Hugo Le Tarnec et Clovis Portron");
            ConsoleKey key;
            do
            {
                Console.WriteLine("Veuillez choisir une option ci-dessous");
                Console.WriteLine("(N)ouvelle partie | (C)arger une partie sauvegardée | (Q)uitter");
                key = Console.ReadKey().Key;
            }
            while (key != ConsoleKey.N && key != ConsoleKey.C && key != ConsoleKey.Q);
            Program.ClearInputBuffer();
            if (key == ConsoleKey.N)
                Jeu.MenuNouvellePartie();
            else if (key == ConsoleKey.C)
                Jeu.ChargerPartie();
            else if (key == ConsoleKey.Q)
                Jeu.Quitter();
        }

        public static void MenuNouvellePartie()
        {
            Console.Clear();
            Console.WriteLine("======= Nouvelle partie =======");
            ConsoleKey key;
            do
            {
                Console.WriteLine("Veuillez choisir une option ci-dessous");
                Console.WriteLine("(F) Commencer une partie contre l'ordinateur niveau Facile \n(N) Commencer une partie contre l'ordinateur niveau Normal \n(R)etour");
                key = Console.ReadKey().Key;
            }
            while (key != ConsoleKey.F && key != ConsoleKey.N && key != ConsoleKey.R);
            Program.ClearInputBuffer();
            if (key == ConsoleKey.R)
                Jeu.MenuPrincipal();
            else if(key == ConsoleKey.F)
            {
                Jeu.NiveauJeu = Niveau.FACILE;
                Jeu.DemarrerNouvellePartie();
            }
            else if(key == ConsoleKey.N)
            {
                Jeu.NiveauJeu = Niveau.NORMAL;
                Jeu.DemarrerNouvellePartie();
            }
        }

        public static void ChargerPartie()
        {
            Console.Clear();
            Joueur.Start();
            Console.WriteLine("======= Charger une partie =======");
            string[] sauvegardes = Sauvegarde.RecupererFichiersSauvegarde();
            if(sauvegardes.Length <= 0)
            {
                Console.WriteLine("Aucune partie sauvegardée n'a été trouvée.");
                Console.WriteLine("Appuyez sur une touche pour continuer...");
                Console.ReadKey(false);
                Jeu.MenuPrincipal();
                return;
            }
            Console.WriteLine("Veuillez sélectionner une sauvegarde ci-dessous");
            for (int i = 0; i < sauvegardes.Length; i++)
            {
                Console.WriteLine("(" + (i + 1) + ")" + sauvegardes[i]);
            }
            int index = -1;
            do
            {
                try
                {
                    index = Convert.ToInt32(Console.ReadLine());
                    index = index - 1;
                    if (index < 0 || index >= sauvegardes.Length)
                        index = -1;
                }
                catch(Exception)
                {
                    index = -1;
                }
                if(index == -1)
                {
                    Console.WriteLine("Ce n'est pas une sélection valide. Veuillez réessayer.");
                }
            }
            while (index == -1);

            Sauvegarde.ReglerFichierSauvegarde(sauvegardes[index]);

            Console.Clear();
            Console.WriteLine("======= Charger une partie =======");
            try
            {
                Sauvegarde.Charger();
            }
            catch(Exception e)
            {
                Console.WriteLine("Impossible de charger la partie. Etes-vous sûr d'avoir un fichier de sauvegarde existant ?");
                Console.WriteLine("Appuyez sur une touche pour continuer...");
                Console.ReadKey(false);
                Jeu.MenuPrincipal();
            }
            Console.WriteLine("La partie a été chargée !");
            Console.WriteLine("Appuyez sur une touche pour continuer...");
            Console.ReadKey(false);
            Jeu.DeroulementPartie();
        }

        public static void DemarrerNouvellePartie()
        {
            
            Console.Clear();
            Joueur.Start();


            // Initialisation des grilles
            Grille.GrilleJ1 = new int[Grille.LargeurGrille, Grille.HauteurGrille];
            Grille.GrilleDecouverteJ1 = new int[Grille.LargeurGrille, Grille.HauteurGrille];

            Grille.GrilleJ2 = new int[Grille.LargeurGrille, Grille.HauteurGrille];
            Grille.GrilleDecouverteJ2 = new int[Grille.LargeurGrille, Grille.HauteurGrille];

            // Initialisation des données des bateaux des joueurs 
            Bateau.PositionBateauxJ1 = new int[Bateau.NombreTypesBateaux, 4];
            Bateau.VieBateauxJ1 = new int[Bateau.NombreTypesBateaux];

            Bateau.PositionBateauxJ2 = new int[Bateau.NombreTypesBateaux, 4];
            Bateau.VieBateauxJ2 = new int[Bateau.NombreTypesBateaux];

            Bateau.PlacerBateauxAuHasard(1);
            Bateau.PlacerBateauxAuHasard(2);

            //TODO: modifier ce code si on souhaite ajouter un mode JCJ
            Console.WriteLine("======= Nouvelle partie =======");
            Grille.AfficherGrille(Grille.GrilleJ1);
            Console.WriteLine("-------------------------------");
            Console.WriteLine("Etes-vous satisfait de ce placement ?");
            Console.WriteLine("(O)ui | N(on)");
            ConsoleKey key;
            do
            {
                key = Console.ReadKey().Key;
            }
            while (key != ConsoleKey.O && key != ConsoleKey.N);
            Program.ClearInputBuffer();
            if (key == ConsoleKey.O)
                Jeu.DeroulementPartie();
            else
                Jeu.DemarrerNouvellePartie();

        }

        public static void DeroulementPartie()
        {
            int joueur = 1;
            IA.Reset();
            while (true)
            {
                Console.Clear();
                Joueur.Jouer(joueur);
                joueur = Joueur.ObtenirAutreJoueur(joueur);
                Console.WriteLine("-- Appuyez sur une touche pour passer au tour de l'autre joueur --");
                Console.ReadKey(false);
                Console.Clear();
                Joueur.Jouer(joueur);
                joueur = Joueur.ObtenirAutreJoueur(joueur);
                if (Joueur.DemanderContinuer() == false)
                {
                    Sauvegarde.Sauvegarder();
                    Jeu.MenuPrincipal();
                }
            }
        }

        public static void Quitter()
        {
            Console.WriteLine("Au revoir !");
            System.Environment.Exit(0);
        }

    }
}
