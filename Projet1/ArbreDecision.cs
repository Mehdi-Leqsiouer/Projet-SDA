using System;

namespace Projet1
{
    class ArbreDecision
    {
        //Attributs
        private Sommet racine;
        int y;
        int seuil1;
        int seuil2;
        int min;
        int tailleMax;
        //Constructeurs

        public ArbreDecision(Sommet racine)
        {
            this.racine = racine;
        }
        public ArbreDecision(double[,] echantillon, double[] x1, double[] x2, double[] x3, double[] x4)
        {
            this.racine = new Sommet(echantillon, 0,null, null, null);
            SaisitsUtilisateurArbre(ref y, ref seuil1, ref seuil2, ref min, ref tailleMax);
            double mediane_x1 = MedianeCorrigee(x1);
            double mediane_x2 = MedianeCorrigee(x2);
            double mediane_x3 = MedianeCorrigee(x3);
            double mediane_x4 = MedianeCorrigee(x4);
            CreationArbre(racine, echantillon, mediane_x1, mediane_x2, mediane_x3, mediane_x4);
        }

        public Sommet Racine
        {
            get { return racine; }
            set { racine = value; }
        }


        public void InsertionIndivius(Sommet arbre,double[] valeur, double[] x1, double [] x2, double []x3, double[] x4)
        {
            double mediane_x1 = MedianeCorrigee(x1);
            double mediane_x2 = MedianeCorrigee(x2);
            double mediane_x3 = MedianeCorrigee(x3);
            double mediane_x4 = MedianeCorrigee(x4);
            int critere = 0;
            if (arbre.FilsGauche != null)
                critere = arbre.FilsGauche.Variable;
            else if (arbre.FilsDroit != null)
                critere = arbre.FilsDroit.Variable;

            if (critere == 1)
            {
                if (valeur[critere] <= mediane_x1)
                {
                    return; // gauche
                }
                else
                {
                    return; // droite
                }
            }
            else if (critere == 2)
            {
                if (valeur[critere] <= mediane_x2)
                {
                    return; // gauche
                }
                else
                {
                    return; // droite
                }
            }

            else if (critere == 3)
            {
                if (valeur[critere] <= mediane_x3)
                {
                    return; // gauche
                }
                else
                {
                    return; // droite
                }
            }

            else if (critere == 4)
            {
                if (valeur[critere] <= mediane_x4)
                {
                    return; // gauche
                }
                else
                {
                    return; // droite
                }
            }
            else
                return; //FIN
        }

        //Exo6
        private void CreationArbre(Sommet arbre, double[,] val, double mediane_x1, double mediane_x2, double mediane_x3, double mediane_x4)
        {
            if (arbre == null)
            {
                return;
                //AffichagePrefixe(arbre);
            }
            else
            {
                double pourcentage = PourcentageIndivius(y, val) * 100;
                if (tailleMax < HauteurArbre(racine) && val.GetLength(0) > min && (pourcentage > seuil1 && pourcentage < seuil2)) // test ok
                {
                    // choix du XI
                    int x1_nb1 = DivisionVariableXi(val, 1, mediane_x1, false);
                    int x1_nb2 = DivisionVariableXi(val, 1, mediane_x1, true);
                    int a = Math.Abs(x1_nb1 - x1_nb2);

                    int x2_nb1 = DivisionVariableXi(val, 2, mediane_x2, false);
                    int x2_nb2 = DivisionVariableXi(val, 2, mediane_x2, true);
                    int b = Math.Abs(x2_nb1 - x2_nb2);

                    int x3_nb1 = DivisionVariableXi(val, 3, mediane_x3, false);
                    int x3_nb2 = DivisionVariableXi(val, 3, mediane_x3, true);
                    int c = Math.Abs(x3_nb1 - x3_nb2);

                    int x4_nb1 = DivisionVariableXi(val, 4, mediane_x4, false);
                    int x4_nb2 = DivisionVariableXi(val, 4, mediane_x4, true);
                    int d = Math.Abs(x4_nb1 - x4_nb2);

                    //meilleur rapport gauche / droite
                    int max = MeilleureDivision(a, b, c, d);

                    int tailleG = 0;
                    int tailleD = 0;
                    double mediane = 0;

                    if (max == 1)
                    {
                        tailleG = x1_nb1;
                        tailleD = x1_nb2;
                        mediane = mediane_x1;
                    }
                    else if (max == 2)
                    {
                        tailleG = x2_nb1;
                        tailleD = x2_nb2;
                        mediane = mediane_x2;
                    }
                    else if (max == 3)
                    {
                        tailleG = x3_nb1;
                        tailleD = x3_nb2;
                        mediane = mediane_x3;
                    }
                    else if (max == 4)
                    {
                        tailleG = x4_nb1;
                        tailleD = x4_nb2;
                        mediane = mediane_x4;
                    }


                    //TODO : diviser la population
                    double[,] gauche = NouveauEchantillon(val, max, mediane, false, tailleG);
                    double[,] droite = NouveauEchantillon(val, max, mediane, true, tailleD);

                    arbre.FilsGauche = new Sommet(gauche, max,null, null, arbre);
                    arbre.FilsDroit = new Sommet(droite, max,null, null, arbre);

                    CreationArbre(arbre.FilsGauche, gauche, mediane_x1, mediane_x2, mediane_x3, mediane_x4);
                    CreationArbre(arbre.FilsDroit, droite, mediane_x1, mediane_x2, mediane_x3, mediane_x4);
                }
            }
        }

