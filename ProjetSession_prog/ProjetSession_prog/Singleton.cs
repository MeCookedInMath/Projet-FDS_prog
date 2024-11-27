using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetSession_prog
{
    internal class Singleton
    {

        ObservableCollection<Activites> listeActivites;
        ObservableCollection<Adherents> listeAdherents;
        ObservableCollection<Seances> listeSeances;
        static Singleton instance = null;

        Boolean connection = false;

        MySqlConnection con =  new MySqlConnection("Server=cours.cegep3r.info;Database=a2024_420335ri_eq6;Uid=2258419;Pwd=2258419;");


        
        public Singleton()
        {
            listeActivites = new ObservableCollection<Activites>();
            listeAdherents = new ObservableCollection<Adherents>();
            listeSeances = new ObservableCollection<Seances>();

        }

        public static Singleton getInstance()
        {
            if (instance == null)
                instance = new Singleton();

            return instance;
        }


        public ObservableCollection<Activites> ListeActivites
        {
            get { return listeActivites; }
        }


        public ObservableCollection<Adherents> ListeAdherents
        {
            get { return listeAdherents; }
        }


        public ObservableCollection<Seances> Liste
        {
            get { return listeSeances; }
        }

        public Boolean IsSetConnection()
        {
            return connection;
        }

        public void SetConnectionTrue() { 
            connection = true;
        }

        public void SetConnectionFalse() { 
            connection = false;
        }



        public ObservableCollection<Activites> getListeActivites()
        {
            try
            {


                Boolean valide = true;


                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "Select * from activites";
                con.Open();
                MySqlDataReader r = commande.ExecuteReader();
                while (r.Read())
                {
                    for (global::System.Int32 i = 0; i < listeActivites.Count; i++)
                    {
                        if (listeActivites[i].Nom == r["nom"].ToString())
                        {
                            valide = false;
                        }
                    }

                    if (valide)
                    {
                        listeActivites.Add(new Activites(r["nom"].ToString(),Convert.ToInt32( r["id_categorie"]), r["type"].ToString(), Convert.ToDouble(r["cout_organisation"].ToString()), Convert.ToDouble(r["prix_vente"].ToString())  ));
                    }


                }

                r.Close();
                con.Close();
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
                con.Close();
            }

            return listeActivites;
        }



        public ObservableCollection<Adherents> getListeAdherents()
        {
            try
            {


                Boolean valide = true;


                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "Select * from adherents";
                con.Open();
                MySqlDataReader r = commande.ExecuteReader();
                while (r.Read())
                {
                    for (global::System.Int32 i = 0; i < listeAdherents.Count; i++)
                    {
                        if (listeAdherents[i].Nom == r["no_identification"].ToString())
                        {
                            valide = false;
                        }
                    }

                    if (valide)
                    {
                        listeAdherents.Add(new Adherents(r["no_identification"].ToString(), r["nom"].ToString(), r["prenom"].ToString(), r["adresse"].ToString(), r["date_naissance"].ToString(), Convert.ToInt32(r["age"])  ));
                    }


                }

                r.Close();
                con.Close();
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
                con.Close();
            }

            return listeAdherents;
        }



    }
}
