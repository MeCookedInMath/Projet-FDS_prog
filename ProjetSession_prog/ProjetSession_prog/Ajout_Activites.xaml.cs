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
    public sealed partial class Ajout_Activites : ContentDialog
    {
        public string Nom { get; set; }
        public int Id_Categorie { get; set; }
        public string Type { get; set; }
        public double Cout_Organisation { get; set; }
        public double Prix_Vente { get; set; }


        public Ajout_Activites()
        {
            this.InitializeComponent();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Nom = nom_activite.Text;
            Id_Categorie = Convert.ToInt32(id_categorie.Text);




            if ( string.IsNullOrEmpty(Nom))
            {
                args.Cancel = true;
                Title = "Tous les champs doivent être remplis";
            }
        }


    }
}
