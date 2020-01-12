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
        //Constructeur

        public ArbreDecision(List<double[]> echantillon)
        {
            this.racine = new Sommet(echantillon, 0, 0,null, null, null);
            SaisitsUtilisateurArbre(ref y, ref seuil1, ref seuil2, ref min, ref tailleMax);

            this.tailledepart = echantillon.Count;
            CreationArbre(racine, echantillon);
            Console.WriteLine("Arbre créer avec succès ! Voici les actions possible :");
        }

        public Sommet Racine
        {
            get { return racine; }
            set { racine = value; }
        }

        //cette fonction permet de remplir le tableau, elle a en parametre des tableau de double et renvoi rien
        public void RemplirTableau(double[] soustab, double[] x1, double[] x2, double[] x3, double[] x4,int i)
        {
            x1[i] = soustab[1];
            x2[i] = soustab[2];
            x3[i] = soustab[3];
            x4[i] = soustab[4];
        }

        //Exo6
        //cette fonction permet de créer l'arbre, elle a en parametre une variable de type sommet et une liste de double et renvoi rien
        private void CreationArbre(Sommet arbre, List<double[]> val)
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

                    double mediane_x1 = MedianeCorrigee(x1);
                    double mediane_x2 = MedianeCorrigee(x2);
                    double mediane_x3 = MedianeCorrigee(x3);
                    double mediane_x4 = MedianeCorrigee(x4);
                    
                    List<double[]> gauche1 = NouveauEchantillon(val, 1, mediane_x1, false);
                    List<double[]> droite1 = NouveauEchantillon(val, 1, mediane_x1, true);

                    List<double[]> gauche2 = NouveauEchantillon(val, 2, mediane_x2, false);
                    List<double[]> droite2 = NouveauEchantillon(val, 2, mediane_x2, true);

                    List<double[]> gauche3 = NouveauEchantillon(val, 3, mediane_x3, false);
                    List<double[]> droite3 = NouveauEchantillon(val, 3, mediane_x3, true);

                    List<double[]> gauche4 = NouveauEchantillon(val, 4, mediane_x4, false);
                    List<double[]> droite4 = NouveauEchantillon(val, 4, mediane_x4, true);

                    double prec1_1 = PourcentageIndividus(y, gauche1)*100;
                    double prec1_2 = PourcentageIndividus(y, droite1)*100;
                    int a = Math.Max((int)prec1_1, (int)prec1_2);

                    double prec2_1 = PourcentageIndividus(y, gauche2)*100;
                    double prec2_2 = PourcentageIndividus(y, droite2)*100;
                    int b = Math.Max((int)prec2_1, (int)prec2_2);

                    double prec3_1 = PourcentageIndividus(y, gauche3)*100;
                    double prec3_2 = PourcentageIndividus(y, droite3)*100;
                    int c = Math.Max((int)prec3_1, (int)prec3_2);

                    double prec4_1 = PourcentageIndividus(y, gauche4)*100;
                    double prec4_2 = PourcentageIndividus(y, droite4)*100;
                    int d = Math.Max((int)prec4_1, (int)prec4_2);

                    if (mediane_x1 == -1)
                        a = -1;
                    if (mediane_x2 == -1)
                        b = -1;
                    if (mediane_x3 == -1)
                        c = -1;
                    if (mediane_x4 == -1)
                        d = -1;

                    int max = MeilleureDivision(a, b, c, d);

                    double mediane = 0;

                    if (max == 1)
                        mediane = mediane_x1;
                    else if (max == 2)
                        mediane = mediane_x2;
                    else if (max == 3)
                        mediane = mediane_x3;
                    else if (max == 4)
                        mediane = mediane_x4;

                    List<double[]> gauche = NouveauEchantillon(val, max, mediane, false);
                    List<double[]> droite = NouveauEchantillon(val, max, mediane, true);

                    if (mediane_x1 == -1 && mediane_x2 == -1 && mediane_x3 == -1 && mediane_x4 == -1)
                        return;

                    arbre.FilsGauche = new Sommet(gauche, max, mediane, null, null, arbre);
                    arbre.FilsDroit = new Sommet(droite, max, mediane, null, null, arbre);

                    // AffichageListe(gauche);
                    //AffichageListe(droite);

                    CreationArbre(arbre.FilsGauche, gauche);
                    CreationArbre(arbre.FilsDroit, droite);
                    
                }
            }
        }
        //cette fonction permet d'afficher la liste, elle a en parametre une liste de double et renvoi rien
        public void AffichageListe(List<double[]> liste)
        {
            Console.WriteLine("----- Début sommet ------");
            Console.WriteLine("Taille : " + liste.Count);
            foreach(var el in liste)
            {
                for (int i = 0; i < el.Length;i++)
                {
                    Console.Write((double)el[i] + " " );
                }
                Console.WriteLine();
            }
            Console.WriteLine("----- Fin sommet ------");
        }
        //cette fonction permet de recuperer la nouvelle liste, elle a en parametre une liste de double, un entier, double et un bool et renvoi une liste de double
        public List<double[]> NouveauEchantillon(List<double[]> individus, int xi, double mediane_xi, bool gauche_droite)
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
        //cette fonction permet de comparer les differentes variables en parametre, elle a en parametre 4 entiers et renvoi un entier
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
        //cette fonction permet d'obtenir le pourcentage d'individus, elle a en parametre un entier et une liste de double et renvoi un double
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
        //cette fonction permet de determiner la précision de l'individu selon la variable Y, elle a en parametre une variable de type sommet et un tableau de double  et renvoi rien
        public void InsertionIndivius(Sommet arbre, double[] valeur)
        {
            int critere = 0;
            double valcritere = 0;
            //Console.WriteLine(arbre.Variable + " " + arbre.VariableValeur);

             if (arbre.FilsGauche == null && arbre.FilsDroit == null)
             {
                 foreach(var el in arbre.Parent.Valeur)
                 {
                    Sommet temp = new Sommet(arbre.Valeur, arbre.Variable, arbre.VariableValeur, arbre.FilsGauche, arbre.FilsDroit, arbre.Parent);
                    Console.WriteLine("Chemin à l'envers : ");
                    CheminALenvers(temp);                    
                    Console.WriteLine("Précision pour que cette individu soit de type Y = "+y+" : "+Math.Round(this.PourcentageIndividus(y, arbre.Valeur),4)*100+"%");
                    return;
                }
             }

            if (arbre != null)
            {
               // int gauche_droite = 0;
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
            }

        }

        //cette fonction demande la saisie à l'utilisateur, elle a en parametre un tableau de double et renvoi rien
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
        //cette fonction permet d'afficher le tableau, elle a en parametre un tableau de double et renvoi rien
        static void AffichageTableau(double[] tableau)
        {
            for (int i = 0; i < tableau.Length; i++)
                Console.WriteLine("i : "+(i+1)+" Valeur : " + tableau[i]);
            Console.WriteLine("Fin");
        }
        //cette fonction permet de trier le tableau, elle a en parametre un tableau de double et renvoi rien
        static void TriABulleV2(double[] tableau)
        {
            int  j;
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

        //cette fonction permet de calculer la mediane, elle a en parametre un tableau de double et renvoi un double
        static double Mediane(double[] val)
        {
            double[] temp = val;
            TriABulleV2(temp);
           // AffichageTableau(temp);
            double res = 0;
            int n = temp.Length;
            if (n % 2 == 0)
            {
                res = (double)(temp[(n-1) / 2] + temp[((n-1) / 2) + 1]) / 2;
            }
            else
                res = temp[((n - 1) + 1) / 2];
            //Console.WriteLine(res);
            return res;
        }
        //cette fonction permet d'obtenir la valeur max, elle a en parametre un tableau de double et renvoi un double
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
        //cette fonction permet d'obtenir la 2e valeur max, elle a en parametre un tableau de double et renvoi un double
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
        //cette fonction permet de savoir si deux valeurs sont identiques, elle a en parametre un tableau de double et renvoi un bool
        static bool ValIdentique(double[] val)
        {
            bool res = true;
            double tmp = val[0];
            for (int i = 1; i < val.Length; i++)
            {
                if (val[i] != tmp)
                {
                    res = false;
                }
            }
            return res;
        }
        //cette fonction permet de tester la mediane, elle a en parametre un tableau de double et renvoi un bool
        static bool TestMediane(double[] val)
        {
            bool res = true;
            int n = val.Length;
            if (n < 2 || ValIdentique(val))
                res = false;
            return res;
        }
        //cette fonction permet de modifier la mediane, elle a en parametre un tableau de double et renvoi un double
        static double MedianeCorrigee(double[] val)
        {
            double res = -1;
            int n = val.Length;

            if (!TestMediane(val))
                return -1;

            double mediane = Mediane(val);
            double max = Maximum(val);
            if (mediane == max)
                res = SecondMaximum(val);
            else
                res = mediane;
            return res;
        }
        //cette fonction permet de tester si le sommet est une feuille, elle a en parametre une variable de type sommet et renvoi un bool
        public bool EstFeuille(Sommet element)
        {
            bool feuille = false;
            if (element != null && element.FilsDroit == null && element.FilsGauche == null)
            {

                feuille = true;

            }
            return feuille;
        }
        //cette fonction permet d'afficher le chemin pr aller de la racine au sommet mais dans l'autre sens ,elle a en parametre une variable de type sommet et renvoi rien
        public void CheminALenvers(Sommet sommet)
        {
            if (sommet.Parent == null)
                return;
            else
            {
                int critere = sommet.Variable;
                double valcritere = sommet.VariableValeur;
                string gauche_droite = "Inférieur ou égal";
                double val = 0;
                foreach (var el in sommet.Valeur)
                    val = el[critere];
                if (val > valcritere)
                    gauche_droite = "Supérieur strict";

                Console.WriteLine("Variable x" + critere + " " + gauche_droite + " a " + valcritere);
                CheminALenvers(sommet.Parent);
            }
        }
        //cette fonction permet d'afficher la feuille, elle a en parametre une variable de type sommet et un entier et renvoi rien
        public void AfficherFeuille(Sommet racine, int i)
        {
            if (racine == null)
                return;
            else if (EstFeuille(racine))
            {
                i++;
                Console.WriteLine("Feuille n°" + i);
                Console.WriteLine("Précision feuille : " + Math.Round(this.PourcentageIndividus(y, racine.Valeur),4)*100+"%"); // précision + nb individus + chemin
                Console.WriteLine("Nombre d'individus : " + racine.Valeur.Count);
                Console.WriteLine("Chemin à l'envers : ");
                CheminALenvers(racine);
                Console.WriteLine();
            }
            else
            {
                AfficherFeuille(racine.FilsGauche,i);
                AfficherFeuille(racine.FilsDroit,i);
            }
        }
        //cette fonction permet d'obtenir la largeur de l'arbre, elle a en parametre une variable de type sommet et renvoi un entier
        public int LargeurArbre(Sommet racine)
        {
            if (racine == null)
                return 0;
            else if (racine.FilsGauche == null && racine.FilsDroit == null)
                return 1;
            else
                return LargeurArbre(racine.FilsGauche) + LargeurArbre(racine.FilsDroit);
        }
        //cette fonction permet d'obtenir la valeur max entre deux variables, elle a en parametre deux entiers et renvoi un entier
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
        //cette fonction permet d'afficher le nombre de noeud de l'arbre, elle a en parametre une variable de type sommet et un entier et renvoi rien
        public void AffichageArbre(Sommet arbre, int count)
        {
            int largeur = LargeurArbre(racine);
            for (int i = 0; i < largeur / 2; i++)
                Console.Write(" ");
            Console.WriteLine("Noeud : "+ count);
            count++;
        }

        //Exo3
        //cette fonction permet d'afficher toute l'aroborescence, elle a en parametre une variable de type sommet et renvoi rien
        public void AffichageArborescence(Sommet arbre)
        {
            if (arbre != null)
            {
                Console.WriteLine();
                Console.WriteLine("Noeud ");
                Console.WriteLine("Précision : " + Math.Round(PourcentageIndividus(this.y, arbre.Valeur),4) * 100 + "%");
                Console.WriteLine("Nombre d'individus : " + arbre.Valeur.Count);
                int critere = arbre.Variable;
                double critereval = arbre.VariableValeur;

                
                string gauche_droite = "Inférieur ou égal";
                double val = 0;
                foreach (var el in arbre.Valeur)
                    val = el[critere];
                if (val > critereval)
                    gauche_droite = "Supérieur strict";

                Console.WriteLine("Variable x" + critere + " " + gauche_droite + " a " + critereval);

                AffichageArborescence(arbre.FilsGauche);
                AffichageArborescence(arbre.FilsDroit);
            }
            /*else
            {
                Console.WriteLine("|-X");
            }*/

        }

        public int Y
        {
            get { return this.y; }
        }

    }
}
