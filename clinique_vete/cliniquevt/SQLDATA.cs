using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinique_vete.cliniquevt
{
    internal class SQLDATA
    {
        public MySqlConnection connectTobase()
        {
            MySqlConnection cnn;
            string connectionString = "server=localhost;database=clinique_vt;uid=root;pwd=;";
            cnn = new MySqlConnection(connectionString);

            try
            {
                cnn.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine("impossible d'ouvrir la connexion" + ex.Message);
            }
            return cnn;
        }
    }
}
