using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ProjetSession_prog
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Affichage : Page 
    {
        public Affichage()
        {
            this.InitializeComponent();
            

            liste_activites.ItemsSource = Singleton.getInstance().getListeActivites();
            liste_adherents.ItemsSource = Singleton.getInstance().getListeAdherents();

        }


        

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

           

            try
            {
                if (Singleton.getInstance().IsSetConnection() == true || Singleton.getInstance().IsSetRole() == "adherent")
                {
                    ListView listView = sender as ListView;

                    Seances seances = listView.SelectedItem as Seances;

                    Singleton.getInstance().creer_Inscriptions(seances.Id, Singleton.getInstance().matricule_connection());

                    
                }
                
                
               
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            
        }

        private void supprimerAdherents_click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Singleton.getInstance().IsSetConnection() == true && Singleton.getInstance().IsSetRole() == "admin")
                {
                    Button button = sender as Button;

                    Adherents adherent = button.DataContext as Adherents;

                    liste_adherents.SelectedItem = adherent;

                    var collectionProduits = liste_adherents.ItemsSource as ObservableCollection<Adherents>;

                    collectionProduits.Remove(adherent);

                    Singleton.getInstance().supprimerAdherents(adherent.No_Identification);
                }
                else
                {
                    
                }



            }
            catch (Exception error)
            {
                Debug.WriteLine(error.Message);
            }
        }

        private void supprimerActivites_click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Singleton.getInstance().IsSetConnection()==true && Singleton.getInstance().IsSetRole() == "admin")
                {
                    Button button = sender as Button;

                    Activites activite = button.DataContext as Activites;

                    liste_activites.SelectedItem = activite;

                    var collectionProduits = liste_activites.ItemsSource as ObservableCollection<Activites>;

                    collectionProduits.Remove(activite);

                    Singleton.getInstance().supprimerActivites(activite.Nom);
                }
                else
                {
                   
                }

                

            }
            catch (Exception error)
            {
                Debug.WriteLine(error.Message);
            }
        }

        private async void modifierAdherents_click(object sender, RoutedEventArgs e)
        {
            Button modifier = sender as Button;
            Adherents adherent = modifier.DataContext as Adherents;

            ModifierAdherents dialog = new ModifierAdherents(adherent.Nom, adherent.Prenom, adherent.Adresse);
            dialog.XamlRoot = this.XamlRoot;
            dialog.Title = "Modifier l'adhérent";
            dialog.PrimaryButtonText = "Modifier";
            dialog.CloseButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Close;

            ContentDialogResult resultat = await dialog.ShowAsync();


            if (resultat == ContentDialogResult.Primary)
            {
                string noIdentification = adherent.No_Identification;

                string nom = dialog.Nom;
                
                string prenom = dialog.Prenom;

                string adresse = dialog.Adresse;

                string dateNaissance = dialog.Date_Naissance;


                if (Singleton.getInstance().IsSetConnection() && Singleton.getInstance().IsSetRole() == "admin")
                {
                    try
                    {
                        Singleton.getInstance().modifierAdherents(noIdentification, nom, prenom, adresse, dateNaissance);
                    }
                    catch(MySqlException ex) { 
                        Debug.WriteLine(ex.Message);
                        
                    }
                    

                    liste_adherents.ItemsSource = Singleton.getInstance().getListeAdherents();
                }

            }

        }

        private async void modifierActivites_click(object sender, RoutedEventArgs e)
        {
            Button modifier = sender as Button;
            Activites activites = modifier.DataContext as Activites;

            ModifierActivites dialog = new ModifierActivites(activites.Id_Categorie, activites.Type, activites.Cout_organisation, activites.Prix_vente);
            dialog.XamlRoot = this.XamlRoot;
            dialog.Title = "Modifier l'activité";
            dialog.PrimaryButtonText = "Modifier";
            dialog.CloseButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Close;

            ContentDialogResult resultat = await dialog.ShowAsync();


            if (resultat == ContentDialogResult.Primary)
            {
                string nomActivite = activites.Nom;

                int id_categorie = dialog.Id_Categorie;

                string type = dialog.Type;

                double cout_organisation = dialog.Cout_Organisation;

                double prix_vente = dialog.Prix_Vente;

                if (Singleton.getInstance().IsSetConnection() && Singleton.getInstance().IsSetRole() == "admin")
                {
                    try
                    {
                        Singleton.getInstance().modifierActivites(nomActivite, id_categorie+1, type, cout_organisation, prix_vente);
                    }
                    catch (MySqlException ex)
                    {
                        Debug.WriteLine(ex.Message);
                        
                    }
                    
                    liste_activites.ItemsSource = Singleton.getInstance().getListeActivites();
                }
            }
        }

        private async void lv_SeancesASupprimer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView modifier = sender as ListView;

            Seances seance = modifier.SelectedItem as Seances;



            ModifierSeances dialog = new ModifierSeances(seance.Nom_Activite, seance.Date, seance.Heure, seance.Nbr_Places);
            dialog.XamlRoot = this.XamlRoot;
            dialog.Title = "Modifier l'activité";
            dialog.PrimaryButtonText = "Modifier";
            dialog.SecondaryButtonText = "Supprimer";
            dialog.CloseButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Close;

            ContentDialogResult resultat = await dialog.ShowAsync();


            if (resultat == ContentDialogResult.Primary)
            {
                int id = seance.Id;

                string nomActivite = dialog.NomActivite;

                string date = dialog.Date;

                string heure = dialog.Heure;

                int nbrPlaces = dialog.Nbr_Places;

                if (Singleton.getInstance().IsSetConnection() && Singleton.getInstance().IsSetRole() == "admin")
                {
                    try
                    {
                        Singleton.getInstance().modifierSeances(id, nomActivite, date, heure, nbrPlaces);
                    }
                    catch (MySqlException ex)
                    {
                        Debug.WriteLine(ex.Message);
                        
                    }
                    
                    
                }
            }


            // checker cette méthode car quand on modifie, ca supprime tout de suite après

            if (resultat == ContentDialogResult.Secondary)
            {
                if (Singleton.getInstance().IsSetConnection() && Singleton.getInstance().IsSetRole() == "admin")
                {
                    try
                    {
                        Singleton.getInstance().supprimerSeances(seance.Id);

                        ObservableCollection<Seances> collectionSeances = modifier.ItemsSource as ObservableCollection<Seances>;

                        if (collectionSeances != null)
                        {
                            collectionSeances.Remove(seance);
                        }

                        
                    }
                    catch (MySqlException ex)
                    {
                        Debug.WriteLine(ex.Message);
                        
                    }
                    
                    
                }
            }

        }

        private async void listeSeancesParAdhérent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (Singleton.getInstance().IsSetConnection() == true || Singleton.getInstance().IsSetRole() == "admin")
                {
                    ListView listView = sender as ListView;

                    Seances seance = listView.SelectedItem as Seances;

                    Adherents adherent = listView.DataContext as Adherents;


                    SupprimerInscription dialog = new SupprimerInscription();
                    dialog.XamlRoot = this.XamlRoot;
                    dialog.PrimaryButtonText = "Accepter";
                    dialog.CloseButtonText = "Annuler";
                    dialog.DefaultButton = ContentDialogButton.Close;

                    ContentDialogResult resultat = await dialog.ShowAsync();


                    if (resultat == ContentDialogResult.Primary)
                    {
                        try
                        {
                            Singleton.getInstance().supprimerInscriptions(adherent.No_Identification, seance.Id);
                        }
                        catch (MySqlException ex)
                        {
                            Debug.WriteLine(ex.Message);
                        }

                        listView.ItemsSource = Singleton.getInstance().getListeSeancesPourAdhérents(Singleton.getInstance().matricule_connection());
                    }
                }



            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
