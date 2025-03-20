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
    /// DAO spécifique pour la gestion des matières.
    /// </summary>
    public class DAOMatiere : DAO<Matiere>
    {
        public override int insert(Matiere m)
        {
            Connect();
            string sql = "INSERT INTO Matiere (code, designation, VH, code_module) VALUES (@code, @designation, @VH, @code_module)";
            var parameters = new Dictionary<string, object>
            {
                { "@code", m.Code },
                { "@designation", m.Designation },
                { "@VH", m.VH },
                { "@code_module", m.Code_Module }
            };
            int result = iud(sql, parameters);
            Dispose();
            return result;
        }

        public override int update(Matiere m)
        {
            Connect();
            string sql = "UPDATE Matiere SET designation=@designation, VH=@VH, code_module=@code_module WHERE code=@code";
            var parameters = new Dictionary<string, object>
            {
                { "@code", m.Code },
                { "@designation", m.Designation },
                { "@VH", m.VH },
                { "@code_module", m.Code_Module }
            };
            int result = iud(sql, parameters);
            Dispose();
            return result;
        }

        public override int delete(object id)
        {
            Connect();
            string sql = "DELETE FROM Matiere WHERE code=@code";
            var parameters = new Dictionary<string, object>
            {
                { "@code", id }
            };
            int result = iud(sql, parameters);
            Dispose();
            return result;
        }

        public override Matiere findById(object id)
        {
            Connect();
            string sql = "SELECT * FROM Matiere WHERE code=@code";
            var parameters = new Dictionary<string, object>
            {
                { "@code", id }
            };
            Matiere m = null;
            using (IDataReader reader = select(sql, parameters))
            {
                if (reader.Read())
                {
                    m = new Matiere
                    {
                        Id = reader.GetInt32("id"),
                        Code = reader.GetString("code"),
                        Designation = reader.GetString("designation"),
                        VH = reader.GetInt32("VH"),
                        Code_Module = reader.GetString("code_module")
                    };
                }
                reader.Close();
            }
            Dispose();
            return m;
        }

        public override List<Matiere> findAll()
        {
            Connect();
            string sql = "SELECT * FROM Matiere";
            List<Matiere> list = new List<Matiere>();
            using (IDataReader reader = select(sql))
            {
                while (reader.Read())
                {
                    Matiere m = new Matiere
                    {
                        Id = reader.GetInt32("id"),
                        Code = reader.GetString("code"),
                        Designation = reader.GetString("designation"),
                        VH = reader.GetInt32("VH"),
                        Code_Module = reader.GetString("code_module")
                    };
                    list.Add(m);
                }
                reader.Close();
            }
            Dispose();
            return list;
        }

        public override List<Matiere> find(Matiere criteria)
        {
            Connect();
            string sql = "SELECT * FROM Matiere WHERE 1=1";
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
            if (criteria.VH != 0)
            {
                sql += " AND VH=@VH";
                parameters["@VH"] = criteria.VH;
            }
            if (!string.IsNullOrEmpty(criteria.Code_Module))
            {
                sql += " AND code_module=@code_module";
                parameters["@code_module"] = criteria.Code_Module;
            }

            List<Matiere> list = new List<Matiere>();
            using (IDataReader reader = select(sql, parameters))
            {
                while (reader.Read())
                {
                    Matiere m = new Matiere
                    {
                        Id = reader.GetInt32("id"),
                        Code = reader.GetString("code"),
                        Designation = reader.GetString("designation"),
                        VH = reader.GetInt32("VH"),
                        Code_Module = reader.GetString("code_module")
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
