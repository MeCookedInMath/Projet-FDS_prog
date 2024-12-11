using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Pickers;
using Windows.Storage;
using WinRT.Interop;

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
        string matricule_adherent = null;
        Adherents adherentConnecte;

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

        public void setMatricule_connection(string no_identification)
        {
            matricule_adherent = no_identification;
        }

        public string matricule_connection()
        {
            return matricule_adherent;
        }


        public Adherents getAdherentConnecte()
        {
            
            try
            {

                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = $"Select * from adherents where no_identification = '{Singleton.getInstance().matricule_connection()}'";
                con.Open();
                MySqlDataReader r = commande.ExecuteReader();
                while (r.Read())
                {
                     adherentConnecte = new Adherents(   r["no_identification"].ToString(), r["nom"].ToString(), r["prenom"].ToString(),
                        r["adresse"].ToString(), r["date_naissance"].ToString(), Convert.ToInt32(r["age"].ToString())   );
                }

                r.Close();
                con.Close();
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
                con.Close();
            }

            return adherentConnecte;
        }

        

        public async void setMessageUtilisateur(string message, Microsoft.UI.Xaml.UIElement uiElement)
        {
            
            ContentDialog dialog = new ContentDialog
            {
                Title = "Mon titre",
                CloseButtonText = "OK",
                Content = message
            };

           
            dialog.XamlRoot = uiElement.XamlRoot;

          
            var result = await dialog.ShowAsync();

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
                    double note = r["note"] != DBNull.Value ? Convert.ToDouble(r["note"]) : 0;

                    listeSeances.Add(new Seances(Convert.ToInt32(r["id"]), nomActivite, r["date"].ToString(), r["heure"].ToString(), Convert.ToInt32(r["nbr_places"]), note ));

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



        public List<Seances> getListeSeancesPourAdhérents(string noIdentification)
        {
            List<Seances> listeSeancesParAdherent = new List<Seances>();
            try
            {
                

                Boolean valide = true;


                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = $"Select id, nom_activite, date, heure, nbr_places, note from seances inner join inscriptions i on seances.id = i.id_seance where i.id_adherent = '{noIdentification}'";
                con.Open();
                MySqlDataReader r = commande.ExecuteReader();
                while (r.Read())
                {
                    double note = r["note"] != DBNull.Value ? Convert.ToDouble(r["note"]) : 0;

                    listeSeancesParAdherent.Add(new Seances(Convert.ToInt32(r["id"]), r["nom_activite"].ToString(), r["date"].ToString(), r["heure"].ToString(), Convert.ToInt32(r["nbr_places"]), note));

                }

                r.Close();
                con.Close();
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
                con.Close();
            }

            return listeSeancesParAdherent;
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
                    matricule_adherent = id;

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


        public void creer_Inscriptions(int id_seance, string matricule)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand("insertion_inscriptions");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.AddWithValue("idDeAdherent", matricule);
                commande.Parameters.AddWithValue("idDeSeances", id_seance);

                con.Open();
                commande.Prepare();
                int i = commande.ExecuteNonQuery();

                
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
            }
            con.Close();
            

        }

        public void creer_Activites(string nom_activite, int id_categorie, string type, double cout_organisation, double prix_vente)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand("insertion_activites");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.AddWithValue("activites_nom", nom_activite);
                commande.Parameters.AddWithValue("activites_categorieId", id_categorie);
                commande.Parameters.AddWithValue("activites_type", type);
                commande.Parameters.AddWithValue("activites_coutOrganisation", cout_organisation);
                commande.Parameters.AddWithValue("activites_prixVente", prix_vente);

                con.Open();
                commande.Prepare();
                int i = commande.ExecuteNonQuery();


            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
            }
            con.Close();


        }


        public void creer_Adherents(string nom, string prenom, string adresse, string date_naissance)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand("insertion_adherents");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.AddWithValue("adherents_nom", nom);
                commande.Parameters.AddWithValue("adherents_prenom", prenom);
                commande.Parameters.AddWithValue("adherents_adresse", adresse);
                commande.Parameters.AddWithValue("adherents_dateNaissance", date_naissance);
              

                con.Open();
                commande.Prepare();
                int i = commande.ExecuteNonQuery();


            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
            }
            con.Close();


        }


        public void creer_Seances(string nom_activite, string date, string heure, int nbr_places)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand("insertion_seances");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.AddWithValue("seance_nomActivite", nom_activite);
                commande.Parameters.AddWithValue("seance_date", date);
                commande.Parameters.AddWithValue("seance_heure", heure);
                commande.Parameters.AddWithValue("seance_nbrPlace", nbr_places);


                con.Open();
                commande.Prepare();
                int i = commande.ExecuteNonQuery();


            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
            }
            con.Close();


        }

        public void supprimerActivites(string nomActivite)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = $"delete from activites where nom = '{nomActivite}'";

                con.Open();
                commande.Prepare();
                commande.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                con.Close();
            }
        }


        public void supprimerAdherents(string noIdentification)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = $"delete from adherents where no_identification = '{noIdentification}'";

                con.Open();
                commande.Prepare();
                commande.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                con.Close();
            }
        }

        public void supprimerSeances(int id)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = $"delete from seances where id = '{id}'";

                con.Open();
                commande.Prepare();
                commande.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                con.Close();
            }
        }


        public void supprimerInscriptions(string id_adherent, int id_seance)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = $"delete from inscriptions where id_seance = {id_seance} and id_adherent = '{id_adherent}'";

                con.Open();
                commande.Prepare();
                commande.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                con.Close();
            }
        }


        public void modifierActivites(string nom_Activite,int id_categorie, string type, double cout_organisation, double prix_vente)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "UPDATE activites SET id_categorie = @id_categorie, type = @type, cout_organisation = @cout_organisation, prix_vente = @prix_vente WHERE nom = @nom_Activite";

                // Add parameters to the command
                commande.Parameters.AddWithValue("@id_categorie", id_categorie);
                commande.Parameters.AddWithValue("@type", type);
                commande.Parameters.AddWithValue("@cout_organisation", cout_organisation);
                commande.Parameters.AddWithValue("@prix_vente", prix_vente);
                commande.Parameters.AddWithValue("@nom_Activite", nom_Activite);
                con.Open();
                commande.Prepare();
                commande.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                con.Close();
            }
        }


        public void modifierAdherents(string noIdentification, string nom, string prenom, string adresse, string date_naissance)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = $"UPDATE adherents SET nom = '{nom}', prenom = '{prenom}', adresse = '{adresse}', date_naissance = '{date_naissance}' where no_identification = '{noIdentification}'";

                con.Open();
                commande.Prepare();
                commande.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                con.Close();
            }
        }



        public void modifierSeances(int id, string nomActivite, string date, string heure, int nbrPlaces)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "update seances SET nom_activite = @nomActivite, date = @date, heure = @heure, nbr_places = @nbr_places where id = @id";

                commande.Parameters.AddWithValue("@nomActivite", nomActivite);
                commande.Parameters.AddWithValue("@date", date);
                commande.Parameters.AddWithValue("@heure", heure);
                commande.Parameters.AddWithValue("@nbr_places", nbrPlaces);
                commande.Parameters.AddWithValue("@id", id);

                con.Open();
                commande.Prepare();
                commande.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                con.Close();
            }
        }

        public void evaluerSeances(string noIdentification, int idSeance, int note)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand("insertion_evaluations");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.AddWithValue("idDeAdherent", noIdentification);
                commande.Parameters.AddWithValue("idDeSeances", idSeance);
                commande.Parameters.AddWithValue("noteDeSeances", note);


                con.Open();
                commande.Prepare();
                int i = commande.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                con.Close();
            }
            con.Close();
        }


        public int GetNombreActivites()
        {
            int nombreActivites = 0;
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "SELECT COUNT(*) FROM activites";
                con.Open();
                nombreActivites = Convert.ToInt32(commande.ExecuteScalar());
                con.Close();
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
                con.Close();
            }
            return nombreActivites;
        }



        public int GetNombreAdherents()
        {
            int nombreAdherents = 0;
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "SELECT COUNT(*) FROM adherents";
                con.Open();
                nombreAdherents = Convert.ToInt32(commande.ExecuteScalar());
                con.Close();
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
                con.Close();
            }
            return nombreAdherents;
        }

        public List<string> GetTopSeances(int limit = 5)
        {
            List<string> topSeances = new List<string>();
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = $"SELECT nom_activite, COUNT(id_adherent) AS nb_inscriptions FROM inscriptions INNER JOIN seances ON inscriptions.id_seance = seances.id GROUP BY nom_activite ORDER BY nb_inscriptions DESC LIMIT {limit}";
                con.Open();
                MySqlDataReader r = commande.ExecuteReader();
                while (r.Read())
                {
                    string seanceInfo = $"{r["nom_activite"]} - {r["nb_inscriptions"]} inscriptions";
                    topSeances.Add(seanceInfo);
                }
                r.Close();
                con.Close();
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
                con.Close();
            }
            return topSeances;
        }
        public List<(string ActiviteNom, double Moyenne)> GetMoyenneNote()
        {
            List<(string ActiviteNom, double Moyenne)> moyennes = new List<(string ActiviteNom, double Moyenne)>();

            try
            {
                string select = "SELECT activite_nom, moyenne FROM moyenne_Note;";
                MySqlCommand commande = new MySqlCommand(select, con);
                con.Open();

                MySqlDataReader r = commande.ExecuteReader();
                while (r.Read())
                {
                    string activiteNom = r["activite_nom"].ToString();
                    double moyenne = r["moyenne"] != DBNull.Value ? Convert.ToDouble(r["moyenne"]) : 0.0;

                    moyennes.Add((activiteNom, moyenne));
                }

                r.Close();
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine($"Erreur lors de la récupération des moyennes : {ex.Message}");
            }
            finally
            {
                con.Close();
            }

            return moyennes;
        }

        public List<(string ActiviteNom, int NombreParticipants)> GetNombreParticipantsParActivite()
        {
            List<(string ActiviteNom, int NombreParticipants)> participantsParActivite = new List<(string ActiviteNom, int NombreParticipants)>();

            try
            {
                string select = "SELECT nom, nbr_participants FROM nbr_participants_activite;";
                MySqlCommand commande = new MySqlCommand(select, con);
                con.Open();

                MySqlDataReader r = commande.ExecuteReader();
                while (r.Read())
                {
                    string activiteNom = r["nom"].ToString();
                    int nbrParticipants = r["nbr_participants"] != DBNull.Value ? Convert.ToInt32(r["nbr_participants"]) : 0;

                    participantsParActivite.Add((activiteNom, nbrParticipants));
                }

                r.Close();
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine($"Erreur lors de la récupération des participants par activité : {ex.Message}");
            }
            finally
            {
                con.Close();
            }

            return participantsParActivite;
        }

        public List<(string AdherentId, string ActiviteNom, double PrixMoyen)> GetPrixMoyenParActiviteParParticipant()
        {
            List<(string AdherentId, string ActiviteNom, double PrixMoyen)> prixMoyenListe = new List<(string AdherentId, string ActiviteNom, double PrixMoyen)>();

            try
            {
                string select = "SELECT id_adherent, activite_nom, prix_moyen FROM moy_prix_activite;";
                MySqlCommand commande = new MySqlCommand(select, con);
                con.Open();

                MySqlDataReader r = commande.ExecuteReader();
                while (r.Read())
                {
                    string adherentId = r["id_adherent"].ToString();
                    string activiteNom = r["activite_nom"].ToString();
                    double prixMoyen = r["prix_moyen"] != DBNull.Value ? Convert.ToDouble(r["prix_moyen"]) : 0;

                    prixMoyenListe.Add((adherentId, activiteNom, prixMoyen));
                }

                r.Close();
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine($"Erreur lors de la récupération du prix moyen par activité : {ex.Message}");
            }
            finally
            {
                con.Close();
            }

            return prixMoyenListe;
        }

        public (string NoIdentification, string Nom, string Prenom, int NombreSeances) GetParticipantAvecPlusDeSeances()
        {
            (string NoIdentification, string Nom, string Prenom, int NombreSeances) participant = (null, null, null, 0);

            try
            {
                string select = "SELECT no_identification, nom, prenom, nombre_seances FROM participant_plusseance;";
                MySqlCommand commande = new MySqlCommand(select, con);
                con.Open();

                MySqlDataReader r = commande.ExecuteReader();
                if (r.Read())
                {
                    participant.NoIdentification = r["no_identification"].ToString();
                    participant.Nom = r["nom"].ToString();
                    participant.Prenom = r["prenom"].ToString();
                    participant.NombreSeances = Convert.ToInt32(r["nombre_seances"]);
                }

                r.Close();
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine($"Erreur lors de la récupération du participant avec le plus de séances : {ex.Message}");
            }
            finally
            {
                con.Close();
            }

            return participant;
        }
        public async Task ExportActivitiesToCSV(List<Activites> activities)
        {

            StringBuilder csvData = new StringBuilder();
            csvData.AppendLine("Nom Activité,Catégorie,Type,Coût d'Organisation, Prix de Vente");

            foreach (Activites activity in activities)
            {
                csvData.AppendLine($"{activity.Nom},{activity.Id_Categorie},{activity.Type},{activity.Cout_organisation},{activity.Prix_vente}");
            }

            FileSavePicker savePicker = new FileSavePicker
            {
                SuggestedStartLocation = PickerLocationId.Desktop
            };
            savePicker.FileTypeChoices.Add("CSV", new List<string>() { ".csv" });
            savePicker.SuggestedFileName = "activites_export";

            StorageFile file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {

                await FileIO.WriteTextAsync(file, csvData.ToString());
                var dialog = new ContentDialog()
                {
                    Title = "Exportation réussie",
                    Content = "Les activités ont été exportées avec succès.",
                    CloseButtonText = "OK"
                };
                await dialog.ShowAsync();
            }
            else
            {

                var dialog = new ContentDialog()
                {
                    Title = "Erreur",
                    Content = "Aucun fichier n'a été sélectionné.",
                    CloseButtonText = "OK"
                };
                await dialog.ShowAsync();
            }
        }



        public List<Activites> GetActivities()
        {
            List<Activites> activitiesList = new List<Activites>();


            string select = "SELECT * FROM activites";
            MySqlCommand cmd = new MySqlCommand(select, con);

            try
            {
                con.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string name = reader["nom"].ToString();
                    int categoryId = Convert.ToInt32(reader["id_categorie"]);
                    string type = reader["type"].ToString();
                    double cost = Convert.ToDouble(reader["cout_organisation"]);
                    double price = Convert.ToDouble(reader["prix_vente"]);

                    activitiesList.Add(new Activites(name, categoryId, type, cost, price));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erreur lors de la récupération des activités: {ex.Message}");
            }
            finally
            {
                con.Close();
            }

            return activitiesList;
        }

        public async void Btn_Exporter_Adherents_Click(object sender, RoutedEventArgs e)
        {

            var adherentsList = Singleton.getInstance().GetAdherents();


            StringBuilder csvData = new StringBuilder();
            csvData.AppendLine("Numéro d'identification,Nom,Prénom,Adresse,Date de naissance,Âge");


            foreach (Adherents adherent in adherentsList)
            {
                csvData.AppendLine($"{adherent.No_Identification},{adherent.Nom},{adherent.Prenom},{adherent.Adresse},{adherent.Date_Naissance},{adherent.Age}");
            }


            FileSavePicker savePicker = new FileSavePicker
            {
                SuggestedStartLocation = PickerLocationId.Desktop
            };
            savePicker.FileTypeChoices.Add("CSV", new List<string> { ".csv" });
            savePicker.SuggestedFileName = "adherents_export";


            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(Window.Current);
            InitializeWithWindow.Initialize(savePicker, hwnd);


            StorageFile file = await savePicker.PickSaveFileAsync();

            if (file != null)
            {

                await FileIO.WriteTextAsync(file, csvData.ToString());


                var dialog = new ContentDialog()
                {
                    Title = "Exportation réussie",
                    Content = "Les adhérents ont été exportés avec succès.",
                    CloseButtonText = "OK"
                };
                await dialog.ShowAsync();
            }
            else
            {

                var dialog = new ContentDialog()
                {
                    Title = "Erreur",
                    Content = "Aucun fichier n'a été sélectionné.",
                    CloseButtonText = "OK"
                };
                await dialog.ShowAsync();
            }
        }
        public List<Adherents> GetAdherents()
        {
            List<Adherents> adherentsList = new List<Adherents>();

            string query = "SELECT * FROM adherents";
            MySqlCommand cmd = new MySqlCommand(query, con);

            try
            {
                con.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string no_identification = reader["no_identification"].ToString();
                    string nom = reader["nom"].ToString();
                    string prenom = reader["prenom"].ToString();
                    string adresse = reader["adresse"].ToString();
                    string date_naissance = reader["date_naissance"].ToString();
                    int age = Convert.ToInt32(reader["age"]);

                    adherentsList.Add(new Adherents(no_identification, nom, prenom, adresse, date_naissance, age));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erreur lors de la récupération des adhérents: {ex.Message}");
            }
            finally
            {
                con.Close();
            }

            return adherentsList;
        }









    }
}
