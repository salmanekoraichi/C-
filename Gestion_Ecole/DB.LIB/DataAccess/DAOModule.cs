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
    /// DAO spécifique pour la gestion des modules.
    /// </summary>
    public class DAOModule : DAO<Module>
    {
        public override int insert(Module m)
        {
            Connect();
            string sql = @"INSERT INTO Module (code, designation, niveau, semestre, code_fil)
                           VALUES (@code, @designation, @niveau, @semestre, @code_fil)";
            var parameters = new Dictionary<string, object>
            {
                { "@code", m.Code },
                { "@designation", m.Designation },
                { "@niveau", m.Niveau },
                { "@semestre", m.Semestre },
                { "@code_fil", m.Code_Fil }
            };
            int result = iud(sql, parameters);
            Dispose();
            return result;
        }

        public override int update(Module m)
        {
            Connect();
            string sql = @"UPDATE Module SET designation=@designation, niveau=@niveau, semestre=@semestre, code_fil=@code_fil
                           WHERE code=@code";
            var parameters = new Dictionary<string, object>
            {
                { "@code", m.Code },
                { "@designation", m.Designation },
                { "@niveau", m.Niveau },
                { "@semestre", m.Semestre },
                { "@code_fil", m.Code_Fil }
            };
            int result = iud(sql, parameters);
            Dispose();
            return result;
        }

        public override int delete(object id)
        {
            Connect();
            string sql = "DELETE FROM Module WHERE code=@code";
            var parameters = new Dictionary<string, object>
            {
                { "@code", id }
            };
            int result = iud(sql, parameters);
            Dispose();
            return result;
        }

        public override Module findById(object id)
        {
            Connect();
            string sql = "SELECT * FROM Module WHERE code=@code";
            var parameters = new Dictionary<string, object>
            {
                { "@code", id }
            };
            Module m = null;
            using (IDataReader reader = select(sql, parameters))
            {
                if (reader.Read())
                {
                    m = new Module
                    {
                        Id = reader.GetInt32("id"),
                        Code = reader.GetString("code"),
                        Designation = reader.GetString("designation"),
                        Niveau = reader.GetString("niveau"),
                        Semestre = reader.GetInt32("semestre"),
                        Code_Fil = reader.GetString("code_fil")
                    };
                }
                reader.Close();
            }
            Dispose();
            return m;
        }

        public override List<Module> findAll()
        {
            Connect();
            string sql = "SELECT * FROM Module";
            List<Module> list = new List<Module>();
            using (IDataReader reader = select(sql))
            {
                while (reader.Read())
                {
                    Module m = new Module
                    {
                        Id = reader.GetInt32("id"),
                        Code = reader.GetString("code"),
                        Designation = reader.GetString("designation"),
                        Niveau = reader.GetString("niveau"),
                        Semestre = reader.GetInt32("semestre"),
                        Code_Fil = reader.GetString("code_fil")
                    };
                    list.Add(m);
                }
                reader.Close();
            }
            Dispose();
            return list;
        }

        public override List<Module> find(Module criteria)
        {
            Connect();
            string sql = "SELECT * FROM Module WHERE 1=1";
            var parameters = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(criteria.Code))
            {
                sql += " AND code=@code";
                parameters["@code"] = criteria.Code;
            }
            if (!string.IsNullOrEmpty(criteria.Designation))
            {
                sql += " AND designation LIKE @designation";
                parameters["@designation"] = "%" + criteria.Designation + "%";
            }
            if (!string.IsNullOrEmpty(criteria.Niveau))
            {
                sql += " AND niveau=@niveau";
                parameters["@niveau"] = criteria.Niveau;
            }
            if (criteria.Semestre != 0)
            {
                sql += " AND semestre=@semestre";
                parameters["@semestre"] = criteria.Semestre;
            }
            if (!string.IsNullOrEmpty(criteria.Code_Fil))
            {
                sql += " AND code_fil=@code_fil";
                parameters["@code_fil"] = criteria.Code_Fil;
            }

            List<Module> list = new List<Module>();
            using (IDataReader reader = select(sql, parameters))
            {
                while (reader.Read())
                {
                    Module m = new Module
                    {
                        Id = reader.GetInt32("id"),
                        Code = reader.GetString("code"),
                        Designation = reader.GetString("designation"),
                        Niveau = reader.GetString("niveau"),
                        Semestre = reader.GetInt32("semestre"),
                        Code_Fil = reader.GetString("code_fil")
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
