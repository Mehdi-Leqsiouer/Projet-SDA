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
            int[] Y = new int[120];
            float[] x1 = new float[120];
            float[] x2 = new float[120];
            float[] x3 = new float[120];
            float[] x4 = new float [120];
            //chemin relatif projet1/bin/debug/netcoreapp1/iris.txt
            string chemin = Path.GetFullPath(path);
            Console.WriteLine(chemin);
            // Read the file and display it line by line. 
            if (File.Exists(chemin))
            {
                StreamReader file =
                    new StreamReader(chemin);
                while ((line = file.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                    if (counter > 0)
                    {
                        Y[counter-1] = line[0];
                        x1[counter - 1] = line[0];
                        x2[counter - 1] = line[0];
                        x3[counter - 1] = line[0];
                    }
                    counter++;
                }

                file.Close();
                Console.WriteLine("There were {0} lines.", counter);
            }
            else
                Console.WriteLine("Erreur à l'ouverture du fichier, chemin erronné");
            // Suspend the screen.  
        }

        static float Mediane(float[] val)
        {
            float res = 0;
            int n = val.Length;
            if (n % 2 == 0)
            {
                res = (val[n / 2] + val[(n / 2)+1]) / 2;
            }
            else
                res = val[(n+1) / 2];
            return res;
        }

        static float Maximum(float[] val)
        {
            float res = 0;
            for (int i =0;i<val.Length;i++)
            {
                if (val[i] > res)
                    res = val[i];
            }
            return res;
        }

        static float SecondMaximum(float[] val )
        {
            float res = 0;
            float max1 = Maximum(val);
            for (int i =0;i<val.Length;i++)
            {
                if (val[i] > res && val[i] < max1)
                    res = val[i];
            }
            return res;
        }

        static float MedianeCorrigee(float [] val)
        {
            float res = 0;
            float mediane = Mediane(val);
            float max = Maximum(val);
            if (mediane == max)
                res = SecondMaximum(val);
            else
                res = max;
            return res;
        }

        static void Main(string[] args)
        {
            int y = -1;
            int seuil1 = -1;
            int seuil2 = -1;
            int min = -1;
            int tailleMax = -1;
            //SaisitsUtilisateur(ref y, ref seuil1, ref seuil2, ref min, ref tailleMax);

            ReadFile("iris.txt");
            Console.ReadKey();
        }
    }
}
