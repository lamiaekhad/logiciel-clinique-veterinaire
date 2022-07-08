using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace clinique_vete.cliniquevt
{
    internal class Animals
    {
        private int id;
        private string Typeanimal;
        private string Nomanimal;
        private int Ageanimal;
        private decimal Poidanimal;
        private string Couleuranimal;
        private string Propanimal;
        

        public int ID { get => id; set => id = value; }
        public string typeanimal { get => Typeanimal; set => Typeanimal = value; }
        public string nomanimal { get => Nomanimal; set => Nomanimal = value; }
        public int ageanimal { get => Ageanimal; set => Ageanimal = value; }
        public string couleuranimal { get => Couleuranimal; set => Couleuranimal = value; }
        public string propanimal { get => Propanimal; set => Propanimal = value; }
        public decimal poidanimal { get => Poidanimal; set => Poidanimal = value; }

        public Animals()
        {

        }
       
       public Animals(int id, string typeanimal, string nomanimal, int ageanimal, decimal poidanimal, string couleuranimal, string propanimal)
        {
            this.ID = id;
            this.typeanimal = typeanimal;
            this.nomanimal = nomanimal;
            this.ageanimal = ageanimal;
            this.poidanimal = poidanimal;
            this.couleuranimal = couleuranimal;
            this.propanimal = propanimal;
        }
       

    }
}
