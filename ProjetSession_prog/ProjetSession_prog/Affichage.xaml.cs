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
            

            
            liste_adherents.ItemsSource = Singleton.getInstance().getListeAdherents();


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

                    Singleton.getInstance().setMessageUtilisateur("La suppression de l'adhérent a fonctionné", this);
                }
                else
                {
                    Singleton.getInstance().setMessageUtilisateur("Vous ne pouvez supprimer une activité si vous n'êtes pas un administrateur", this);
                }




            }
            catch (Exception error)
            {
                Debug.WriteLine(error.Message);
                Singleton.getInstance().setMessageUtilisateur("La suppression de l'adhérent n'a pas fonctionné", this);
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
                        
                        Singleton.getInstance().setMessageUtilisateur("La modification a bien fonctionné", this);
                        
                    }
                    catch(MySqlException ex) { 
                        Debug.WriteLine(ex.Message);
                        
                    }
                    

                    liste_adherents.ItemsSource = Singleton.getInstance().getListeAdherents();
                }
                else
                {
                    Singleton.getInstance().setMessageUtilisateur("Vous ne pouvez modifier un adhérent si vous n'êtes pas un administrateur", this);
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

                            Singleton.getInstance().setMessageUtilisateur("L'adhérent a bien été désinscrits de cette séance", this);
                        }
                        catch (MySqlException ex)
                        {
                            Debug.WriteLine(ex.Message);
                            Singleton.getInstance().setMessageUtilisateur("L'adhérent n'a pas été désinscrits de cette séance", this);
                        }

                        listView.ItemsSource = Singleton.getInstance().getListeSeancesPourAdhérents(Singleton.getInstance().matricule_connection());
                    }
                }
                else {
                    Singleton.getInstance().setMessageUtilisateur("L'adhérent n'a pas pu être désinscrits de cette séance", this);
                }




            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }


        


    }
}
