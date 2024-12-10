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
using Windows.Foundation;
using Windows.Foundation.Collections;

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
    }
}
