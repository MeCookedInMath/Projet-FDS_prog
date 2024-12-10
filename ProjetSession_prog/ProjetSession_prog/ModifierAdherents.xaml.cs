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
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ProjetSession_prog
{
    public sealed partial class ModifierAdherents : ContentDialog
    {

        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Adresse { get; set; }

        public string Date_Naissance { get; set; }

        public Boolean Valide { get; set; }

        public ModifierAdherents(string _nom, string _prenom, string _adresse)
        {
            this.InitializeComponent();

            nom.Text = _nom;
            prenom.Text = _prenom;
            adresse.Text = _adresse;
            
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Valide = true;


            if (string.IsNullOrEmpty(nom.Text))
            {
                erreur_nom.Visibility = Visibility.Visible;
                erreur_nom.Text = "Ce champ ne peut pas être vide";
                Valide = false;
            }
            else
            {
                erreur_nom.Visibility = Visibility.Collapsed;
                Nom = nom.Text;
            }


            if (string.IsNullOrEmpty(prenom.Text))
            {
                erreur_prenom.Visibility = Visibility.Visible;
                erreur_prenom.Text = "Ce champ ne peut pas être vide";
                Valide = false;
            }
            else
            {
                erreur_prenom.Visibility = Visibility.Collapsed;
                Prenom = prenom.Text;
            }


            if (string.IsNullOrEmpty(adresse.Text))
            {
                erreur_adresse.Visibility = Visibility.Visible;
                erreur_adresse.Text = "Ce champ ne peut pas être vide";
                Valide = false;
            }
            else
            {
                if (Regex.IsMatch(adresse.Text, "^[0-9] [a-z]$")) // rechecker le regex demain au plus sacrant
                {
                    erreur_adresse.Visibility = Visibility.Collapsed;
                    Adresse = adresse.Text;
                }
                else
                {
                    erreur_adresse.Visibility = Visibility.Visible;
                    erreur_adresse.Text = "L'adresse donnée n'est pas conforme au format voulu.(ex : 4291 rang de l'acadie)";
                    Valide = false;
                }
            }


            if (date_naissance.Date == null)
            {
                erreur_dateNaissance.Visibility = Visibility.Visible;
                erreur_dateNaissance.Text = "Ce champ ne peut pas être vide";
                Valide = false;
            }
            else
            {
                erreur_dateNaissance.Visibility = Visibility.Collapsed;
                Date_Naissance = date_naissance.Date.ToString("yyyy-MM-dd");
            }


            args.Cancel = !Valide;
        }
    }
}
