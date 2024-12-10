using Google.Protobuf.WellKnownTypes;
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
    public sealed partial class ModifierActivites : ContentDialog
    {

     
        public int Id_Categorie { get; set; }
        public string Type { get; set; }
        public double Cout_Organisation { get; set; }
        public double Prix_Vente { get; set; }

        public Boolean Valide { get; set; }


        public ModifierActivites(int _id_categorie, string _type, double _cout_organisation, double _prix)
        {
            this.InitializeComponent();

            id_categorie.SelectedIndex = _id_categorie - 1;
            type.Text = _type;
            cout_organisation.Text = _cout_organisation.ToString();
            prix_vente.Text = _prix.ToString();

        }



        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Valide = true;

            // Validation de la catégorie sélectionnée
            if (id_categorie.SelectedIndex == -1)
            {
                erreur_categorie.Text = "Veuillez sélectionner une catégorie";
                erreur_categorie.Visibility = Visibility.Visible;
                Valide = false;
            }
            else
            {
                erreur_categorie.Visibility = Visibility.Collapsed;
                Id_Categorie = id_categorie.SelectedIndex + 1;
            }

            // Validation du coût d'organisation (chiffres uniquement)
            if (string.IsNullOrEmpty(cout_organisation.Text))
            {
                erreur_coutOrganisation.Text = "Ce champ doit être rempli";
                erreur_coutOrganisation.Visibility = Visibility.Visible;
                Valide = false;
            }
            else if (!double.TryParse(cout_organisation.Text, out double coutOrganisation))
            {
                erreur_coutOrganisation.Text = "La valeur insérée doit être numérique";
                erreur_coutOrganisation.Visibility = Visibility.Visible;
                Valide = false;
            }
            else
            {
                erreur_coutOrganisation.Visibility = Visibility.Collapsed;
                Cout_Organisation = Convert.ToDouble(cout_organisation.Text);
            }

            // Validation du champ "Type"
            if (string.IsNullOrEmpty(type.Text))
            {
                erreur_type.Text = "Le champ type ne peut être vide";
                erreur_type.Visibility = Visibility.Visible;
                Valide = false;
            }
            else
            {
                erreur_type.Visibility = Visibility.Collapsed;
                Type = type.Text;
            }

            // Validation du prix de vente (chiffres uniquement)
            if (string.IsNullOrEmpty(prix_vente.Text))
            {
                erreur_prixVente.Text = "Ce champ doit être rempli";
                erreur_prixVente.Visibility = Visibility.Visible;
                Valide = false;
            }
            else if (!double.TryParse(prix_vente.Text, out double prixVente))
            {
                erreur_prixVente.Text = "La valeur insérée doit être numérique";
                erreur_prixVente.Visibility = Visibility.Visible;
                Valide = false;
            }
            else
            {
                erreur_prixVente.Visibility = Visibility.Collapsed;
                Prix_Vente = Convert.ToDouble(prix_vente.Text);
            }

            // Si l'une des validations échoue, annuler la fermeture du dialog
            if (!Valide)
            {
                args.Cancel = true;
            }
        }
    }
}
