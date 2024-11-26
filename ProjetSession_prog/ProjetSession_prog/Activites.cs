using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetSession_prog
{
    internal class Activites
    {
        string nom;
        int id_categorie;
        string type;
        double cout_organisation;
        double prix_vente;


        public Adherents(string _nom, int _id_categorie, string _type, double _cout_organisation, double _prix_vente)
        {
            this.nom = _nom;
            this.id_categorie = _id_categorie;
            this.type = _type;
            this.cout_organisation = _cout_organisation;
            this.prix_vente = _prix_vente;
        }

        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }

        public int Id_Categorie
        {
            get { return id_categorie; }
            set { id_categorie = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public double Cout_organisation
        {
            get { return cout_organisation; }
            set { cout_organisation = value; }
        }

        public double Prix_vente
        {
            get { return prix_vente; }
            set { prix_vente = value; }
        }

        public override string ToString()
        {
            return $"Nom de l'activité : {Nom}, catégorie : {Id_Categorie}, type : {Type}, coût d'organisation : {Cout_organisation}, prix de vente : {Prix_vente}";
        }
    }
}
