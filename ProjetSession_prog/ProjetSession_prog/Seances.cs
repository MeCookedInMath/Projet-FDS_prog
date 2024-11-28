using Microsoft.UI.Xaml.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Devices;

namespace ProjetSession_prog
{
    internal class Seances
    {
        int id;
        string nom_activite;
        string date;
        string heure;
        int nbr_places;
        double note;


        public Seances(int _id, string _nomActivite, string _date, string _heure, int _nbr_places, double _note) {

            this.id = _id;
            this.nom_activite = _nomActivite;
            this.date = _date;
            this.heure = _heure;
            this.nbr_places = _nbr_places;
            this.note = _note;
        }


        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Nom_Activite
        {
            get { return nom_activite; }
            set { nom_activite = value; }
        }

        public string Date
        {
            get { return date; }
            set { date = value; }
        }

        public string Heure
        {
            get { return heure; }
            set { heure = value; }
        }


        public int Nbr_Places
        {
            get { return nbr_places; }
            set { nbr_places = value; }
        }

        public double Note
        {
            get { return note; }
            set { note = value; }
        }

        public override string ToString()
        {
            return $"activité : {Nom_Activite}, date : {Date}, heure : {Heure}, note : {Note}";
        }



    }
}
