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
    public sealed partial class Ajout_Adherents : ContentDialog
    {

        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Adresse { get; set; }
        public string Date_Naissance { get; set; }
        public Ajout_Adherents()
        {
            this.InitializeComponent();
        }


        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Nom = nom.Text;
            Prenom = prenom.Text;
            Adresse = adresse.Text;
            Date_Naissance = date_naissance.Date.ToString("yyyy-MM-dd");



            if (string.IsNullOrEmpty(Nom) || string.IsNullOrEmpty(Prenom) || string.IsNullOrEmpty(Adresse) || string.IsNullOrEmpty(Date_Naissance))
            {
                args.Cancel = true;
                Title = "Tous les champs doivent être remplis";
            }
        }


    }
}
