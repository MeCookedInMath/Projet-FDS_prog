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


        private void connexion_admin_Click(object sender, RoutedEventArgs e)
        {

            
            string matricule_admin = txtbx_matricule_admin.Text.Trim();
            int id = Convert.ToInt32(matricule_admin);
            string mdp = txtbx_mdp_admin.Text;

            Singleton.getInstance().getConnectionAdmin(id, mdp);

            if (Singleton.getInstance().IsSetConnection() && Singleton.getInstance().IsSetRole() == "admin")
            {
                validation_connexion1.Text = "Vous êtes bien connecté en tant qu'administrateur.";
            }
            else {
                
                validation_connexion1.Text = "La connexion n'a pas fonctionné.";
            }
            
        }

        private void connexion_adherent_Click(object sender, RoutedEventArgs e)
        {
            string matricule_adherent = txtbx_matricule_adherent.Text.Trim();
            
            string mdp = txtbx_mdp_adherent.Text;

            Singleton.getInstance().getConnectionAdherent(matricule_adherent, mdp);

            if (Singleton.getInstance().IsSetConnection() && Singleton.getInstance().IsSetRole() == "adherent")
            {
                validation_connexion2.Text = $"Vous êtes bien connecté en tant que {mdp}";
            }
            else
            {
                validation_connexion2.Text = "La connexion n'a pas fonctionné.";
            }

        }
    }
}
