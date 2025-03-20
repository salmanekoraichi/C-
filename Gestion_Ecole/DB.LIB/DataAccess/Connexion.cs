using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using global::DB.LIB.Interfaces;

namespace DB.LIB.DataAccess
{
    public class Connexion : IConnexion
    {
        private MySqlConnection cnx;
        private MySqlCommand cmd;

        // Chaine de connexion MySQL (à adapter à votre environnement)
        private string connectionString =
            "Server=localhost;Database=ensat;Uid=root;Pwd=;";

        public Connexion()
        {
            // Ici, on pourrait charger la chaine de connexion 
            // depuis un fichier de config ou de l’environnement
        }

        public void Connect()
        {
            cnx = new MySqlConnection(connectionString);
            cnx.Open();
            cmd = cnx.CreateCommand();
        }

        public int iud(string sql, Dictionary<string, object> parameters = null)
        {
            cmd.CommandText = sql;
            cmd.Parameters.Clear();

            if (parameters != null)
            {
                foreach (var p in parameters)
                {
                    cmd.Parameters.AddWithValue(p.Key, p.Value);
                }
            }

            return cmd.ExecuteNonQuery();
        }

        public IDataReader select(string sql, Dictionary<string, object> parameters = null)
        {
            cmd.CommandText = sql;
            cmd.Parameters.Clear();

            if (parameters != null)
            {
                foreach (var p in parameters)
                {
                    cmd.Parameters.AddWithValue(p.Key, p.Value);
                }
            }

            return cmd.ExecuteReader();
        }

        public void Dispose()
        {
            if (cmd != null)
                cmd.Dispose();
            if (cnx != null && cnx.State == ConnectionState.Open)
                cnx.Close();
        }
    }
}
