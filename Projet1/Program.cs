using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Projet1
{
    class Program
    {
        //cette fonction permet de remplir le tableau, elle a en parametre un tableau de string et renvoi un tableau de double
        static double[] RemplirTableau(string[] soustab)
        {
            double[] newsoustab = new double[5];
            for (int i = 0; i < soustab.Length; i++)
                newsoustab[i] = Double.Parse(soustab[i], CultureInfo.InvariantCulture);

            return newsoustab;
        }
        //cette fonction permet de lire les fichiers, elle a en parametre un string et une liste et renvoie rien.
        static void ReadFile(String path, List<double[]> echantillon)
        {
            int counter = 0;
            string line;

            //chemin relatif projet1/bin/debug/netcoreapp1/iris.txt
            string chemin = Path.GetFullPath(path);
            //Console.WriteLine(chemin);
            // Read the file and display it line by line. 
            if (File.Exists(chemin))
            {
                StreamReader file =
                    new StreamReader(chemin);
                while ((line = file.ReadLine()) != null)
                {
                    //Console.WriteLine(line);
                    string[] soustab = line.Split(' ');
                    if (counter > 0)
                    {
                        double[] newsoustab = RemplirTableau(soustab);
                        echantillon.Add(newsoustab);
                    }
                    counter++;
                }

                file.Close();
                //Console.WriteLine("There were {0} lines.", counter);
            }
            else
                Console.WriteLine("Erreur à l'ouverture du fichier, chemin erronné");
            //AffichageTableau(x4);
        }
        //cette fonction permet d'afficher le tableau, elle a en parametre un tableau de double et renvoi rien
        static void AffichageTableau(double[] tableau)
        {
            for (int i = 0; i < tableau.Length; i++)
                Console.WriteLine(" Valeur : " + tableau[i]);
            Console.WriteLine("Fin");
        }
        //cette fonction permet de remplir le tableau, elle a en parametre un tableau de string et renvoi un tableau de double
        static void AffichageTableau2D(double[,] tableau)
        {
            for (int i = 0; i < tableau.GetLength(0); i++)
            {
                for (int j = 0; j < tableau.GetLength(1); j++)
                {
                    Console.Write(tableau[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("Fin");
        }
        //cette fonction demande une saisie a l'utilisateur, elle a en parametre des doubles et renvoi rien
        static void SaisitsUtilisateurIndividu(ref double x1, ref double x2, ref double x3, ref double x4)
        {
            bool saisie = false;

            do
            {
                Console.Write("Saisissez la variable x1 ");
                saisie = double.TryParse(Console.ReadLine(), out x1);
            } while (saisie == false || x1 <= 0);

            do
            {
                Console.Write("Saisissez la variable x2 ");
                saisie = double.TryParse(Console.ReadLine(), out x2);
            } while (saisie == false || x2 <= 0);

            do
            {
                Console.Write("Saisissez la variable x3 ");
                saisie = double.TryParse(Console.ReadLine(), out x3);
            } while (saisie == false || x3 <= 0);

            do
            {
                Console.Write("Saisissez la variable x4 ");
                saisie = double.TryParse(Console.ReadLine(), out x4);
            } while (saisie == false || x4 <= 0);

        }
        //cette fonction permet d'afficher la liste, elle a en parametre une liste de double et renvoi rien
        static void AffichageListe(List<double[]> liste)
        {
            foreach (var el in liste)
            {
                for (int i = 0; i < el.Length; i++)
                {
                    Console.Write(el[i] + " ");
                }
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("----- PROJET SDA : LEQSIOUER Mehdi & QOTB Badr -----");

            List<double[]> echantillon = new List<double[]>();

            ReadFile("iris.txt", echantillon);
            ArbreDecision arbre = new ArbreDecision(echantillon);



            double x1N = 6.0;
            double x2N = 2.2;
            double x3N = 5.0;
            double x4N = 1.5;

            string lettre = "";
            do
            {
                Console.WriteLine("----- MENU -----");
                Console.WriteLine("-----  Entrer 1 : Afficher la hauteur de l'arbre -----");
                Console.WriteLine("-----  Entrer 2 : Afficher la largeur de l'arbre -----");
                Console.WriteLine("-----  Entrer 3 : Afficher l'arbre sous forme arborescente -----");
                Console.WriteLine("-----  Entrer 4 : Afficher les feuilles -----");
                Console.WriteLine("-----  Entrer 5 : Prédire la variable Y d'un individu à saisir -----");
                Console.WriteLine("-----  Entrer 6 : Précision des 10 échantillons (pour Y = 2 !) de l'énoncé question 6) -----");
                Console.WriteLine("-----  Entrer 7 : Créer un nouvel arbre -----");
                Console.WriteLine("----- Presser Q ou q pour quitter -----");
                lettre = Console.ReadLine();

                switch (lettre)
                {
                    case "1":
                        Console.WriteLine("Hauteur : " + arbre.HauteurArbre(arbre.Racine));
                        Console.WriteLine();
                        break;
                    case "2":
                        Console.WriteLine("Largeur : " + arbre.LargeurArbre(arbre.Racine));
                        Console.WriteLine();
                        break;
                    case "3":
                        Console.WriteLine("Affichage arbre, précisions pour Y = " + arbre.Y);
                        arbre.AffichageArborescence(arbre.Racine);
                        Console.WriteLine();
                        break;
                    case "4":
                        arbre.AfficherFeuille(arbre.Racine, 0);
                        Console.WriteLine();
                        break;
                    case "5":
                        SaisitsUtilisateurIndividu(ref x1N, ref x2N, ref x3N, ref x4N);
                        double[] vals = { 0, x1N, x2N, x3N, x4N };
                        arbre.InsertionIndivius(arbre.Racine, vals);
                        Console.WriteLine();
                        break;
                    case "6":
                        double[,] tab = { { 7.7, 3.0, 6.1, 2.3 }, {6.2,2.8,4.8,1.8 } , {5.5,2.5,4.0,1.3 },{ 6.7,3.3,5.7,2.5},{ 6.0,2.2,5.0,1.5},{ 6.0,2.7,5.1,1.6}, {5.7,2.6,3.5,1.0 }, {5.8,2.6,4.0,1.2 },
                        { 5.1,3.4,1.5,0.2},{ 5.4,3.9,1.3,0.4} };
                        ArbreDecision abr = new ArbreDecision(echantillon, 2, 10, 90, 10, 8);
                        for (int i = 0; i < 9; i++)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Test pour : x1 =" + tab[i, 0] + " x2 =" + tab[i, 1] + " x3 =" + tab[i, 2] + " x4 =" + tab[i, 3]);
                            double[] tmp = { 0, tab[i, 0], tab[i, 1], tab[i, 2], tab[i, 3] };
                            abr.InsertionIndivius(abr.Racine, tmp);
                            Console.WriteLine();
                        }
                        break;
                    case "7":
                        arbre = new ArbreDecision(echantillon);
                        break;
                    case "Q":
                    case "q":
                        Console.WriteLine("Fin du programme");
                        break;
                    default:
                        Console.WriteLine("Erreur dans la saisie ! ");
                        break;
                }

            } while (!lettre.Equals("Q") && !lettre.Equals("q"));

            Console.ReadKey();
        }

    }
}
