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
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Authentification : Page
    {
        public Authentification()
        {
            this.InitializeComponent();
            
        }


        

        private void deconnect_btn_Click(object sender, RoutedEventArgs e)
        {
            Singleton.getInstance().SetConnectionFalse();

        }

        
        
        
        private async void connexion_admin_Click(object sender, RoutedEventArgs e)
        {
            controleUtilisateur dialog = new controleUtilisateur();
            dialog.XamlRoot = this.XamlRoot;
            dialog.Title = "Authentification";
            dialog.PrimaryButtonText = "Se connecter";
            dialog.CloseButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Close;

            ContentDialogResult resultat = await dialog.ShowAsync();

            if (resultat == ContentDialogResult.Primary)
            {
                int matricule = Convert.ToInt32(dialog.Matricule);
                string mdp = dialog.Mdp;


                Singleton.getInstance().getConnectionAdmin(matricule, mdp);

                if (Singleton.getInstance().IsSetConnection() && Singleton.getInstance().IsSetRole() == "admin")
                {
                   
                    Singleton.getInstance().setMessageUtilisateur("Vous êtes bien connecté en tant qu'administrateur", this);

                    
                }
                else
                {
                    Singleton.getInstance().setMessageUtilisateur("La connexion n'a pas fonctionné", this);
                    
                }
            }



        }


        private async void connexion_adherent_Click(object sender, RoutedEventArgs e)
        {
            controleUtilisateur dialog = new controleUtilisateur();
            dialog.XamlRoot = this.XamlRoot;
            dialog.Title = "Authentification";
            dialog.PrimaryButtonText = "Se connecter";
            dialog.CloseButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Close;

            ContentDialogResult resultat = await dialog.ShowAsync();

            if (resultat == ContentDialogResult.Primary)
            {
                string matricule = dialog.Matricule;
                string mdp = dialog.Mdp;


                Singleton.getInstance().getConnectionAdherent(matricule, mdp);

                if (Singleton.getInstance().IsSetConnection() && Singleton.getInstance().IsSetRole() == "adherent")
                {

                    Singleton.getInstance().setMatricule_connection(matricule);
                    Singleton.getInstance().setMessageUtilisateur("Vous êtes bien connecté en tant qu'adhérent", this);

                    
                }
                else
                {

                    Singleton.getInstance().setMessageUtilisateur("La connexion n'a pas fonctionné", this);
                }
            }



        }



      

        
    }
}
