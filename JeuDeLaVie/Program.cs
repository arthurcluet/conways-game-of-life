using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EsilvGui;

namespace JeuDeLaVie
{
    class Program
    {
        [System.STAThreadAttribute()]
        static void Main(string[] args)
        {
            int[,] grille = GenererGrille(80, 80, 0.5);

            Fenetre gui = new Fenetre(grille, 10, 0, 0, "Génération 1");

            for(int i = 0; i < 2000; i++)
            {
                Next(grille);
                gui.RafraichirTout();
                gui.changerMessage("Génération " + (i + 2));
                System.Threading.Thread.Sleep(100);
            }
        


            Console.ReadKey();
        }

        static void Next(int[,] grille)
        {
            int[,] grilleSuivante = new int[grille.GetLength(0), grille.GetLength(1)];
            for(int i = 0; i < grille.GetLength(0); i++)
            {
                for(int j = 0; j < grille.GetLength(1); j++)
                {
                    int voisins = VoisinageCellule(grille, i, j);
                    if (grille[i, j] == 1 && voisins < 2) grilleSuivante[i, j] = 0;
                    else if (grille[i, j] == 1 && voisins > 3) grilleSuivante[i, j] = 0;
                    else if (grille[i, j] == 0 && voisins == 3) grilleSuivante[i, j] = 1;
                    else grilleSuivante[i, j] = grille[i, j];
                }
            }
            for (int i = 0; i < grille.GetLength(0); i++)
            {
                for (int j = 0; j < grille.GetLength(1); j++)
                {
                    grille[i, j] = grilleSuivante[i, j];
                }
            }
        }

        static int[,] GenererGrille(int hauteur, int largeur, double tauxRemplissage)
        {
            int[,] grille;

            if(hauteur > 0 && largeur > 0 && tauxRemplissage >= 0.1 && tauxRemplissage <= 0.9)
            {
                grille = new int[hauteur, largeur];
                int casesARemplir = (int)(hauteur * largeur * tauxRemplissage);
                Random random = new Random();

                for (int i = 0; i < hauteur; i++)
                {
                    for (int j = 0; j < largeur; j++)
                    {
                        grille[i, j] = 0;
                    }
                }

                while (casesARemplir > 0)
                {
                    int ligne = random.Next(0, hauteur);
                    int colonne = random.Next(0, hauteur);
                    if (grille[ligne, colonne] == 0)
                    {
                        grille[ligne, colonne] = 1;
                        casesARemplir--;
                    }
                }
            } else
            {
                grille = null;
            }

            return grille;
        }

        static void AfficherGrille(int[,] grille)
        {
            if(grille == null || grille.GetLength(0) < 1 || grille.GetLength(1) < 1)
            {
                // Erreur
                Console.WriteLine("(grille vide)");
            } else
            {
                for (int i = 0; i < grille.GetLength(0); i++)
                {
                    for (int j = 0; j < grille.GetLength(1); j++)
                    {
                        string affichage;
                        if (grille[i, j] == 1) affichage = "# ";
                        else if (grille[i, j] == 0) affichage = ". ";
                        else affichage = "X ";
                        // Si une valeur de la grille est différente de 0 ou 1 on affiche un X en guise d'erreur
                        // On aurait pu aussi vérifier l'intégrité de la grille avant l'affichage et ne rien afficher en cas d'erreur
                        Console.Write(affichage);
                    }
                    Console.WriteLine();
                }
            }
        }

        static int VoisinageCellule(int[,] grille, int ligne, int colonne)
        {
            int cellulesVivantesVoisines = 0;
            for(int i = ligne - 1; i <= ligne + 1; i++)
            {
                for(int j = colonne - 1; j <= colonne +1 ; j++)
                {
                    if(i != ligne || j != colonne)
                    {
                        // Pour chaque case autour de la case [i,j]
                        int y = i;
                        int x = j;
                        if (y < 0) y += grille.GetLength(0);
                        if (x < 0) x += grille.GetLength(1);
                        if (y >= grille.GetLength(0)) y -= grille.GetLength(0);
                        if (x >= grille.GetLength(1)) x -= grille.GetLength(1);
                        if(grille[y, x] == 1) cellulesVivantesVoisines++;

                    }
                }
            }
            return cellulesVivantesVoisines;
        }


    }
}
