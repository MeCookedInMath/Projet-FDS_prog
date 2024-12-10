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

            date_seance.MinYear = DateTimeOffset.Now;
            date_seance.MaxYear = DateTimeOffset.Now.AddYears(3);

            foreach (Activites activite in Singleton.getInstance().getListeActivites())
            {
                nom_activite.Items.Add(activite.Nom);
            }
        }




        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
           
            erreur_nom.Visibility = Visibility.Collapsed;
            erreur_date.Visibility = Visibility.Collapsed;
            erreur_heure.Visibility = Visibility.Collapsed;
            erreur_nbrPLaces.Visibility = Visibility.Collapsed;

            Valide = true;

            
            if (string.IsNullOrEmpty(nom_activite.SelectedItem?.ToString()))
            {
                erreur_nom.Visibility = Visibility.Visible;
                erreur_nom.Text = "Ce champ doit être rempli";
                Valide = false;
            }
            else
            {
                Nom_Activite = nom_activite.SelectedItem.ToString();
            }

           
            if (date_seance.Date == null)
            {
                erreur_date.Visibility = Visibility.Visible;
                erreur_date.Text = "Ce champ doit être rempli";
                Valide = false;
            }
            else
            {
                Date = date_seance.Date.ToString("yyyy-MM-dd");
            }

            
            if (heure_seance.Time == null)
            {
                erreur_heure.Visibility = Visibility.Visible;
                erreur_heure.Text = "Ce champ doit être rempli";
                Valide = false;
            }
            else
            {
                Heure = heure_seance.Time.ToString();
            }

           
            if (string.IsNullOrEmpty(nbr_places.Text))
            {
                erreur_nbrPLaces.Visibility = Visibility.Visible;
                erreur_nbrPLaces.Text = "Ce champ doit être rempli";
                Valide = false;
            }
            else
            {
                if (!int.TryParse(nbr_places.Text, out int nombre) || nombre <= 0)
                {
                    erreur_nbrPLaces.Visibility = Visibility.Visible;
                    erreur_nbrPLaces.Text = "La valeur entrée doit être un nombre valide supérieur à zéro";
                    Valide = false;
                }
                else
                {
                    Nbr_Places = nombre;
                }
            }

            
            args.Cancel = !Valide;
        }

    }
}
