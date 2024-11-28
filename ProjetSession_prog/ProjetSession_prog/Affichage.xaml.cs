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

        private void supprimer_click(object sender, RoutedEventArgs e)
        {

        }

        

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView listView = sender as ListView;
            Activites activites = listView.DataContext as Activites;
            Seances seances = listView.SelectedItem as Seances;


        }
    }
}
