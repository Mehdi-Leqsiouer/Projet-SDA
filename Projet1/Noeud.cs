using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet1
{
    class Noeud<T>
    {
        //Attributs
        private T valeur;
        Noeud<T> filsGauche;
        Noeud<T> filsDroit;
        //Constructeur
        public Noeud(T valeur, Noeud<T> filsGauche, Noeud<T> filsDroit)
        {
            this.valeur = valeur;
            this.filsGauche = filsGauche;
            this.filsDroit = filsDroit;
        }
        //Propriétés 
        public T Valeur
        {
            get { return valeur; }
        }
        public Noeud<T> FilsDroit
        {
            get { return filsDroit; }
            set { filsDroit = value; }
        }
        public Noeud<T> FilsGauche
        {
            get { return filsGauche; }
            set { filsGauche = value; }
        }
    }
}
