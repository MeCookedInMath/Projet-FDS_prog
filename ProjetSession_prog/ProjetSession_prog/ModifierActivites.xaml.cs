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


        public ModifierActivites(int _id_categorie, string _type, double _cout_organisation, double _prix)
        {
            this.InitializeComponent();

            id_categorie.Text = _id_categorie.ToString();
            type.Text = _type;
            cout_organisation.Text = _cout_organisation.ToString();
            prix_vente.Text = _prix.ToString();

        }



        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            
            Id_Categorie = Convert.ToInt32(id_categorie.Text);
            Type = type.Text;
            Cout_Organisation = Convert.ToDouble(cout_organisation.Text);
            Prix_Vente = Convert.ToDouble(prix_vente.Text);


            int nombre;
            double nombre2;

            if ( Int32.TryParse(Id_Categorie.ToString(), out nombre) == false || string.IsNullOrEmpty(Type) || double.TryParse(Cout_Organisation.ToString(), out nombre2) == false || double.TryParse(Prix_Vente.ToString(), out nombre2) == false)
            {
                args.Cancel = true;
                Title = "Tous les champs doivent être remplis";
            }
        }
    }
}
