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
    public sealed partial class Inscriptions : Page
    {
        public Inscriptions()
        {
            this.InitializeComponent();

            listeSeancesParAdherent.ItemsSource = Singleton.getInstance().getListeSeancesPourAdhérents(   Singleton.getInstance().matricule_connection()   );
        }

        

        private async void SeancesPourAdherent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView listView = sender as ListView;

            Seances seance = listView.SelectedItem as Seances;


            SupprimerInscription dialog = new SupprimerInscription();
            dialog.XamlRoot = this.XamlRoot;
            dialog.PrimaryButtonText = "Accepter";
            dialog.CloseButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Close;

            ContentDialogResult resultat = await dialog.ShowAsync();

            if (Singleton.getInstance().IsSetConnection() == true && Singleton.getInstance().IsSetRole() == "adherent")
            {
                if (resultat == ContentDialogResult.Primary)
                {
                    try
                    {
                        Singleton.getInstance().supprimerInscriptions(Singleton.getInstance().matricule_connection(), seance.Id);

                        Singleton.getInstance().setMessageUtilisateur("L'adhérent a bien été désinscrits de cette séance", this);
                    }
                    catch(MySqlException ex) {
                        Debug.WriteLine(ex.Message);

                        Singleton.getInstance().setMessageUtilisateur("L'adhérent n'a pas été désinscrits de cette séance", this);
                    }

                    listeSeancesParAdherent.ItemsSource = Singleton.getInstance().getListeSeancesPourAdhérents(Singleton.getInstance().matricule_connection());
                }
            }
            else {
                Singleton.getInstance().setMessageUtilisateur("Vous ne pouvez vous inscrire à une activité en tant qu'adhérent", this);
                this.Frame.Navigate(typeof(Affichage));
            }

            
        }

        

        private async void evaluer_seances_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Seances seances = button.DataContext as Seances;

            EvaluerSeances dialog = new EvaluerSeances();
            dialog.XamlRoot = this.XamlRoot;
            dialog.Title = "Évaluation de la séance";
            dialog.PrimaryButtonText = "Enregistrer la note";
            dialog.CloseButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Close;

            ContentDialogResult resultat = await dialog.ShowAsync();


            if (resultat == ContentDialogResult.Primary)
            {
                try
                {
                    
                    Singleton.getInstance().evaluerSeances(Singleton.getInstance().matricule_connection(), seances.Id, dialog.Note);

                    Singleton.getInstance().setMessageUtilisateur("Votre évaluation a été enregistrée", this);
                }
                catch (MySqlException ex)
                {
                    Debug.WriteLine(ex.Message);
                    Singleton.getInstance().setMessageUtilisateur("Votre évaluation n'a pas été enregistrée", this);
                }

                listeSeancesParAdherent.ItemsSource = Singleton.getInstance().getListeSeancesPourAdhérents(Singleton.getInstance().matricule_connection());
            }

        }
    }
}
