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
    public sealed partial class Ajout_Seances : ContentDialog
    {

        public string Nom_Activite { get; set; }
        public string Date { get; set; }

        public string Heure { get; set; }

        public int Nbr_Places { get; set; }

        public Boolean Valide {  get; set; }

        public Ajout_Seances()
        {
            this.InitializeComponent();

            foreach (Activites activite in Singleton.getInstance().getListeActivites())
            {
                nom_activite.Items.Add(activite.Nom);
            }
        }



        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Valide = true;
             int nombre;

            
            
            Nbr_Places = Convert.ToInt32(nbr_places.Text);

            if (string.IsNullOrEmpty(nom_activite.SelectedItem.ToString()))
            {
                erreur_nom.Visibility = Visibility.Visible;
                erreur_nom.Text = "Ce champ doit être rempli";
                Valide = false;
            }
            else {
                erreur_nom.Visibility = Visibility.Collapsed;
                Valide = true;
                Nom_Activite = nom_activite.SelectedItem.ToString();
            }

            if (string.IsNullOrEmpty(date_seance.Date.ToString()))
            {
                erreur_date.Visibility = Visibility.Visible;
                erreur_date.Text = "Ce champ doit être rempli";
                Valide = false;
            }
            else
            {
                erreur_date.Visibility = Visibility.Collapsed;
                Valide = true;
                Date = date_seance.Date.ToString("yyyy-MM-dd");
            }


            if (string.IsNullOrEmpty( heure_seance.Time.ToString()) )
            {
                erreur_heure.Visibility = Visibility.Visible;
                erreur_heure.Text = "Ce champ doit être rempli";
                Valide = false;
            }
            else
            {
                erreur_heure.Visibility = Visibility.Collapsed;
                Valide = true;
                Heure = heure_seance.Time.ToString();

            }


            if (string.IsNullOrEmpty(nbr_places.Text))
            {
                erreur_nbrPLaces.Visibility = Visibility.Visible;
                erreur_nbrPLaces.Text = "Ce champ doit être rempli";
                Valide = false;
            }
            else {
                if (Int32.TryParse(Nbr_Places.ToString(), out nombre) == false)
                {
                    erreur_nbrPLaces.Visibility = Visibility.Visible;
                    erreur_nbrPLaces.Text = "La valeur entré doit être numérique";
                    Valide = false;
                }
                else
                {
                    erreur_nbrPLaces.Visibility = Visibility.Collapsed;
                    Valide = true;
                    Nbr_Places = Convert.ToInt32(nbr_places.Text);
                }
            }


            if (Valide) {
                args.Cancel = true;
            }
            else
            {
                args.Cancel = false;
            }

            
        }

    }
}
