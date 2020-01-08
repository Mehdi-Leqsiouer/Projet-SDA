using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet1
{
    class Sommet
    {
        //Attributs
        //Attributs
        private double [,]valeur;
        Sommet filsGauche;
        Sommet filsDroit;
        Sommet parent;
        //Constructeur
        public Sommet(double[,] valeur, Sommet filsGauche, Sommet filsDroit, Sommet parent)
        {
            this.valeur = valeur;
            this.filsGauche = filsGauche;
            this.filsDroit = filsDroit;
            this.parent = parent;
        }
        //Propriétés 
        public double[,] Valeur
        {
            get { return valeur; }
        }
        public Sommet FilsDroit
        {
            get { return filsDroit; }
            set { filsDroit = value; }
        }
        public Sommet FilsGauche
        {
            get { return filsGauche; }
            set { filsGauche = value; }
        }

        public Sommet Parent
        {
            get { return parent; }
            set { parent = value; }
        }
    }
}
