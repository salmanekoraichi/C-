using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Data;
using DB.LIB.Models;

namespace DB.LIB.DataAccess
{
    /// <summary>
    /// DAO spécifique pour la gestion des notes.
    /// </summary>
    public class DAONotes : DAO<Notes>
    {
        public override int insert(Notes n)
        {
            Connect();
            string sql = @"INSERT INTO Notes (code_eleve, code_mat, note)
                           VALUES (@code_eleve, @code_mat, @note)";
            var parameters = new Dictionary<string, object>()
            {
                { "@code_eleve", n.Code_Eleve },
                { "@code_mat", n.Code_Matiere },
                { "@note", n.Note }
            };
            int result = iud(sql, parameters);
            Dispose();
            return result;
        }

        public override int update(Notes n)
        {
            Connect();
            string sql = @"UPDATE Notes 
                           SET code_eleve=@code_eleve, code_mat=@code_mat, note=@note
                           WHERE id=@id";
            var parameters = new Dictionary<string, object>()
            {
                { "@id", n.Id },
                { "@code_eleve", n.Code_Eleve },
                { "@code_mat", n.Code_Matiere },
                { "@note", n.Note }
            };
            int result = iud(sql, parameters);
            Dispose();
            return result;
        }

        public override int delete(object id)
        {
            Connect();
            string sql = "DELETE FROM Notes WHERE id=@id";
            var parameters = new Dictionary<string, object>()
            {
                { "@id", id }
            };
            int result = iud(sql, parameters);
            Dispose();
            return result;
        }

        public override Notes findById(object id)
        {
            Connect();
            string sql = "SELECT * FROM Notes WHERE id=@id";
            var parameters = new Dictionary<string, object>()
            {
                { "@id", id }
            };
            Notes n = null;
            using (IDataReader reader = select(sql, parameters))
            {
                if (reader.Read())
                {
                    n = new Notes
                    {
                        Id = reader.GetInt32("id"),
                        Code_Eleve = reader.GetString("code_eleve"),
                        Code_Matiere = reader.GetString("code_mat"),
                        Note = reader.GetFloat("note")
                    };
                }
                reader.Close();
            }
            Dispose();
            return n;
        }

        public override List<Notes> findAll()
        {
            Connect();
            string sql = "SELECT * FROM Notes";
            var list = new List<Notes>();
            using (IDataReader reader = select(sql))
            {
                while (reader.Read())
                {
                    Notes n = new Notes
                    {
                        Id = reader.GetInt32("id"),
                        Code_Eleve = reader.GetString("code_eleve"),
                        Code_Matiere = reader.GetString("code_mat"),
                        Note = reader.GetFloat("note")
                    };
                    list.Add(n);
                }
                reader.Close();
            }
            Dispose();
            return list;
        }

        public override List<Notes> find(Notes criteria)
        {
            Connect();
            string sql = "SELECT * FROM Notes WHERE 1=1";
            var parameters = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(criteria.Code_Eleve))
            {
                sql += " AND code_eleve=@code_eleve";
                parameters["@code_eleve"] = criteria.Code_Eleve;
            }
            if (!string.IsNullOrEmpty(criteria.Code_Matiere))
            {
                sql += " AND code_mat=@code_mat";
                parameters["@code_mat"] = criteria.Code_Matiere;
            }
            if (criteria.Note != 0)
            {
                sql += " AND note=@note";
                parameters["@note"] = criteria.Note;
            }
            var list = new List<Notes>();
            using (IDataReader reader = select(sql, parameters))
            {
                while (reader.Read())
                {
                    Notes n = new Notes
                    {
                        Id = reader.GetInt32("id"),
                        Code_Eleve = reader.GetString("code_eleve"),
                        Code_Matiere = reader.GetString("code_mat"),
                        Note = reader.GetFloat("note")
                    };
                    list.Add(n);
                }
                reader.Close();
            }
            Dispose();
            return list;
        }
    }
}
