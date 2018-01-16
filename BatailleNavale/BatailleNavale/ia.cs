using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatailleNavale
{
    /// <summary>
    /// Classe statique permettant de gérer l'intelligence artificielle du jeu
    /// </summary>
    class IA
    {

        /// <summary>
        /// Liste des dernières positions x des tirs pour chacun des canons
        /// </summary>
        private static int[] DerniersTirsX = new int[Bateau.NombreTypesBateaux];

        /// <summary>
        /// Liste des dernières positions y des tirs pour chacun des canons
        /// </summary>
        private static int[] DerniersTirsY = new int[Bateau.NombreTypesBateaux];

        /// <summary>
        /// Liste des combos (enchainement de tirs touchés) pour chacun des canons
        /// </summary>
        private static int[] DerniersTirsCombo = new int[Bateau.NombreTypesBateaux];

        /// <summary>
        /// Liste contenant les directions prises pour chacuns des canons
        /// </summary>
        private static int[] DirectionsTirs = new int[Bateau.NombreTypesBateaux];

        /// <summary>
        /// Vrai si l'IA a été initialisée
        /// </summary>
        private static bool initialized = false;

        /// <summary>
        /// Initialise l'intelligence artificielle
        /// </summary>
        public static void Start()
        {
            IA.DerniersTirsX = new int[Bateau.NombreTypesBateaux];
            IA.DerniersTirsY = new int[Bateau.NombreTypesBateaux];
            IA.DerniersTirsCombo = new int[Bateau.NombreTypesBateaux];
            IA.DirectionsTirs = new int[Bateau.NombreTypesBateaux];
            for (int i = 0; i < IA.DirectionsTirs.Length; i++)
                IA.DirectionsTirs[i] = -1;
            IA.initialized = true;
        }

        /// <summary>
        /// Marque l'intelligence articifielle comme n'étant par encore initialisée
        /// </summary>
        public static void Reset()
        {
            IA.initialized = false;
        }

        /// <summary>
        /// Calcule la position d'un tir de l'IA vers une cellule donnée
        /// </summary>
        /// <param name="joueur">joueur correspondant à l'IA</param>
        /// <param name="x">Entrée-sortie, à la fin de la fonction, contient la position x du tir </param>
        /// <param name="y">Entrée-sortie, à la fin de la fonction, contient la position y du tir</param>
        /// <param name="canon">Index du canon utilisé par l'IA</param>
        public static void PositionIA(int joueur, out int x, out int y, int canon = 0)
        {
            x = 0; y = 0;
            if (Jeu.NiveauJeu == Jeu.Niveau.ENFANT)
                PositionIAEnfant(joueur, out x, out y);
            else if (Jeu.NiveauJeu == Jeu.Niveau.FACILE)
                PositionIAFacile(joueur, out x, out y, canon);
            else if (Jeu.NiveauJeu == Jeu.Niveau.NORMAL)
                PositionIANormal(joueur, out x, out y, canon);
        }

        /// <summary>
        /// Calcule "très naivement" la position d'un tir de l'IA vers une cellule donnée
        /// </summary>
        /// <param name="joueur">joueur correspondant à l'IA</param>
        /// <param name="x">Entrée-sortie, à la fin de la fonction, contient la position x du tir</param>
        /// <param name="y">Entrée-sortie, à la fin de la fonction, contient la position y du tir</param>
        private static void PositionIAEnfant(int joueur, out int x, out int y)
        {
            do
            {
                Random rdm = Program.random;
                x = rdm.Next(0, Grille.LargeurGrille);
                y = rdm.Next(0, Grille.HauteurGrille);

            }
            while (Grille.ObtenirGrilleDecouverteJoueur(joueur)[x, y] != (int)Grille.Cases.VIDE);
        }

        /// <summary>
        /// Calcule naivement la position d'un tir de l'IA vers une cellule donnée
        /// </summary>
        /// <param name="joueur">joueur correspondant à l'IA</param>
        /// <param name="x">Entrée-sortie, à la fin de la fonction, contient la position x du tir</param>
        /// <param name="y">Entrée-sortie, à la fin de la fonction, contient la position y du tir</param>
        private static void PositionIAFacile(int joueur, out int x, out int y, int canon)
        {
            x = 0;
            y = 0;
            IA.PositionAuHasard(joueur,out x,out y, canon);
        }


        /// <summary>
        /// Calcule "intelligement" la position d'un tir de l'IA vers une cellule donnée
        /// </summary>
        /// <param name="joueur">joueur correspondant à l'IA</param>
        /// <param name="x">Entrée-sortie, à la fin de la fonction, contient la position x du tir</param>
        /// <param name="y">Entrée-sortie, à la fin de la fonction, contient la position y du tir</param>
        /// <param name="canon">Index du canon utilisé</param>
        private static void PositionIANormal(int joueur, out int x, out int y, int canon)
        {
            if (IA.initialized == false)
                IA.Start();

            if (IA.DirectionsTirs[canon] == -1)
            {
                IA.PositionAuHasard(joueur, out x, out y, canon);
                IA.DerniersTirsX[canon] = x;
                IA.DerniersTirsY[canon] = y;
                return;
            }

            switch (IA.DirectionsTirs[canon])
            {
                default:
                case 0:
                    x = IA.DerniersTirsX[canon] - 1;
                    y = IA.DerniersTirsY[canon];
                    break;
                case 1:
                    x = IA.DerniersTirsX[canon];
                    y = IA.DerniersTirsY[canon] - 1;
                    break;
                case 2:
                    x = IA.DerniersTirsX[canon] + 1;
                    y = IA.DerniersTirsY[canon];
                    break;
                case 3:
                    x = IA.DerniersTirsX[canon];
                    y = IA.DerniersTirsY[canon] + 1;
                    break;
            }

            IA.DerniersTirsX[canon] = x;
            IA.DerniersTirsY[canon] = y;

            IA.DerniersTirsCombo[canon]++;

            if(x < 0 || x >= Grille.LargeurGrille || y < 0 || y >= Grille.HauteurGrille || Grille.ObtenirGrilleDecouverteJoueur(joueur)[x, y] != (int)Grille.Cases.VIDE)
            {
                IA.SignalerRate(canon);
                IA.PositionIANormal(joueur, out x, out y, canon);
            }

        }

        /// <summary>
        /// Signale à l'IA que le canon passé en paramètre a touché un bateau
        /// </summary>
        /// <param name="canon">Canon concerné</param>
        public static void SignalerTouche(int canon)
        {
            if (IA.DirectionsTirs[canon] == -1)
                IA.DirectionsTirs[canon] = 0;
        }

        /// <summary>
        /// Signale à l'IA que le canon passé en paramètre a coulé un bateau.
        /// </summary>
        /// <param name="joueur">Joueur correspondant à l'IA</param>
        /// <param name="canon">Canon concerné</param>
        public static void SignalerCoule(int joueur, int canon)
        {
            IA.ResetCanon(canon);
            if(Joueur.ObtenirTailleSalve(joueur) > 1) // Si ce n'est pas le dernier canon encore utilisé, on le supprime afin de conserver les états de tout les autres canons encore utilisés
            { // Pour éviter d'"oublier" un canon
                for(int i = canon; i < Bateau.NombreTypesBateaux -1; i++)
                {
                    IA.DerniersTirsX[i] = IA.DerniersTirsX[i + 1];
                    IA.DerniersTirsY[i] = IA.DerniersTirsY[i + 1];
                    IA.DerniersTirsCombo[i] = IA.DerniersTirsCombo[i + 1];
                    IA.DirectionsTirs[i] = IA.DirectionsTirs[i + 1];
                }
            }
        }

        /// <summary>
        /// Remet à zéro les données associées au canon passé en paramètre
        /// </summary>
        /// <param name="canon">Index du canon concerné</param>
        private static void ResetCanon(int canon)
        {
            IA.DerniersTirsCombo[canon] = 0;
            IA.DirectionsTirs[canon] = -1;
        }

        /// <summary>
        /// Signale à l'IA que le canon passé en paramètre a raté
        /// </summary>
        /// <param name="canon">Canon concerné</param>
        public static void SignalerRate(int canon)
        {
            if (IA.DirectionsTirs[canon] == -1)
                return;

            int x, y;
            switch (IA.DirectionsTirs[canon])
            {
                default:
                case 0:
                    x = IA.DerniersTirsX[canon] + IA.DerniersTirsCombo[canon];
                    y = IA.DerniersTirsY[canon];
                    break;
                case 1:
                    x = IA.DerniersTirsX[canon];
                    y = IA.DerniersTirsY[canon] + IA.DerniersTirsCombo[canon];
                    break;
                case 2:
                    x = IA.DerniersTirsX[canon] - IA.DerniersTirsCombo[canon];
                    y = IA.DerniersTirsY[canon];
                    break;
                case 3:
                    x = IA.DerniersTirsX[canon];
                    y = IA.DerniersTirsY[canon] - IA.DerniersTirsCombo[canon];
                    break;
            }
            IA.DerniersTirsX[canon] = x;
            IA.DerniersTirsY[canon] = y;

            IA.DerniersTirsCombo[canon] = 0;
            IA. DirectionsTirs[canon]++;

            if (IA.DirectionsTirs[canon] > 3)
                IA.ResetCanon(canon);
        }


        /// <summary>
        /// Renvoie une position au hasard de manière "non-naive" (c'est à dire en prenant en compte les tirs des autres canons, et les tirs déja effectués)
        /// </summary>
        /// <param name="joueur">Joueur associé à l'IA</param>
        /// <param name="x">Entrée-sortie, à la fin de la fonction, contient la position x du tir</param>
        /// <param name="y">Entrée-sortie, à la fin de la fonction, contient la position y du tir</param>
        /// <param name="canon">Index du canon utilisé</param>
        private static void PositionAuHasard(int joueur, out int x, out int y, int canon)
        {
            bool positionValide = true;
            do
            {
                Random rdm = Program.random;
                x = rdm.Next(0, Grille.LargeurGrille);
                y = rdm.Next(0, Grille.HauteurGrille);
                // On vérifie si on a déjà tiré dans cette case
                positionValide = Grille.ObtenirGrilleDecouverteJoueur(joueur)[x, y] == (int)Grille.Cases.VIDE;
                int i = 0;
                // On vérifie que ce tour si, un autre canon n'est pas déjà réglé dans cette direction
                while (i < canon && positionValide == true)
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

        /*
        public static void SignalerTouche()
        {
            IA.DernierTirTouche = IA.DerniersTirsX.Length - 1;
            IA.Combo++;
        }

        public static void SignalerCoule()
        {
            IA.Combo = 0;
        }

        

        /*
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
        }*/
    }
}
