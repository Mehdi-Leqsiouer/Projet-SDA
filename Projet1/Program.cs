using System;
using System.IO;

namespace Projet1
{
    class Program
    {
   
        static void SaisitsUtilisateur(ref int Y, ref int seuil1, ref int seuil2, ref int min,
            ref int tailleMax)
        {
            bool saisie = false;
            
            do
            {
                Console.Write("Saisissez la valeur Y à prédire ");
                saisie = int.TryParse(Console.ReadLine(), out Y);
            } while (saisie == false || Y <= 0 || Y >= 4);

            do
            {
                Console.Write("Saisissez le seuil minimal de précision en % ");
                saisie = int.TryParse(Console.ReadLine(), out seuil1);
            } while (saisie == false || seuil1 <= 0 || seuil1 > 100);

            do
            {
                Console.Write("Saisissez le seuil maximal de précision en % ");
                saisie = int.TryParse(Console.ReadLine(), out seuil2);
            } while (saisie == false || seuil2 <= 0 || seuil2 > 100 || seuil2 <= seuil1);

            do
            {
                Console.Write("Saisissez le nombre minimal d'invidivu par échantillon ");
                saisie = int.TryParse(Console.ReadLine(), out min);
            } while (saisie == false || min <= 0 || min > 100);

            do
            {
                Console.Write("Saisissez la taille maximale de l'arbre ");
                saisie = int.TryParse(Console.ReadLine(), out tailleMax);
            } while (saisie == false || tailleMax <= 0);
        }

        static void ReadFile(String path)
        {
            int counter = 0;
            string line;
            //chemin relatif projet1/bin/debug/netcoreapp1/iris.txt
            string chemin = Path.GetFullPath(path);
            // Read the file and display it line by line. 
            if (System.IO.File.Exists(chemin))
            {
                System.IO.StreamReader file =
                    new System.IO.StreamReader(chemin);
                while ((line = file.ReadLine()) != null)
                {
                    System.Console.WriteLine(line);
                    counter++;
                }

                file.Close();
                System.Console.WriteLine("There were {0} lines.", counter);
            }
            else
                Console.WriteLine("Erreur à l'ouverture du fichier, chemin erronné");
            // Suspend the screen.  
        }

        static void Main(string[] args)
        {
            int y = -1;
            int seuil1 = -1;
            int seuil2 = -1;
            int min = -1;
            int tailleMax = -1;
            SaisitsUtilisateur(ref y, ref seuil1, ref seuil2, ref min, ref tailleMax);

            ReadFile("iris.txt");
            Console.ReadKey();
        }
    }
}
