using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Org.BouncyCastle.Utilities.IO;
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
    public sealed partial class controleUtilisateur : ContentDialog
    {
        public string Matricule { get; set; }
        public string Mdp { get; set; }

        bool fermer = false;

        public controleUtilisateur()
        {
            this.InitializeComponent();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Matricule = matricule.Text;
            Mdp = pwd_user.Password;

            


            if (string.IsNullOrEmpty(Matricule) || string.IsNullOrEmpty(Mdp))
            {
                args.Cancel = true;
                Title = "Tous les champs doivent être remplis";
            }
        }

    }
}
