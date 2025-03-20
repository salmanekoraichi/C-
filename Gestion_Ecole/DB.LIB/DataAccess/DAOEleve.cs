using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using DB.LIB.DataAccess;
using DB.LIB.Models;
using System;
using System.Collections.Generic;
using System.Data;
using DB.LIB.Models;
using DB.LIB.Interfaces;

namespace DB.LIB.DataAccess
{
    /// <summary>
    /// DAO spécifique pour la gestion des élèves.
    /// </summary>
    public class DAOEleve : DAO<Eleve>
    {
        public override int insert(Eleve e)
        {
            Connect();
            string sql = @"INSERT INTO Eleve (code, nom, prenom, niveau, code_fil)
                           VALUES (@code, @nom, @prenom, @niveau, @code_fil)";
            var parameters = new Dictionary<string, object>()
            {
                { "@code", e.Code },
                { "@nom", e.Nom },
                { "@prenom", e.Prenom },
                { "@niveau", e.Niveau },
                { "@code_fil", e.Code_Fil }
            };
            int result = iud(sql, parameters);
            Dispose();
            return result;
        }

        public override int update(Eleve e)
        {
            Connect();
            string sql = @"UPDATE Eleve 
                           SET nom=@nom, prenom=@prenom, niveau=@niveau, code_fil=@code_fil
                           WHERE code=@code";
            var parameters = new Dictionary<string, object>()
            {
                { "@code", e.Code },
                { "@nom", e.Nom },
                { "@prenom", e.Prenom },
                { "@niveau", e.Niveau },
                { "@code_fil", e.Code_Fil }
            };
            int result = iud(sql, parameters);
            Dispose();
            return result;
        }

        public override int delete(object id)
        {
            Connect();
            string sql = "DELETE FROM Eleve WHERE code=@code";
            var parameters = new Dictionary<string, object>()
            {
                { "@code", id }
            };
            int result = iud(sql, parameters);
            Dispose();
            return result;
        }

        public override Eleve findById(object id)
        {
            Connect();
            string sql = "SELECT * FROM Eleve WHERE code=@code";
            var parameters = new Dictionary<string, object>()
            {
                { "@code", id }
            };
            Eleve e = null;
            using (IDataReader reader = select(sql, parameters))
            {
                if (reader.Read())
                {
                    e = new Eleve
                    {
                        Id = reader.GetInt32("id"),
                        Code = reader.GetString("code"),
                        Nom = reader.GetString("nom"),
                        Prenom = reader.GetString("prenom"),
                        Niveau = reader.GetString("niveau"),
                        Code_Fil = reader.GetString("code_fil")
                    };
                }
                reader.Close();
            }
            Dispose();
            return e;
        }

        public override List<Eleve> findAll()
        {
            Connect();
            string sql = "SELECT * FROM Eleve";
            var list = new List<Eleve>();
            using (IDataReader reader = select(sql))
            {
                while (reader.Read())
                {
                    Eleve e = new Eleve
                    {
                        Id = reader.GetInt32("id"),
                        Code = reader.GetString("code"),
                        Nom = reader.GetString("nom"),
                        Prenom = reader.GetString("prenom"),
                        Niveau = reader.GetString("niveau"),
                        Code_Fil = reader.GetString("code_fil")
                    };
                    list.Add(e);
                }
                reader.Close();
            }
            Dispose();
            return list;
        }

        public override List<Eleve> find(Eleve criteria)
        {
            Connect();
            string sql = "SELECT * FROM Eleve WHERE 1=1";
            var parameters = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(criteria.Code))
            {
                sql += " AND code=@code";
                parameters["@code"] = criteria.Code;
            }
            if (!string.IsNullOrEmpty(criteria.Nom))
            {
                sql += " AND nom LIKE @nom";
                parameters["@nom"] = "%" + criteria.Nom + "%";
            }
            if (!string.IsNullOrEmpty(criteria.Prenom))
            {
                sql += " AND prenom LIKE @prenom";
                parameters["@prenom"] = "%" + criteria.Prenom + "%";
            }
            if (!string.IsNullOrEmpty(criteria.Niveau))
            {
                sql += " AND niveau=@niveau";
                parameters["@niveau"] = criteria.Niveau;
            }
            if (!string.IsNullOrEmpty(criteria.Code_Fil))
            {
                sql += " AND code_fil=@code_fil";
                parameters["@code_fil"] = criteria.Code_Fil;
            }
            var list = new List<Eleve>();
            using (IDataReader reader = select(sql, parameters))
            {
                while (reader.Read())
                {
                    Eleve e = new Eleve
                    {
                        Id = reader.GetInt32("id"),
                        Code = reader.GetString("code"),
                        Nom = reader.GetString("nom"),
                        Prenom = reader.GetString("prenom"),
                        Niveau = reader.GetString("niveau"),
                        Code_Fil = reader.GetString("code_fil")
                    };
                    list.Add(e);
                }
                reader.Close();
            }
            Dispose();
            return list;
        }
    }
}