        public double[,] NouveauEchantillon(double[,] individus, int xi, double mediane_xi, bool gauche_droite, int taille)
        {
            double[,] tab = new double[taille, 5];
            int j = 0;
            for (int i = 0; i < individus.GetLength(0); i++)
            {
                if (!gauche_droite)
                {
                    if (individus[i, xi] <= mediane_xi)
                    {
                        for (int z = 0; z < 5; z++)
                        {
                            tab[j, z] = individus[i, z];
                        }
                        j++;
                    }
                }
                else
                {
                    if (individus[i, xi] > mediane_xi)
                    {
                        for (int z = 0; z < 5; z++)
                        {
                            tab[j, z] = individus[i, z];
                        }
                        j++;
                    }
                }
            }
            return tab;
        }

        public int MeilleureDivision(int a, int b, int c, int d)
        {
            int res = -1;
            int temp = Math.Max(a, b);
            int temp2 = Math.Max(c, d);
            int max = Math.Max(temp, temp2);
            if (max == a)
                res = 1;
            else if (max == b)
                res = 2;
            else if (max == c)
                res = 3;
            else
                res = 4;

            return res;
        }

        //Renvoie le nombre d'individu à gauche ou a droite selon une variable xi
        public int DivisionVariableXi(double[,] individus, int xi, double mediane_xi, bool gauche_droite) //faux = gauche , vrai = droite // xi : 1 2 3 ou 4
        {
            int res = 0;
            for (int i = 0; i < individus.GetLength(0); i++)
            {
                if (!gauche_droite)
                {
                    if (individus[i, xi] <= mediane_xi)
                        res++;
                }
                else
                {
                    if (individus[i, xi] > mediane_xi)
                        res++;
                }
            }
            return res;
        }

        public double PourcentageIndivius(int parY, double[,] individus)
        {
            double res = 0;
            int compteur = 0;
            int total = individus.GetLength(0);
            for (int i = 0; i < individus.GetLength(0); i++)
            {
                if (individus[i, 0] == parY)
                    compteur++;
            }
            res = compteur / total;
            return res;
        }
        /* public void InsertionSommet(Sommet arbre, int val)
         {
             _InsertionSommet(arbre, val);
         }*/

