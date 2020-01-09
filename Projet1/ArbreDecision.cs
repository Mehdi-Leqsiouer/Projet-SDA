using System;
using System.Collections.Generic;
using System.Globalization;

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
        int tailledepart;
        //Constructeurs

        public ArbreDecision(List<double[]> echantillon, double[] x1, double[] x2, double[] x3, double[] x4)
        {
            this.racine = new Sommet(echantillon, 0, 0,null, null, null);
            SaisitsUtilisateurArbre(ref y, ref seuil1, ref seuil2, ref min, ref tailleMax);

            /*y = 2;
            seuil1 = 10;
            seuil2 = 90;
            min = 10;
            tailleMax = 8;*/

            this.tailledepart = echantillon.Count;
            double mediane_x1 = MedianeCorrigee(x1);
            double mediane_x2 = MedianeCorrigee(x2);
            double mediane_x3 = MedianeCorrigee(x3);
            double mediane_x4 = MedianeCorrigee(x4);
           // Console.WriteLine(mediane_x1);
            CreationArbre(racine, echantillon, mediane_x1, mediane_x2, mediane_x3, mediane_x4);
        }

        public Sommet Racine
        {
            get { return racine; }
            set { racine = value; }
        }


        public void RemplirTableau(double[] soustab, double[] x1, double[] x2, double[] x3, double[] x4,int i)
        {
            x1[i] = soustab[1];
            x2[i] = soustab[2];
            x3[i] = soustab[3];
            x4[i] = soustab[4];
        }

        //Exo6
        private void CreationArbre(Sommet arbre, List<double[]> val, double mediane_x1, double mediane_x2, double mediane_x3, double mediane_x4)
        {
            if (arbre == null)
            {
                return;
                //AffichagePrefixe(arbre);
            }


            else
            {
                double pourcentage = PourcentageIndividus(y, val) * 100;
                
                if (tailleMax > HauteurArbre(racine) && val.Count > ((min * tailledepart) / 100) && (pourcentage > seuil1 && pourcentage < seuil2)) // test ok
                {

                    double[] x1 = new double[val.Count];
                    double[] x2 = new double[val.Count];
                    double[] x3 = new double[val.Count];
                    double[] x4 = new double[val.Count];

                    int i = 0;
                    foreach(var el in val)
                    {
                        RemplirTableau(el, x1, x2, x3, x4,i);
                        i++;
                    }

                    mediane_x1 = MedianeCorrigee(x1);
                    mediane_x2 = MedianeCorrigee(x2);
                    mediane_x3 = MedianeCorrigee(x3);
                    mediane_x4 = MedianeCorrigee(x4);



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

                   // Console.WriteLine(mediane_x1 + " " + mediane_x2 + " " + mediane_x3 + " " + mediane_x4);

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

                   // Console.WriteLine(tailleG + " " + tailleD);
                    //TODO : diviser la population
                   // if (!PrecisionEstMax(val))
                   // {
                        List<double[]> gauche = NouveauEchantillon(val, max, mediane, false, tailleG);
                        List<double[]> droite = NouveauEchantillon(val, max, mediane, true, tailleD);

                        //if (!PrecisionEstMax(gauche) && !PrecisionEstMax(droite))
                        //    return;

                        arbre.FilsGauche = new Sommet(gauche, max, mediane,null, null, arbre);
                        arbre.FilsDroit = new Sommet(droite, max, mediane,null, null, arbre);

                       // AffichageListe(gauche);
                        //AffichageListe(droite);

                        CreationArbre(arbre.FilsGauche, gauche, mediane_x1, mediane_x2, mediane_x3, mediane_x4);
                        CreationArbre(arbre.FilsDroit, droite, mediane_x1, mediane_x2, mediane_x3, mediane_x4);
                   // }
                    
                }
            }
        }

        public bool PrecisionEstMax(List<double[]> liste)
        {
            bool res = true;
            double[] tmp = liste[0];
            double tmp2 = tmp[0];
            foreach(var el in liste)
            {
                if (el[0] != tmp2)
                    return false;
            }
            return res;
        }

        public void AffichageListe(List<double[]> liste)
        {
            Console.WriteLine("----- Début sommet ------");
            Console.WriteLine("Taille : " + liste.Count);
            foreach(var el in liste)
            {
                for (int i = 0; i < el.Length;i++)
                {
                    Console.Write(el[i] + " " );
                }
                Console.WriteLine();
            }
            Console.WriteLine("----- Fin sommet ------");
        }

        public List<double[]> NouveauEchantillon(List<double[]> individus, int xi, double mediane_xi, bool gauche_droite, int taille)
        {
            
            //AffichageListe(individus);
            List<double[]> res = new List<double[]>();
            if (individus == null)
                return null;
            foreach (var el in individus)
            {
                double[] tab = new double[5];
                if (!gauche_droite)
                {
                    if (el[xi] <= mediane_xi)
                    {
                        for (int z = 0; z < 5; z++)
                        {
                            if (el[z] != 0)
                            {
                                tab[z] = el[z];
                            }
                        }
                    }
                }
                else
                {
                    if (el[xi] > mediane_xi)
                    {
                        for (int z = 0; z < 5; z++)
                        {
                            if (el[z] != 0)
                            {
                                tab[z] = el[z];
                            }
                        }
                    }
                }
                //AffichageTableau(tab);
                if (tab[0] != 0)
                    res.Add(tab);
            }
            //AffichageListe(res);
            return res;
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
        public int DivisionVariableXi(List<double[]> individus, int xi, double mediane_xi, bool gauche_droite) //faux = gauche , vrai = droite // xi : 1 2 3 ou 4
        {
            int res = 0;
            foreach (var el in individus)
            {
                if (!gauche_droite)
                {
                    if (el[xi] <= mediane_xi && el[xi] != 0)
                        res++;
                }
                else
                {
                    if (el[xi] > mediane_xi && el[xi] != 0)
                        res++;
                }
            }
            return res;
        }

        public double PourcentageIndividus(int parY, List<double[]> individus)
        {
            double res = 0;
            int compteur = 0;
            int total = individus.Count;
            foreach (var el in individus)
            {
                if (el[0] == parY)
                    compteur++;
            }
            res = (float)compteur / total;
            return res;
        }
        /* public void InsertionSommet(Sommet arbre, int val)
         {
             _InsertionSommet(arbre, val);
         }*/

        public void InsertionIndivius(Sommet arbre, double[] valeur)
        {
            int critere = 0;
            double valcritere = 0;
            //Console.WriteLine(arbre.Variable + " " + arbre.VariableValeur);

             if (arbre.FilsGauche == null && arbre.FilsDroit == null)
             {
                 foreach(var el in arbre.Parent.Valeur)
                 {
                    Console.WriteLine(this.PourcentageIndividus(y, arbre.Valeur)*100+"%");
                    return;
                 }
                 Console.WriteLine("Fin arbre null");
             }

            if (arbre != null)
            {
                int gauche_droite = 0;
                if (arbre.FilsGauche != null)
                {
                    critere = arbre.FilsGauche.Variable;
                    valcritere = arbre.FilsGauche.VariableValeur;
                    if (valeur[critere] <= valcritere)
                        InsertionIndivius(arbre.FilsGauche, valeur);
                    else if (arbre.FilsDroit != null)
                        InsertionIndivius(arbre.FilsDroit, valeur);
                }
                else if (arbre.FilsDroit != null)
                {
                    critere = arbre.FilsDroit.Variable;
                    valcritere = arbre.FilsDroit.VariableValeur;
                    if (valeur[critere] > valcritere)
                        InsertionIndivius(arbre.FilsDroit, valeur);
                    else if (arbre.FilsGauche != null)
                        InsertionIndivius(arbre.FilsGauche, valeur);
                }
                if (valeur[critere] > valcritere)
                    gauche_droite = 1;

                switch (critere)
                {
                    case 1:
                        if (gauche_droite == 0)
                            Console.WriteLine("Critère x1 : " + valeur[1] + " " + "inférieur ou egal à : " + valcritere);
                        else
                            Console.WriteLine("Critère x1 : " + valeur[1] + " " + "Supérieur à : " + valcritere);
                        break;
                    case 2:
                        if (gauche_droite == 0)
                            Console.WriteLine("Critère x2 : " + valeur[2] + " " + "inférieur ou egal à : " + valcritere);
                        else
                            Console.WriteLine("Critère x2 : " + valeur[2] + " " + "Supérieur à : " + valcritere);
                        break;
                    case 3:
                        if (gauche_droite == 0)
                            Console.WriteLine("Critère x3 : " + valeur[3] + " " + "inférieur ou egal à : " + valcritere);
                        else
                            Console.WriteLine("Critère x3 : " + valeur[3] + " " + "Supérieur à : " + valcritere);
                        break;
                    case 4:
                        if (gauche_droite == 0)
                            Console.WriteLine("Critère x4 : " + valeur[4] + " " + "inférieur ou egal à : " + valcritere);
                        else
                            Console.WriteLine("Critère x4 : " + valeur[4] + " " + "Supérieur à : " + valcritere);
                        break;

                }

                // Console.WriteLine(critere + " " + valcritere);
            }

        }

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
                Console.Write("Saisissez le nombre minimal d'invidivu par échantillon en % ");
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
                Console.WriteLine("i : "+i+" Valeur : " + tableau[i]);
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
                res = (double)(temp[(n - 1) / 2] + temp[((n - 1) / 2) + 1]) / 2;
            }
            else
                res = temp[((n - 1) + 1) / 2];
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
            //AffichageTableau(val);
            // TODO
            //faire test n<=2 et val identique !
            double mediane = Mediane(val);
            double max = Maximum(val);
            //AffichageTableau(val);
            if (mediane == max)
                res = SecondMaximum(val);
            else
                res = mediane;
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

        public void AfficherFeuille(Sommet racine)
        {
            if (racine == null)
                return;
            else if (EstFeuille(racine))
            {
                Console.WriteLine("Précision feuille : " + this.PourcentageIndividus(y, racine.Valeur)*100+"%"); // précision + nb individus + chemin
                Console.WriteLine("Nombre d'individus : " + racine.Valeur.Count);
            }
            else
            {
                AfficherFeuille(racine.FilsGauche);
                AfficherFeuille(racine.FilsDroit);
            }
        }

        public int LargeurArbre(Sommet racine)
        {
            if (racine == null)
                return 0;
            else if (racine.FilsGauche == null && racine.FilsDroit == null)
                return 1;
            else
                return LargeurArbre(racine.FilsGauche) + LargeurArbre(racine.FilsDroit);
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

        public void AffichageArbre(Sommet arbre, int count)
        {
            int largeur = LargeurArbre(racine);
            for (int i = 0; i < largeur / 2; i++)
                Console.Write(" ");
            Console.WriteLine("Noeud : "+ count);
            count++;
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
                    Console.WriteLine("|-" + arbre.ToString());
                }
                else
                {
                    Console.WriteLine(arbre.ToString());
                }
                if (!EstFeuille(arbre))
                {
                    nbDecalage++;
                    Console.WriteLine("Fils gauche : ");
                    Console.WriteLine("Précision : "+PourcentageIndividus(this.y, arbre.FilsGauche.Valeur)*100+"%");
                    Console.WriteLine("Nombre d'individus : "+arbre.FilsGauche.Valeur.Count);
                    AffichageArborescence(arbre.FilsGauche, nbDecalage);
                    Console.WriteLine("Fils droit : ");
                    Console.WriteLine("Précision : " + PourcentageIndividus(this.y, arbre.FilsDroit.Valeur) * 100+"%");
                    Console.WriteLine("Nombre d'individus : " + arbre.FilsDroit.Valeur.Count);
                    AffichageArborescence(arbre.FilsDroit, nbDecalage);
                }
            }
            else
            {
                Console.WriteLine("|-X");
            }

        }
    }
}
