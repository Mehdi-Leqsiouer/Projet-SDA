using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet1
{
    class ArbreBinaire<T>
    {
        //Attributs
        private Noeud<T> racine;
        //Constructeurs
        public ArbreBinaire(Noeud<T> racine)
        {
            this.racine = racine;
        }
        public ArbreBinaire()
        {
            this.racine = null;
        }
        //Méthodes
        public Noeud<T> Racine
        {
            get { return racine; }
            set { racine = value; }
        }
        //Exo1--1
        public Noeud<T> CreerNoeud(T val)
        {
            return new Noeud<T>(val, null, null);
        }
        //Exo1--2
        public bool AssocierFilsGauche(Noeud<T> parent, Noeud<T> enfant)
        {
            bool res = false;
            if (parent != null && enfant != null)
            {
                if (parent.FilsGauche == null)
                {
                    parent.FilsGauche = enfant;
                    res = true;
                }
            }
            return res;
        }
        //Exo1--3
        public bool AssocierFilsDroit(Noeud<T> parent, Noeud<T> enfant)
        {
            bool res = false;
            if (parent != null && enfant != null)
            {
                if (parent.FilsDroit == null)
                {
                    parent.FilsDroit = enfant;
                    res = true;
                }
            }
            return res;
        }
        //Exo1--5
        public bool EstFeuille(Noeud<T> element)
        {
            bool feuille = false;
            if (element != null && element.FilsDroit == null && element.FilsGauche == null)
            {

                feuille = true;

            }
            return feuille;
        }
        //Exo2--1
        public void AffichagePrefixe(Noeud<T> element)
        {
            if (element != null)
            {
                Console.Write(element.Valeur + " ");
                AffichagePrefixe(element.FilsGauche);
                AffichagePrefixe(element.FilsDroit);
            }

        }
        //Exo2--2
        public void AffichageInfixe(Noeud<T> element)
        {
            if (element != null)
            {
                AffichageInfixe(element.FilsGauche);
                Console.Write(element.Valeur + " ");
                AffichageInfixe(element.FilsDroit);
            }

        }
        //Exo2--3
        public void AffichagePostfixe(Noeud<T> element)
        {
            if (element != null)
            {
                AffichagePostfixe(element.FilsGauche);
                AffichagePostfixe(element.FilsDroit);
                Console.Write(element.Valeur + " ");
            }

        }
        //Exo3
        public void affichage_arborescence(Noeud<T> arbre, int nbDecalage)
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
                    affichage_arborescence(arbre.FilsGauche, nbDecalage);
                    affichage_arborescence(arbre.FilsDroit, nbDecalage);
                }
            }
            else
            {
                Console.WriteLine("|-X");
            }

        }
        //Exo4--1
        public int NombreEnfantsNoeud(Noeud<T> parent)
        {
            int nb = 0;
            if (parent != null)
            {
                if (parent.FilsDroit != null)
                    nb++;
                if (parent.FilsGauche != null)
                    nb++;
            }
            return nb;
        }
        //Exo4--2
        public int NombreDescendantNoeud(Noeud<T> parent)
        {
            if (parent != null)
            {
                return NombreEnfantsNoeud(parent) + NombreDescendantNoeud(parent.FilsDroit) + NombreDescendantNoeud(parent.FilsGauche);
            }
            else
            {
                return 0;
            }
        }
        //Exo4--3
        public int NombreDeFeuilles(Noeud<T> parent)
        {
            if (parent == null)
                return 0;
            if (EstFeuille(parent))
                return 1;
            return NombreDeFeuilles(parent.FilsDroit) + NombreDeFeuilles(parent.FilsGauche);
        }
        //Exo5
        public int Max(int a, int b)
        {
            if (a < b)
                return b;
            else
                return a;
        }
        public int HauteurArbre(Noeud<T> parent)
        {
            if (parent == null)
                return 0;
            if (EstFeuille(parent))
                return 1;
            return 1 + Max(HauteurArbre(parent.FilsDroit), HauteurArbre(parent.FilsGauche));

        }
    }
}