        static void SaisitsUtilisateurArbre(ref int Y, ref int seuil1, ref int seuil2, ref int min,
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

        static void AffichageTableau(double[] tableau)
        {
            for (int i = 0; i < tableau.Length; i++)
                Console.WriteLine(" Valeur : " + tableau[i]);
            Console.WriteLine("Fin");
        }

        static void TriABulleV2(double[] tableau)
        {
            int i, j;
            double inter;
            int cptr = 0;
            bool var = false;
            while (!var)
            {
                var = true;
                for (j = cptr; j < tableau.Length - 1 - cptr; j++)
                {
                    if (tableau[j] > tableau[j + 1])
                    {
                        var = false;
                        inter = tableau[j];
                        tableau[j] = tableau[j + 1];
                        tableau[j + 1] = inter;
                    }
                }
                cptr++;
                for (j = tableau.Length - 1 - cptr; j >= cptr; j--)
                {
                    if (tableau[j] < tableau[j - 1])
                    {
                        var = false;
                        inter = tableau[j];
                        tableau[j] = tableau[j - 1];
                        tableau[j - 1] = inter;
                    }
                }
                //cptr++;
            }
        }


        static double Mediane(double[] val)
        {
            double[] temp = val;
            TriABulleV2(temp);

            double res = 0;
            int n = temp.Length;
            if (n % 2 == 0)
            {
                res = (temp[n / 2] + temp[(n / 2) + 1]) / 2;
            }
            else
                res = temp[(n + 1) / 2];
            return res;
        }

        static double Maximum(double[] val)
        {
            double res = 0;
            for (int i = 0; i < val.Length; i++)
            {
                if (val[i] > res)
                    res = val[i];
            }
            return res;
        }

        static double SecondMaximum(double[] val)
        {
            double res = 0;
            double max1 = Maximum(val);
            for (int i = 0; i < val.Length; i++)
            {
                if (val[i] > res && val[i] < max1)
                    res = val[i];
            }
            return res;
        }

        static bool TestMediane(double[] val)
        {
            bool res = false;

            return res;
        }

        static double MedianeCorrigee(double[] val)
        {
            double res = -1;
            int n = val.Length;
            // TODO
            //faire test n<=2 et val identique !
            double mediane = Mediane(val);
            double max = Maximum(val);
            if (mediane == max)
                res = SecondMaximum(val);
            else
                res = max;
            return res;
        }

        public bool EstFeuille(Sommet element)
        {
            bool feuille = false;
            if (element != null && element.FilsDroit == null && element.FilsGauche == null)
            {

                feuille = true;

            }
            return feuille;
        }

        public int Max(int a, int b)
        {
            if (a < b)
                return b;
            else
                return a;
        }

        public int HauteurArbre(Sommet parent)
        {
            if (parent == null)
                return 0;
            if (EstFeuille(parent))
                return 1;
            return 1 + Max(HauteurArbre(parent.FilsDroit), HauteurArbre(parent.FilsGauche));

        }

        //Exo2--1
        public void AffichagePrefixe(Sommet element)
        {
            if (element != null)
            {
                Console.Write(element.Valeur + " ");
                AffichagePrefixe(element.FilsGauche);
                AffichagePrefixe(element.FilsDroit);
            }

        }

        //Exo3
        public void AffichageArborescence(Sommet arbre, int nbDecalage)
        {
            for (int i = 0; i < nbDecalage; i++)
            {
                Console.Write("  ");
            }
            if (arbre != null)
            {
                if (nbDecalage != 0)
                {
                    Console.WriteLine("|-" + arbre.Valeur);
                }
                else
                {
                    Console.WriteLine(arbre.Valeur);
                }
                if (!EstFeuille(arbre))
                {
                    nbDecalage++;
                    AffichageArborescence(arbre.FilsGauche, nbDecalage);
                    AffichageArborescence(arbre.FilsDroit, nbDecalage);
                }
            }
            else
            {
                Console.WriteLine("|-X");
            }

        }
        /*
        //Rechercher un element
        public Sommet RechercherValeur(Sommet arbre, int valeur)
        {
            if (arbre == null)
            {
                return null;
            }
            //compare to
            if (arbre.Valeur == valeur)
            {
                return arbre;
            }
            else
            {
                //compare to
                if (valeur < arbre.Valeur)
                {
                    return RechercherValeur(arbre.FilsGauche, valeur);
                }
                else // valeur > arbre->valeur
                {
                    return RechercherValeur(arbre.FilsDroit, valeur);
                }
            }
        }*/
        //Valeur minimale dans l'arbre
        public Sommet RechercherValeurMinimale(Sommet arbre)
        {
            if (arbre == null)
            {
                return null;
            }
            if (arbre.FilsGauche == null)
                return arbre;
            return RechercherValeurMinimale(arbre.FilsGauche);
        }
        //Valeur minimale dans l'arbre
        public Sommet RechercherValeurMaximale(Sommet arbre)
        {
            if (arbre == null)
            {
                return null;
            }
            if (arbre.FilsDroit == null)
                return arbre;
            return RechercherValeurMaximale(arbre.FilsDroit);
        }
    }
}
