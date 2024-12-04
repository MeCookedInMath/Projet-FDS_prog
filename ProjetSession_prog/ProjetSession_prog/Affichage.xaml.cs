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
            verif_inscriptions.Text = null;

            liste_activites.ItemsSource = Singleton.getInstance().getListeActivites();
            liste_adherents.ItemsSource = Singleton.getInstance().getListeAdherents();

        }


        

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            verif_inscriptions.Text = "allo";

            try
            {
                if (Singleton.getInstance().IsSetConnection() == true || Singleton.getInstance().IsSetRole() == "adherent")
                {
                    ListView listView = sender as ListView;

                    Seances seances = listView.SelectedItem as Seances;

                    Singleton.getInstance().creer_Inscriptions(seances.Id, Singleton.getInstance().matricule_connection());

                    verif_inscriptions.Text = $"Vous �tes maintenant inscrits � cette activit�.";
                }
                else
                {
                    verif_inscriptions.Text = $"Vous ne pouvez vous inscrire � cette activit� si vous n'�tes pas connect� en tant qu'adh�rent";
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
                    verif_inscriptions.Text = "Vous ne pouvez supprimer un adh�rent si vous n'�tes pas un administrateur";
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
                    verif_inscriptions.Text = "Vous ne pouvez supprimer une activit� si vous n'�tes pas un administrateur";
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
            dialog.Title = "Modifier l'adh�rent";
            dialog.PrimaryButtonText = "Modifier";
            dialog.CloseButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Close;

            ContentDialogResult resultat = await dialog.ShowAsync();


            if (resultat == ContentDialogResult.Primary)
            {
                string nom = dialog.Nom;
                
                string prenom = dialog.Prenom;

                string adresse = dialog.Adresse;

                



            }

        }

        private async void modifierActivites_click(object sender, RoutedEventArgs e)
        {
            Button modifier = sender as Button;
            Activites activites = modifier.DataContext as Activites;

            ModifierActivites dialog = new ModifierActivites(activites.Id_Categorie, activites.Type, activites.Cout_organisation, activites.Prix_vente);
            dialog.XamlRoot = this.XamlRoot;
            dialog.Title = "Modifier l'adh�rent";
            dialog.PrimaryButtonText = "Modifier";
            dialog.CloseButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Close;

            ContentDialogResult resultat = await dialog.ShowAsync();


            if (resultat == ContentDialogResult.Primary)
            {
                int id_categorie = dialog.Id_Categorie;

                string type = dialog.Type;

                double cout_organisation = dialog.Cout_Organisation;

                double prix_vente = dialog.Prix_Vente;





            }
        }
    }
}
