using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetSession_prog
{
    internal class Singleton
    {

        ObservableCollection<Object> listeActivites;
        ObservableCollection<Object> listeAdherents;
        ObservableCollection<Object> listeSeances;
        static Singleton instance = null;

        Boolean connection = false;

        MySqlConnection con = new MySqlConnection("Server=cours.cegep3r.info;Database=a2024_420335ri_eq6;Uid=2258419;Pwd=2258419;");


        
        public Singleton()
        {
            listeActivites = new ObservableCollection<Object>();
            listeAdherents = new ObservableCollection<Object>();
            listeSeances = new ObservableCollection<Object>();

        }

        public static Singleton getInstance()
        {
            if (instance == null)
                instance = new Singleton();

            return instance;
        }

        public Boolean IsSetConnection()
        {
            return connection;
        }

        public void SetConnectionTrue() { 
            connection = true;
        }

        public void SetConnectionFalse() { 
            connection = false;
        }



        }
}
