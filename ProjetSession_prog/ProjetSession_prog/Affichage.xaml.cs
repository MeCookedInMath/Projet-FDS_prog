using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
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
    public sealed partial class Affichage : Page
    {
        public Affichage()
        {
            this.InitializeComponent();
            verif_inscriptions.Text = null;

            liste_activites.ItemsSource = Singleton.getInstance().getListeActivites();
            liste_adherents.ItemsSource = Singleton.getInstance().getListeAdherents();

        }

        private void supprimer_click(object sender, RoutedEventArgs e)
        {

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

                    verif_inscriptions.Text = $"Vous êtes maintenant inscrits à cette activité.";
                }
                else
                {
                    verif_inscriptions.Text = $"Vous ne pouvez vous inscrire à cette activité si vous n'êtes pas connecté en tant qu'adhérent";
                }
                
               
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            
        }
    }
}
