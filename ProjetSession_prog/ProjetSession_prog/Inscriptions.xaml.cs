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


            if (resultat == ContentDialogResult.Primary)
            {
                try
                {
                    Singleton.getInstance().supprimerInscriptions(Singleton.getInstance().matricule_connection(), seance.Id);
                }
                catch(MySqlException ex) {
                    Debug.WriteLine(ex.Message);
                }

                listeSeancesParAdherent.ItemsSource = Singleton.getInstance().getListeSeancesPourAdhérents(Singleton.getInstance().matricule_connection());
            }
        }
    }
}
