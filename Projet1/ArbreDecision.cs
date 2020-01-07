using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet1
{
    class ArbreDecision
    {
        //Attributs
        private Sommet racine;
        //Constructeurs

        public ArbreDecision(Sommet racine)
        {
            this.racine = racine;
        }
        public ArbreDecision()
        {
            this.racine = null;
        }
        //Méthodes
        public Sommet Racine
        {
            get { return racine; }
            set { racine = value; }
        }
        //Exo1--1
        public Sommet CreerSommet(int val)
        {
            return new Sommet(val, null, null);
        }
        //Exo6
        public void _InsertionSommet(Sommet arbre, int val,int hauteurMax, int seuil1, int seuil2, int nombreMin)
        {
            if (arbre == null)
            {
                racine = new Sommet(val, null, null);
                //AffichagePrefixe(arbre);
            }
            else
            {
                if (hauteurMax > 0 && nombreMin > 0)
                    return;
            }
        }
       /* public void InsertionSommet(Sommet arbre, int val)
        {
            _InsertionSommet(arbre, val);
        }*/

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
        //Rechercher un element
        public Sommet RechercherValeur(Sommet arbre, int valeur)
        {
            if (arbre == null)
            {
                return null;
            }
            if (arbre.Valeur == valeur)
            {
                return arbre;
            }
            else
            {
                if (valeur < arbre.Valeur)
                {
                    return RechercherValeur(arbre.FilsGauche, valeur);
                }
                else // valeur > arbre->valeur
                {
                    return RechercherValeur(arbre.FilsDroit, valeur);
                }
            }
        }
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
