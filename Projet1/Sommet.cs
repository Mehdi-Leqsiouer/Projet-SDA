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
        private int valeur;
        Sommet filsGauche;
        Sommet filsDroit;
        //Constructeur
        public Sommet(int valeur, Sommet filsGauche, Sommet filsDroit)
        {
            this.valeur = valeur;
            this.filsGauche = filsGauche;
            this.filsDroit = filsDroit;

        }
        //Propriétés 
        public int Valeur
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
    }
}
