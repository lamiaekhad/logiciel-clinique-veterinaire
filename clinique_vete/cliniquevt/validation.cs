using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace clinique_vete.cliniquevt
{
    internal class validation
    {
        sqldata mysqldata = new sqldata();
        
        public bool validationString(string nom)
        {
            bool valide = false;
            if (!Regex.Match(nom, "^[a-zA-Z]*$").Success)
            {
                valide = true;
                Console.WriteLine("Le choix n'est pas valide...");
            }
            return valide;
        }

       public bool ValidationAge(int inputint)
        {
            const int ageMax = 100;
            bool valide = false;
            if (!(inputint > 0 && inputint < ageMax))
            {
                valide = true;
                Console.WriteLine("Le choix n'est pas valide...");
            }
            return valide;
        }
        public bool validationIdExist(int IDamodifier)  //condition si le id exist
        {
            int exist = 0;
            bool valide = false;
            MySqlConnection conn1 = mysqldata.connectTobase();
            try
            {
                MySqlCommand command2 = new MySqlCommand("select count(*) exist from animal where id=@id2", conn1);
                command2.Parameters.AddWithValue("@id2", IDamodifier);
                command2.Parameters.AddWithValue("exist", exist);
                using (MySqlDataReader reader = command2.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            exist = reader.GetInt32("exist");
                        }
                    }
                }
                if (exist == 0)
                {
                    valide = true;
                    Console.WriteLine("Votre choix n'existe pas");
                }
                command2.ExecuteReader();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Requete impossible" + ex.Message);
            }
            finally
            {
                conn1.Close();
            }

            conn1.Close();
            return valide;
        }

    }
}
