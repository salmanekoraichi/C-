using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace GestionDesAbsances
{
    public static class GestionAbsences
    {
        // TODO: Update with your own connection string
        private static string connectionString = "User Id=root;Host=localhost;Database=gestionabsences";


        // Add or update absences for a given student in a given week
        public static void AjouterOuModifierAbsence(int idEleve, int numSemaine, int nbrAbsences)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                // Check if record exists
                string checkQuery = "SELECT COUNT(*) FROM Absences WHERE ID=@id AND Num_semaine=@semaine";
                using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@id", idEleve);
                    checkCmd.Parameters.AddWithValue("@semaine", numSemaine);
                    long count = (long)checkCmd.ExecuteScalar();

                    if (count > 0)
                    {
                        // Update existing
                        string updateQuery = "UPDATE Absences SET Nbr_absences=@nbr WHERE ID=@id AND Num_semaine=@semaine";
                        using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn))
                        {
                            updateCmd.Parameters.AddWithValue("@nbr", nbrAbsences);
                            updateCmd.Parameters.AddWithValue("@id", idEleve);
                            updateCmd.Parameters.AddWithValue("@semaine", numSemaine);
                            updateCmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        // Insert new
                        string insertQuery = "INSERT INTO Absences (Num_semaine, ID, Nbr_absences) VALUES (@semaine, @id, @nbr)";
                        using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn))
                        {
                            insertCmd.Parameters.AddWithValue("@semaine", numSemaine);
                            insertCmd.Parameters.AddWithValue("@id", idEleve);
                            insertCmd.Parameters.AddWithValue("@nbr", nbrAbsences);
                            insertCmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        public static void SupprimerAbsence(int idEleve, int numSemaine)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Absences WHERE ID=@id AND Num_semaine=@semaine";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idEleve);
                    cmd.Parameters.AddWithValue("@semaine", numSemaine);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Return a DataTable of absences for a given student (all weeks)
        public static DataTable GetAbsencesForEleve(int idEleve)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Absences WHERE ID=@id";
                using (MySqlDataAdapter da = new MySqlDataAdapter(query, conn))
                {
                    da.SelectCommand.Parameters.AddWithValue("@id", idEleve);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        // Get absences for a specific week
        public static int GetAbsencesForWeek(int idEleve, int numSemaine)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Nbr_absences FROM Absences WHERE ID=@id AND Num_semaine=@semaine";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idEleve);
                    cmd.Parameters.AddWithValue("@semaine", numSemaine);
                    object result = cmd.ExecuteScalar();
                    return (result == null || result == DBNull.Value) ? 0 : Convert.ToInt32(result);
                }
            }
        }

        // Get total absences for a student across all weeks
        public static int GetTotalAbsences(int idEleve)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT SUM(Nbr_absences) FROM Absences WHERE ID=@id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idEleve);
                    object result = cmd.ExecuteScalar();
                    return (result == null || result == DBNull.Value) ? 0 : Convert.ToInt32(result);
                }
            }
        }
    }
}
