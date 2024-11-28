using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
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
        string role = null;

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
            role = null;
        }

        public string IsSetRole()
        {
            return role;
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

        public List<Seances> getListeSeancesPourActivites(string nomActivite)
        {
            List<Seances> listeSeances = new List<Seances>();
            try
            {


                Boolean valide = true;


                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = $"Select * from seances where nom_activite = '{nomActivite}' ";
                con.Open();
                MySqlDataReader r = commande.ExecuteReader();
                while (r.Read())
                {
                    //for (global::System.Int32 i = 0; i < listeActivites.Count; i++)
                    //{
                    //    if (listeActivites[i].Nom == r["nom"].ToString())
                    //    {
                    //        valide = false;
                    //    }
                    //}

                    //if (valide)
                    //{
                    //    listeActivites.Add(new Activites(r["nom"].ToString(), Convert.ToInt32(r["id_categorie"]), r["type"].ToString(), Convert.ToDouble(r["cout_organisation"].ToString()), Convert.ToDouble(r["prix_vente"].ToString())));
                    //}
                    listeSeances.Add(new Seances(25, nomActivite, "15-02-2024", "12:00", 50, 3.5));

                }

                r.Close();
                con.Close();
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
                con.Close();
            }

            return listeSeances;
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

        public void getConnectionAdmin(int id, string mdp)
        {
            try
            {
                // Utilisation d'une requête paramétrée pour éviter les injections SQL
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "SELECT COUNT(*) FROM administrateur WHERE id = @id AND mdp = @mdp";
                commande.Parameters.AddWithValue("@id", id);
                commande.Parameters.AddWithValue("@mdp", mdp);  // Note : le mot de passe est envoyé en clair, ce qui est dangereux.

                // Ouverture de la connexion
                con.Open();

                // Exécution de la requête et récupération du résultat
                var resultat = commande.ExecuteScalar();

                // Vérification si l'utilisateur existe dans la base de données
                if (resultat != null && Convert.ToInt32(resultat) == 1)
                {
                    role = "admin";
                    connection = true;

                    con.Close() ;
                }
                else
                {
                    Debug.WriteLine("Identifiants invalides ou utilisateur inexistant.");
                }
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
            }
           con.Close();
        }


        public void getConnectionAdherent(string id, string mdp)
        {
            try
            {
                // Utilisation d'une requête paramétrée pour éviter les injections SQL
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "SELECT COUNT(*) FROM adherents WHERE no_identification = @no_identification AND prenom = @prenom";
                commande.Parameters.AddWithValue("@no_identification", id);
                commande.Parameters.AddWithValue("@prenom", mdp);  // Note : le mot de passe est envoyé en clair, ce qui est dangereux.

                // Ouverture de la connexion
                con.Open();

                // Exécution de la requête et récupération du résultat
                var resultat = commande.ExecuteScalar();

                // Vérification si l'utilisateur existe dans la base de données
                if (resultat != null && Convert.ToInt32(resultat) == 1)
                {
                    role = "adherent";
                    connection = true;

                    con.Close();
                }
                else
                {
                    Debug.WriteLine("Identifiants invalides ou utilisateur inexistant.");
                }
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
            }
            con.Close();
        }








    }
}
