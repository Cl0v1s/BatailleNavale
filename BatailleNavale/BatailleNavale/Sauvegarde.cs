using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatailleNavale
{
    class Sauvegarde
    {
        public static string NomFichier = "Sauvegarde.txt";


        /// <summary>
        /// Convertit le tableau multidimensionneldimensionnel objet en données textuelles pouvant être sauvegardées
        /// </summary>
        /// <param name="lignes">Données existantes</param>
        /// <param name="objet">Tableau multidimensionnel à convertir</param>
        /// <returns>Nouvelles données</returns>
        private static string[] ConvertirObjetEnLigne(string[] lignes, int[,] objet)
        {
            string[] nouvelleslignes = new string[objet.GetLength(0)];
            for (int i = 0; i < objet.GetLength(0); i++)
            {
                StringBuilder line = new StringBuilder();
                for (int j = 0; j < objet.GetLength(1); j++)
                    line.Append(objet[i, j]).Append(" ");
                nouvelleslignes[i] = line.ToString();
            }
            return lignes.Concat(nouvelleslignes).ToArray();

        }


        /// <summary>
        /// Convertit le tableau unidimensionnel objet en données textuelles pouvant être sauvegardées
        /// </summary>
        /// <param name="lignes">Données existantes</param>
        /// <param name="objet">Tableau unidimensionnel à convertir</param>
        /// <returns>Nouvelles données</returns>
        private static string[] ConvertirObjetEnLigne(string[] lignes, int[] objet)
        {
            StringBuilder line = new StringBuilder();
            for (int j = 0; j < objet.Length; j++)
                line.Append(objet[j]).Append(" ");
            return lignes.Concat(new string[] { line.ToString() }).ToArray();
        }

        /// <summary>
        /// Convertit une ligne textuelle du fichier de sauvegarde en tableau de données numériques 
        /// </summary>
        /// <param name="ligne">Ligne textuelle contenant les données</param>
        /// <returns>Le tableau de données numériques contenues dans la chaine de caractères</returns>
        private static int[] ConvertirLigneEnTableauEntier(string ligne)
        {
            string[] data = ligne.Split(' ');
            int[] resultat = new int[data.Length-1];
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i].Length <= 0)
                    continue;
                resultat[i] = Convert.ToInt32(data[i]);
            }
            return resultat;
        }

        public static bool DemandeEffacerAnciennePartie()
        {
            Console.Clear();
            ConsoleKey reponse;
            do
            {
                Console.WriteLine("======= Sauvegarder une partie ===");
                Console.WriteLine("Il existe déjà une sauvegarde précédente, voulez-vous l'écraser pour sauvegarder la partie courante ? (O/N)");
                reponse = Console.ReadKey().Key;
            }
            while (reponse != ConsoleKey.O && reponse != ConsoleKey.N);
            if (reponse == ConsoleKey.O)
                return true;
            return false;
        }

        /// <summary>
        /// Sauvegarde les données de la partie en cours dans un fichier texte de sauvegarde
        /// </summary>
        public static void Sauvegarder()
        {
            if (File.Exists(Sauvegarde.NomFichier) && Sauvegarde.DemandeEffacerAnciennePartie() == false)
                return;
            // Sauvegarde positions J1
            string[] lignes = Sauvegarde.ConvertirObjetEnLigne(new string[0], Bateau.ObtenirPositionBateauxJoueur(1));
            // Sauvegarde vies J1
            lignes = Sauvegarde.ConvertirObjetEnLigne(lignes, Bateau.ObtenirVieBateauxJoueur(1));
            // Sauvegarde positions J2
            lignes = Sauvegarde.ConvertirObjetEnLigne(lignes, Bateau.ObtenirPositionBateauxJoueur(2));
            // Sauvegarde vies J2
            lignes = Sauvegarde.ConvertirObjetEnLigne(lignes, Bateau.ObtenirVieBateauxJoueur(2));
            // Sauvegarde grille j1
            lignes = Sauvegarde.ConvertirObjetEnLigne(lignes, Grille.ObtenirGrilleJoueur(1));
            // Sauvegarde grille decouverte J1
            lignes = Sauvegarde.ConvertirObjetEnLigne(lignes, Grille.ObtenirGrilleDecouverteJoueur(1));
            // Sauvegarde grille J2
            lignes = Sauvegarde.ConvertirObjetEnLigne(lignes, Grille.ObtenirGrilleJoueur(2));
            // Sauvegarde grille decouverte J2
            lignes = Sauvegarde.ConvertirObjetEnLigne(lignes, Grille.ObtenirGrilleDecouverteJoueur(2));
            File.WriteAllLines(Sauvegarde.NomFichier, lignes);
        }

        /// <summary>
        /// Charge les données depuis le fichier de sauvegarde
        /// </summary>
        public static void Charger()
        {
            string[] lignes = File.ReadAllLines(Sauvegarde.NomFichier);
            int index = 0;
            int[] data;
            // Chargement de la position des bateaux du joueur 1
            for(int i = 0; i < Bateau.NombreTypesBateaux; i++)
            {
                data = ConvertirLigneEnTableauEntier(lignes[i]);
                for(int u = 0; u < data.Length; u++)
                    Bateau.PositionBateauxJ1[i, u] = data[u];
            }
            index += Bateau.NombreTypesBateaux;
            // Chargement de la vie des bateaux du joueur 1
            Bateau.VieBateauxJ1 = ConvertirLigneEnTableauEntier(lignes[index]);
            index++;
            // Chargement de la position des bateaux du joueur 2
            for (int i = 0; i < Bateau.NombreTypesBateaux; i++)
            {
                data = ConvertirLigneEnTableauEntier(lignes[index+i]);
                for (int u = 0; u < data.Length; u++)
                    Bateau.PositionBateauxJ2[i, u] = data[u];
            }
            index += Bateau.NombreTypesBateaux;
            // Chargement de la vie des bateaux du joueur 2
            Bateau.VieBateauxJ2 = ConvertirLigneEnTableauEntier(lignes[index]);
            index++;
            // Chargement de la grille du joueur 1
            for (int i = 0; i < Grille.GrilleJ1.GetLength(0); i++)
            {
                data = ConvertirLigneEnTableauEntier(lignes[index+i]);
                for (int u = 0; u < data.Length; u++)
                    Grille.GrilleJ1[i, u] = data[u];
            }
            index += Grille.GrilleJ1.GetLength(0);
            // Chargement de la grille de découverte du joueur 1
            for (int i = 0; i < Grille.GrilleDecouverteJ1.GetLength(0); i++)
            {
                data = ConvertirLigneEnTableauEntier(lignes[index+i]);
                for (int u = 0; u < data.Length; u++)
                    Grille.GrilleDecouverteJ1[i, u] = data[u];
            }
            index += Grille.GrilleDecouverteJ1.GetLength(0);
            // Chargement de la grille du joueur 2
            for (int i = 0; i < Grille.GrilleJ2.GetLength(0); i++)
            {
                data = ConvertirLigneEnTableauEntier(lignes[index+i]);
                for (int u = 0; u < data.Length; u++)
                    Grille.GrilleJ2[i, u] = data[u];
            }
            index += Grille.GrilleJ2.GetLength(0);
            // Chargement de la grille de découverte du joueur 2
            for (int i = 0; i < Grille.GrilleDecouverteJ2.GetLength(0); i++)
            {
                data = ConvertirLigneEnTableauEntier(lignes[index+i]);
                for (int u = 0; u < data.Length; u++)
                    Grille.GrilleDecouverteJ2[i, u] = data[u];
            }
            index += Grille.GrilleDecouverteJ2.GetLength(0);

            // Gestion du nombre de tirs par salves 
            for(int i = 0; i < Bateau.NombreTypesBateaux; i++)
            {
                if (Bateau.VieBateauxJ1[i] <= 0)
                    Joueur.TailleSalveJ2--;
                if (Bateau.VieBateauxJ2[i] <= 0)
                    Joueur.TailleSalveJ1--;
            }

        }
    }
}
