using System;
using System.Collections.Generic;

namespace Projet1
{
    class Sommet
    {
        //Attributs
        //Attributs
        private List<double[]> valeur;
        private int variableDivisee;
        private double variableDiviseeValeur;
        Sommet filsGauche;
        Sommet filsDroit;
        Sommet parent;
        //Constructeur
        public Sommet(List<double[]> valeur, int variable, double  variableValeur, Sommet filsGauche, Sommet filsDroit, Sommet parent)
        {
            this.valeur = valeur;
            this.variableDivisee = variable;
            this.variableDiviseeValeur = variableValeur;
            this.filsGauche = filsGauche;
            this.filsDroit = filsDroit;
            this.parent = parent;
        }

        public void AjoutValeur(double[] valeurs)
        {
            valeur.Add(valeurs);
        }

        public override string ToString()
        {
            string msg = " ";
            foreach (var el in valeur)
            {
                for (int i = 0; i < el.Length; i++)
                {
                    msg += " " + el[i];
                }
                msg += "\n";
            }

            return msg;
        }

        //Propriétés 
        public List<double[]> Valeur
        {
            get { return valeur; }
            set { valeur = value; }
        }

        public int Variable
        {
            get { return variableDivisee; }
            set { variableDivisee = value; }
        }

        public double VariableValeur
        {
            get { return variableDiviseeValeur; }
            set { variableDiviseeValeur = value; }
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
