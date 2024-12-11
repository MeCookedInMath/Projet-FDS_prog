using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;
using System.Diagnostics;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ProjetSession_prog
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AffichageActivites : Page
    {
        public AffichageActivites()
        {
            this.InitializeComponent();

            liste_activites.ItemsSource = Singleton.getInstance().getListeActivites();
        }



        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            try
            {
                if (Singleton.getInstance().IsSetConnection() == true && Singleton.getInstance().IsSetRole() == "adherent")
                {
                    ListView listView = sender as ListView;

                    Seances seances = listView.SelectedItem as Seances;

                    Singleton.getInstance().creer_Inscriptions(seances.Id, Singleton.getInstance().matricule_connection());

                    Singleton.getInstance().setMessageUtilisateur("L'inscription a fonctionné", this);


                }
                else
                {
                    Singleton.getInstance().setMessageUtilisateur("Vous ne pouvez vous inscrire si vous n'êtes pas un adhérent", this);

                }



            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Singleton.getInstance().setMessageUtilisateur("L'inscription n'a pas fonctionné", this);
            }


        }


        private void supprimerActivites_click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Singleton.getInstance().IsSetConnection() == true && Singleton.getInstance().IsSetRole() == "admin")
                {
                    Button button = sender as Button;

                    Activites activite = button.DataContext as Activites;

                    liste_activites.SelectedItem = activite;

                    var collectionProduits = liste_activites.ItemsSource as ObservableCollection<Activites>;

                    collectionProduits.Remove(activite);

                    Singleton.getInstance().supprimerActivites(activite.Nom);

                    Singleton.getInstance().setMessageUtilisateur("La suppression de l'activité a bien fonctionné", this);
                }
                else
                {
                    Singleton.getInstance().setMessageUtilisateur("Vous ne pouvez supprimer une activité si vous n'êtes pas un administrateur", this);
                }



            }
            catch (Exception error)
            {
                Debug.WriteLine(error.Message);
                Singleton.getInstance().setMessageUtilisateur("La suppression de l'activité n'a pas fonctionné", this);
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
                        Singleton.getInstance().modifierActivites(nomActivite, id_categorie + 1, type, cout_organisation, prix_vente);

                        Singleton.getInstance().setMessageUtilisateur("L'activité a bien été modifiée", this);
                    }
                    catch (MySqlException ex)
                    {
                        Debug.WriteLine(ex.Message);
                        Singleton.getInstance().setMessageUtilisateur("L'activité n'a pas été modifiée", this);
                    }

                    liste_activites.ItemsSource = Singleton.getInstance().getListeActivites();
                }
                else
                {
                    Singleton.getInstance().setMessageUtilisateur("Vous ne pouvez pas modifier une activité si vous n'êtes pas un administrateur", this);
                }


            }
        }

        private async void lv_SeancesAModifier_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
                else
                {
                    Singleton.getInstance().setMessageUtilisateur("Vous ne pouvez modifier une séance si vous n'êtes pas un administrateur", this);
                    
                }


            }




            if (resultat == ContentDialogResult.Secondary)
            {
                if (Singleton.getInstance().IsSetConnection() && Singleton.getInstance().IsSetRole() == "admin")
                {
                    try
                    {
                        Singleton.getInstance().supprimerSeances(seance.Id);

                        Singleton.getInstance().setMessageUtilisateur("La séance a bien été supprimée", this);

                        ObservableCollection<Seances> collectionSeances = modifier.ItemsSource as ObservableCollection<Seances>;

                        if (collectionSeances != null)
                        {
                            collectionSeances.Remove(seance);
                        }


                    }
                    catch (MySqlException ex)
                    {
                        Debug.WriteLine(ex.Message);
                        Singleton.getInstance().setMessageUtilisateur("La séance n'a pas été supprimée", this);
                    }


                }
                else
                {
                    Singleton.getInstance().setMessageUtilisateur("Vous ne pouvez supprimer une séance si vous n'êtes pas un administrateur", this);
                }


            }

        }



    }
}
