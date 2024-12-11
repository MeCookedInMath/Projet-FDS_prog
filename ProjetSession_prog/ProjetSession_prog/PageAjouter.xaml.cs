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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Pickers;
using Windows.Storage;
using WinRT.Interop;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ProjetSession_prog
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageAjouter : Page
    {
        public PageAjouter()
        {
            this.InitializeComponent();
        }

        private async void Btn_AjoutActivites_Click(object sender, RoutedEventArgs e)
        {
            Ajout_Activites dialog = new Ajout_Activites();
            dialog.XamlRoot = this.XamlRoot;
            dialog.Title = "Ajouter une activité";
            dialog.PrimaryButtonText = "Enregistrer";
            dialog.CloseButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Close;

            ContentDialogResult resultat = await dialog.ShowAsync();

            if (resultat == ContentDialogResult.Primary)
            {
                
                
                    string nom = dialog.Nom;
                    int id_categorie = dialog.Id_Categorie;
                    string type = dialog.Type;
                    double cout_organisation = dialog.Cout_Organisation;
                    double prix_vente = dialog.Prix_Vente;

                    try
                    {
                        Singleton.getInstance().creer_Activites(nom, id_categorie, type, cout_organisation, prix_vente);
                        validation_click.Text = "L'ajout a bien fonctionné";
                    }
                    catch (MySqlException ex) { 
                        Debug.WriteLine("L'ajout n'a pas fonctionné" + ex.Message);
                        validation_click.Text = "L'ajout n'a pas fonctionné";
                    }
                

                
                

                
            }
        }

        private async void Btn_AjoutAdherents_Click(object sender, RoutedEventArgs e)
        {
            Ajout_Adherents dialog = new Ajout_Adherents();
            dialog.XamlRoot = this.XamlRoot;
            dialog.Title = "Ajouter un adhérent";
            dialog.PrimaryButtonText = "Enregistrer";
            dialog.CloseButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Close;

            ContentDialogResult resultat = await dialog.ShowAsync();

            if (resultat == ContentDialogResult.Primary)
            {
                string nom = dialog.Nom;
                string prenom = dialog.Prenom;
                string adresse = dialog.Adresse;
                string date_naissance = dialog.Date_Naissance;

                try
                {
                    Singleton.getInstance().creer_Adherents(nom, prenom, adresse, date_naissance);
                    validation_click1.Text = "L'ajout a bien fonctionné";
                }
                catch (MySqlException ex)
                {
                    Debug.WriteLine("L'ajout n'a pas fonctionné" + ex.Message);
                    validation_click1.Text = "L'ajout n'a pas fonctionné";
                }



            }
        }

        private async void Btn_AjoutSeances_Click(object sender, RoutedEventArgs e)
        {
            Ajout_Seances dialog = new Ajout_Seances();
            dialog.XamlRoot = this.XamlRoot;
            dialog.Title = "Ajouter une séance";
            dialog.PrimaryButtonText = "Enregistrer";
            dialog.CloseButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Close;

            ContentDialogResult resultat = await dialog.ShowAsync();

            if (resultat == ContentDialogResult.Primary)
            {
                string nomActivite = dialog.Nom_Activite;
                string heure = dialog.Heure;
                string date = dialog.Date;
                int nbr_places = dialog.Nbr_Places;

                try
                {
                    Singleton.getInstance().creer_Seances(nomActivite, date, heure, nbr_places);
                    validation_click2.Text = "L'ajout a bien fonctionné";
                }
                catch (MySqlException ex)
                {
                    Debug.WriteLine("L'ajout n'a pas fonctionné" + ex.Message);
                    validation_click2.Text = "L'ajout n'a pas fonctionné";
                }



            }
        }



        private async void Btn_Exporter_Adherents_Click(object sender, RoutedEventArgs e)
        {

            var adherentsList = Singleton.getInstance().GetAdherents();


            StringBuilder csvData = new StringBuilder();
            csvData.AppendLine("Numéro d'identification,Nom,Prénom,Adresse,Date de naissance,Âge");

            foreach (var adherent in adherentsList)
            {
                csvData.AppendLine($"{adherent.No_Identification},{adherent.Nom},{adherent.Prenom},{adherent.Adresse},{adherent.Date_Naissance},{adherent.Age}");
            }

            var window = App.MainWindow;

            var savePicker = new FileSavePicker
            {
                SuggestedStartLocation = PickerLocationId.Desktop
            };
            savePicker.FileTypeChoices.Add("CSV", new List<string> { ".csv" });
            savePicker.SuggestedFileName = "adherents_export";

            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(window);
            InitializeWithWindow.Initialize(savePicker, hwnd);


            StorageFile file = await savePicker.PickSaveFileAsync();


            if (file != null)
            {

                await FileIO.WriteTextAsync(file, csvData.ToString());

                Singleton.getInstance().setMessageUtilisateur("Les adhérents ont été exportées avec succès.", this);

            }
            else
            {

                Singleton.getInstance().setMessageUtilisateur("Erreur lors de l'exportation.", this);

            }
        }


        public async void Btn_Exporter_Activites_Click(object sender, RoutedEventArgs e)
        {
            var activitesList = Singleton.getInstance().GetActivities();

            StringBuilder csvData = new StringBuilder();
            csvData.AppendLine("Nom,Catégorie,Type,Coût d'organisation,Prix de vente");


            foreach (var activite in activitesList)
            {
                csvData.AppendLine($"{activite.Nom},{activite.Id_Categorie},{activite.Type},{activite.Cout_organisation},{activite.Prix_vente}");
            }

            var window = App.MainWindow;

            var savePicker = new FileSavePicker
            {
                SuggestedStartLocation = PickerLocationId.Desktop
            };
            savePicker.FileTypeChoices.Add("CSV", new List<string> { ".csv" });
            savePicker.SuggestedFileName = "activites_export";

            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(window);


            InitializeWithWindow.Initialize(savePicker, hwnd);


            StorageFile file = await savePicker.PickSaveFileAsync();

            if (file != null)
            {

                await FileIO.WriteTextAsync(file, csvData.ToString());

                Singleton.getInstance().setMessageUtilisateur("Les activités ont été exportées avec succès.", this);
                

            }
            else
            {

                Singleton.getInstance().setMessageUtilisateur("Erreur lors de l'exportation.", this);

            }
        }








    }
}
