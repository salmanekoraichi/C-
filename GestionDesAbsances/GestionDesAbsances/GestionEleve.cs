using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace GestionDesAbsances
{
    public static class GestionEleve
    {
     
        public static void AjouterEleve(string nom, string prenom, string groupe)
        {
            using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=gestionabsences;Uid=root;Pwd=;"))
            {
                conn.Open();
                string query = "INSERT INTO Eleves (Nom, Prenom, Groupe) VALUES (@nom, @prenom, @groupe)";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nom", nom);
                    cmd.Parameters.AddWithValue("@prenom", prenom);
                    cmd.Parameters.AddWithValue("@groupe", groupe);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void ModifierEleve(int id, string nom, string prenom, string groupe)
        {
            using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=gestionabsences;Uid=root;Pwd=;"))
            {
                conn.Open();
                string query = "UPDATE Eleves SET Nom=@nom, Prenom=@prenom, Groupe=@groupe WHERE ID=@id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@nom", nom);
                    cmd.Parameters.AddWithValue("@prenom", prenom);
                    cmd.Parameters.AddWithValue("@groupe", groupe);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void SupprimerEleve(int id)
        {
            using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=gestionabsences;Uid=root;Pwd=;"))
            {
                conn.Open();
                string query = "DELETE FROM Eleves WHERE ID=@id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static DataTable RechercherEleves(string nom, string prenom, string groupe)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=gestionabsences;Uid=root;Pwd=;"))
            {
                conn.Open();
                // Simple WHERE with LIKE for each field
                string query = @"SELECT * FROM Eleves
                                 WHERE Nom LIKE @nom
                                   AND Prenom LIKE @prenom
                                   AND Groupe LIKE @groupe";
                using (MySqlDataAdapter da = new MySqlDataAdapter(query, conn))
                {
                    da.SelectCommand.Parameters.AddWithValue("@nom", "%" + nom + "%");
                    da.SelectCommand.Parameters.AddWithValue("@prenom", "%" + prenom + "%");
                    da.SelectCommand.Parameters.AddWithValue("@groupe", "%" + groupe + "%");
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public static DataTable ListerEleves()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=gestionabsences;Uid=root;Pwd=;"))
            {
                conn.Open();
                string query = "SELECT * FROM Eleves";
                using (MySqlDataAdapter da = new MySqlDataAdapter(query, conn))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }
    }
}
