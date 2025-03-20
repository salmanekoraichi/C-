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
    /// DAO pour la gestion des moyennes.
    /// </summary>
    public class DAOMoyenne : DAO<Moyenne>
    {
        public override int insert(Moyenne m)
        {
            Connect();
            string sql = "INSERT INTO Moyennes (code_eleve, code_fil, niveau, moyenne) VALUES (@code_eleve, @code_fil, @niveau, @moyenne)";
            var parameters = new Dictionary<string, object>
            {
                { "@code_eleve", m.Code_Eleve },
                { "@code_fil", m.Code_Fil },
                { "@niveau", m.Niveau },
                { "@moyenne", m.MoyenneValue }
            };
            int result = iud(sql, parameters);
            Dispose();
            return result;
        }

        public override int update(Moyenne m)
        {
            Connect();
            string sql = "UPDATE Moyennes SET moyenne=@moyenne WHERE code_eleve=@code_eleve AND niveau=@niveau";
            var parameters = new Dictionary<string, object>
            {
                { "@code_eleve", m.Code_Eleve },
                { "@niveau", m.Niveau },
                { "@moyenne", m.MoyenneValue }
            };
            int result = iud(sql, parameters);
            Dispose();
            return result;
        }

        public override int delete(object id)
        {
            // Ici, on supprime par clé primaire (id)
            Connect();
            string sql = "DELETE FROM Moyennes WHERE id=@id";
            var parameters = new Dictionary<string, object>
            {
                { "@id", id }
            };
            int result = iud(sql, parameters);
            Dispose();
            return result;
        }

        public override Moyenne findById(object id)
        {
            Connect();
            string sql = "SELECT * FROM Moyennes WHERE id=@id";
            var parameters = new Dictionary<string, object>
            {
                { "@id", id }
            };
            Moyenne m = null;
            using (IDataReader reader = select(sql, parameters))
            {
                if (reader.Read())
                {
                    m = new Moyenne
                    {
                        Id = reader.GetInt32("id"),
                        Code_Eleve = reader.GetString("code_eleve"),
                        Code_Fil = reader.GetString("code_fil"),
                        Niveau = reader.GetString("niveau"),
                        MoyenneValue = reader.GetFloat("moyenne")
                    };
                }
                reader.Close();
            }
            Dispose();
            return m;
        }

        public override List<Moyenne> findAll()
        {
            Connect();
            string sql = "SELECT * FROM Moyennes";
            var list = new List<Moyenne>();
            using (IDataReader reader = select(sql))
            {
                while (reader.Read())
                {
                    Moyenne m = new Moyenne
                    {
                        Id = reader.GetInt32("id"),
                        Code_Eleve = reader.GetString("code_eleve"),
                        Code_Fil = reader.GetString("code_fil"),
                        Niveau = reader.GetString("niveau"),
                        MoyenneValue = reader.GetFloat("moyenne")
                    };
                    list.Add(m);
                }
                reader.Close();
            }
            Dispose();
            return list;
        }

        public override List<Moyenne> find(Moyenne criteria)
        {
            Connect();
            string sql = "SELECT * FROM Moyennes WHERE 1=1";
            var parameters = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(criteria.Code_Eleve))
            {
                sql += " AND code_eleve=@code_eleve";
                parameters["@code_eleve"] = criteria.Code_Eleve;
            }
            if (!string.IsNullOrEmpty(criteria.Code_Fil))
            {
                sql += " AND code_fil=@code_fil";
                parameters["@code_fil"] = criteria.Code_Fil;
            }
            if (!string.IsNullOrEmpty(criteria.Niveau))
            {
                sql += " AND niveau=@niveau";
                parameters["@niveau"] = criteria.Niveau;
            }
            if (criteria.MoyenneValue != 0)
            {
                sql += " AND moyenne=@moyenne";
                parameters["@moyenne"] = criteria.MoyenneValue;
            }

            var list = new List<Moyenne>();
            using (IDataReader reader = select(sql, parameters))
            {
                while (reader.Read())
                {
                    Moyenne m = new Moyenne
                    {
                        Id = reader.GetInt32("id"),
                        Code_Eleve = reader.GetString("code_eleve"),
                        Code_Fil = reader.GetString("code_fil"),
                        Niveau = reader.GetString("niveau"),
                        MoyenneValue = reader.GetFloat("moyenne")
                    };
                    list.Add(m);
                }
                reader.Close();
            }
            Dispose();
            return list;
        }
    }
}
