﻿ using Microsoft.UI.Xaml;
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
    public sealed partial class Ajout_Activites : ContentDialog
    {
        public string Nom { get; set; }
        public int Id_Categorie { get; set; }
        public string Type { get; set; }
        public double Cout_Organisation { get; set; }
        public double Prix_Vente { get; set; }

        public Boolean Valide { get; set; }


        public Ajout_Activites()
        {
            this.InitializeComponent();
        }
        
        int nombre;
        double nombre2;

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Valide = true;  // Par défaut, on suppose que tout est valide

            // Validation du champ "Nom"
            if (string.IsNullOrEmpty(nom_activite.Text))
            {
                erreur_nom.Text = "Le champ nom ne peut être vide";
                erreur_nom.Visibility = Visibility.Visible;
                Valide = false;
            }
            else
            {
                erreur_nom.Visibility = Visibility.Collapsed;
                Nom = nom_activite.Text;
            }

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
                Id_Categorie = id_categorie.SelectedIndex;
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
                Cout_Organisation = coutOrganisation;
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
                Prix_Vente = prixVente;
            }

            // Si l'une des validations échoue, annuler la fermeture du dialog
            if (!Valide)
            {
                args.Cancel = true;
            }
        }




    }
}
