using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Statistiques : Page
    {
        public Statistiques()
        {
            this.InitializeComponent();
            ChargerStatistiques();

            if (Singleton.getInstance().IsSetConnection() == false || Singleton.getInstance().IsSetRole() != "admin")
            {
                //mainFrame.Navigate(typeof(Affichage));
            }
        }

        private void ChargerStatistiques()
        {
            var singleton = Singleton.getInstance();


            var nombreActivites = singleton.GetNombreActivites();
            var nombreAdherents = singleton.GetNombreAdherents();
            var topSeances = singleton.GetTopSeances();
            var moyennes = singleton.GetMoyenneNote();
            var participantsParActivite = singleton.GetNombreParticipantsParActivite();
            var prixMoyens = singleton.GetPrixMoyenParActiviteParParticipant();
            var participantAvecPlusDeSeances = singleton.GetParticipantAvecPlusDeSeances();


            TextBlockActivite1.Text = $"Nombre total d'activités: {nombreActivites}";
            TextBlockActivite2.Text = $"Nombre total d'adhérents: {nombreAdherents}";

            if (topSeances.Count > 0)
            {
                TextBlockActivite4.Text = $"Activité avec le plus d'inscriptions: {topSeances[0]}";
            }


            if (participantAvecPlusDeSeances.NoIdentification != null)
            {
                TextBlockActivite7.Text = $"Participant avec le plus de séances: {participantAvecPlusDeSeances.Nom} {participantAvecPlusDeSeances.Prenom} ({participantAvecPlusDeSeances.NombreSeances} séances)";
            }
            foreach (var moyenne in moyennes)
            {
                var textBlock = new TextBlock
                {
                    Text = $"{moyenne.ActiviteNom}: Moyenne = {moyenne.Moyenne:F2}",
                    Foreground = new SolidColorBrush(Colors.Black),
                    FontSize = 16,
                    Margin = new Thickness(0, 5, 0, 5)
                };


                stckpnl_stat_5.Children.Add(textBlock);
            }
            foreach (var prix in prixMoyens)
            {

                var textBlock = new TextBlock
                {
                    Text = $"{prix.ActiviteNom}: Prix moyen = {prix.PrixMoyen:F2} $",
                    Foreground = new SolidColorBrush(Colors.Black),
                    FontSize = 16,
                    Margin = new Thickness(0, 5, 0, 5)
                };


                stckpnl_stat_6.Children.Add(textBlock);
            }
            foreach (var participant in participantsParActivite)
            {
                var textBlock = new TextBlock
                {
                    Text = $"{participant.ActiviteNom}: {participant.NombreParticipants} participants",
                    Foreground = new SolidColorBrush(Colors.Black),
                    FontSize = 16,
                    Margin = new Thickness(0, 5, 0, 5)
                };
                stckpnl_stat_3.Children.Add(textBlock);
            }
            var participantsObservable = new ObservableCollection<(string ActiviteNom, int NombreParticipants)>(participantsParActivite);

            Debug.WriteLine($"Participant avec le plus de séances: {participantAvecPlusDeSeances.Nom} {participantAvecPlusDeSeances.Prenom} - {participantAvecPlusDeSeances.NombreSeances} séances");
        }
    }
}
