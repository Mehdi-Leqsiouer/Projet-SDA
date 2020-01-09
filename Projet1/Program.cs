using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Projet1
{
    class Program
    {

        static double[] RemplirTableau(string[] soustab, double[] x1, double[] x2, double[] x3, double[] x4, int counter)
        {
            double[] newsoustab = new double[5];
            for (int i = 0; i < soustab.Length; i++)
                newsoustab[i] = Double.Parse(soustab[i], CultureInfo.InvariantCulture);

            x1[counter - 1] = newsoustab[1];
            x2[counter - 1] = newsoustab[2];
            x3[counter - 1] = newsoustab[3];
            x4[counter - 1] = newsoustab[4];
            return newsoustab;
        }

        static void ReadFile(String path, List<double[]> echantillon, double[] x1, double[] x2, double[] x3, double[] x4)
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
                        double[] newsoustab = RemplirTableau(soustab, x1, x2, x3, x4, counter);
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

        static void AffichageTableau(double[] tableau)
        {
            for (int i = 0; i < tableau.Length; i++)
                Console.WriteLine(" Valeur : " + tableau[i]);
            Console.WriteLine("Fin");
        }

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

        static void AffichageListe(List<double[]> liste)
        {
            foreach (var el in liste)
            {
                for (int i = 0; i < el.Length; i++)
                {
                    Console.Write(el[i] +" ");
                }
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            List<double[]> echantillon = new List<double[]>();
            double[] x1 = new double[120];
            double[] x2 = new double[120];
            double[] x3 = new double[120];
            double[] x4 = new double[120];

            ReadFile("iris.txt", echantillon, x1, x2, x3, x4);
            ArbreDecision arbre = new ArbreDecision(echantillon, x1, x2, x3, x4);

            string lettre = "";

            double x1N = 6.0;
            double x2N = 2.2;
            double x3N = 5.0;
            double x4N = 1.5;

              do
              {
                  Console.WriteLine("----- MENU -----");
                  Console.WriteLine("----- Choix 1 : Afficher la hauteur de l'arbre -----");
                  Console.WriteLine("----- Choix 2 : Afficher la largeur de l'arbre -----");
                  Console.WriteLine("----- Choix 3 : Afficher l'arbre sous forme arborescente -----");
                  Console.WriteLine("----- Choix 4 : Afficher les feuilles -----");
                  Console.WriteLine("----- Choix 5 : Prédire la variable Y d'un individu à saisir -----");
                  Console.WriteLine("----- Presser Q ou q pour quitter -----");
                  lettre = Console.ReadLine();

                  switch (lettre)
                  {
                      case "1":
                          Console.WriteLine("Hauteur : " + arbre.HauteurArbre(arbre.Racine));
                          break;
                      case "2":
                          Console.WriteLine("Largeur : " + arbre.LargeurArbre(arbre.Racine));
                          break;
                      case "3":
                          arbre.AffichageArborescence(arbre.Racine,1);
                          break;
                      case "4":
                          arbre.AfficherFeuille(arbre.Racine);
                          break;
                      case "5":
                          SaisitsUtilisateurIndividu(ref x1N, ref x2N, ref x3N, ref x4N);
                          double[] vals = {0,x1N,x2N,x3N,x4N };
                          arbre.InsertionIndivius(arbre.Racine,vals);
                          break;
                  }

              } while (!lettre.Equals("Q") && !lettre.Equals("q"));

            //AffichageTableau2D(echantillon);

            //arbre.AffichageArborescence(arbre.Racine, 5);
            //SaisitsUtilisateurIndividu(ref x1N, ref x2N, ref x3N, ref x4N);
            //arbre.AffichageArbre(arbre.Racine, 1);

            //double[] valeurs = { 0,x1N, x2N, x3N, x4N };
           // arbre.InsertionIndivius(arbre.Racine, valeurs);
            //double[] vals = {0,x1N,x2N,x3N,x4N };
           // arbre.InsertionIndivius(arbre.Racine,valeurs);


            Console.WriteLine("Fin programme");
            Console.ReadKey();
        }

    }
}
