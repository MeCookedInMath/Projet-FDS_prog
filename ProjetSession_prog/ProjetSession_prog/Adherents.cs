using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetSession_prog
{
    internal class Adherents
    {
        string no_identification;
        string nom;
        string prenom;
        string adresse;
        string date_naissance;
        int age;


        public Adherents(string _no_identification, string _nom, string _prenom, string _adresse, string _date_naissance, int _age)
        {
            this.no_identification = _no_identification;
            this.nom = _nom;
            this.prenom = _prenom;
            this.adresse = _adresse;
            this.date_naissance = _date_naissance;
            this.age = _age;
        }


        public string No_Identification
        {
            get { return no_identification; }
            set { no_identification = value; }
        }

        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }

        public string Prenom
        {
            get { return prenom; }
            set { prenom = value; }
        }

        public string Adresse
        {
            get { return adresse; }
            set { adresse = value; }
        }


        public string Date_Naissance
        {
            get { return date_naissance; }
            set { date_naissance = value; }
        }


        public int Age
        {
            get { return age; }
            set { age = value; }
        }


        public override string ToString()
        {
            return $"Numéro d'identification : {No_Identification}, Nom : {Nom}, Prenom : {Prenom}, Adresse : {Adresse}, Date de naissance : {Date_Naissance}, Âge : {Age}";
        }




    }
}
