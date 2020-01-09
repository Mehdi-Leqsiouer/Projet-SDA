using System;

namespace Projet1
{
    class Sommet
    {
        //Attributs
        //Attributs
        private double[,] valeur;
        private int variableDivisee;
        Sommet filsGauche;
        Sommet filsDroit;
        Sommet parent;
        //Constructeur
        public Sommet(double[,] valeur, int variable,Sommet filsGauche, Sommet filsDroit, Sommet parent)
        {
            this.valeur = valeur;
            this.variableDivisee = variable;
            this.filsGauche = filsGauche;
            this.filsDroit = filsDroit;
            this.parent = parent;
        }

        public void AjoutValeur(double [] valeurs)
        {
           // int n = valeur.GetLength(0);
            //for(int i = 0; i )
        }

        //Propriétés 
        public double[,] Valeur
        {
            get { return valeur; }
            set { valeur = value; }
        }

        public int Variable
        {
            get { return variableDivisee; }
            set { variableDivisee = value; }
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
