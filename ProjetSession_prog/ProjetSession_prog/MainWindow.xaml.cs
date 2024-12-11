using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ProjetSession_prog
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {

        private bool IsAdmin = false;
        public MainWindow()
        {


            this.InitializeComponent();
            

            mainFrame.Navigate(typeof(Affichage));

            UpdateMenuVisibilityAsync();
        }

        public async Task UpdateMenuVisibilityAsync()
        {


            await Task.Yield();
            
            if (Singleton.getInstance().IsSetConnection() == true && Singleton.getInstance().IsSetRole() == "admin")
            {
                // Afficher les éléments réservés aux administrateurs
                iStatistiques.Visibility = Visibility.Visible;
                iAjouter.Visibility = Visibility.Visible;

                navItemHeader.Content = "Connecté en tant que qu'administrateur"; 
                
            }
            else
            {
                // Masquer les éléments réservés aux administrateurs
                iStatistiques.Visibility = Visibility.Collapsed;
                iAjouter.Visibility = Visibility.Collapsed;
                 
            }

            if (Singleton.getInstance().IsSetConnection() == true && Singleton.getInstance().IsSetRole() == "adherent")
            {
                Adherents adherentConnecte = Singleton.getInstance().getAdherentConnecte();
                navItemHeader.Content = "Connecté en tant que " + adherentConnecte.Prenom + " " + adherentConnecte.Nom;


                iInscriptions.Visibility = Visibility.Visible;
            }
            else
            {
                iInscriptions.Visibility = Visibility.Collapsed;
            }
        }



        private void navView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var item = (NavigationViewItem)args.SelectedItem;

            UpdateMenuVisibilityAsync();
            
            switch (item.Name)
            {
                case "iAuthentification":
                    mainFrame.Navigate(typeof(Authentification));
                    break;
                case "iStatistiques":
                    mainFrame.Navigate(typeof(Statistiques));
                    break;
                case "iAffichage":
                    mainFrame.Navigate(typeof(Affichage));
                    break;
                case "iAjouter":
                    mainFrame.Navigate(typeof(PageAjouter));
                    break;
                case "iInscriptions":
                    mainFrame.Navigate(typeof(Inscriptions));
                    break;


                default:
                    break;
            }

            

        }



       




    }
}
