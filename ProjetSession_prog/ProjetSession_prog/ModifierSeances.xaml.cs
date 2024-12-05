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

    public sealed partial class ModifierSeances : ContentDialog
    {
        public string NomActivite { get; set; }
        public string Date { get; set; }

        public string Heure { get; set; }

        public int Nbr_Places { get; set; }

        public ModifierSeances(string _nomActivite,string _date, string _heure, int _nbrPlaces)
        {
            this.InitializeComponent();

            nomActivite.Text = _nomActivite;
            date_seance.Date = Convert.ToDateTime(_date);
            heure_seance.Time = TimeSpan.Parse(_heure);
            nbr_places.Text = _nbrPlaces.ToString();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            NomActivite = nomActivite.Text;
            Date = date_seance.Date.ToString("yyyy-MM-dd");
            Heure = heure_seance.Time.ToString();
            Nbr_Places = Convert.ToInt32(nbr_places.Text);



            int nombre;
            if (string.IsNullOrEmpty(NomActivite) || string.IsNullOrEmpty(Date) || string.IsNullOrEmpty(Heure) || Int32.TryParse(Nbr_Places.ToString(), out nombre) == false)
            {
                args.Cancel = true;
                Title = "Tous les champs doivent être remplis ou avoir des valeurs au bon format";
            }
        }

        
    }
}
