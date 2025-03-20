using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Data;
using DB.LIB.Interfaces;
using DB.LIB.Models;

namespace DB.LIB.DataAccess
{
    /// <summary>
    /// DAO spécifique pour la gestion des filières.
    /// </summary>
    public class DAOFiliere : DAO<Filiere>
    {
        public override int insert(Filiere f)
        {
            Connect();
            string sql = "INSERT INTO Filiere (code, designation) VALUES (@code, @designation)";
            var parameters = new Dictionary<string, object>
            {
                { "@code", f.Code },
                { "@designation", f.Designation }
            };
            int result = iud(sql, parameters);
            Dispose();
            return result;
        }

        public override int update(Filiere f)
        {
            Connect();
            string sql = "UPDATE Filiere SET designation=@designation WHERE code=@code";
            var parameters = new Dictionary<string, object>
            {
                { "@code", f.Code },
                { "@designation", f.Designation }
            };
            int result = iud(sql, parameters);
            Dispose();
            return result;
        }

        public override int delete(object id)
        {
            Connect();
            string sql = "DELETE FROM Filiere WHERE code=@code";
            var parameters = new Dictionary<string, object>
            {
                { "@code", id }
            };
            int result = iud(sql, parameters);
            Dispose();
            return result;
        }

        public override Filiere findById(object id)
        {
            Connect();
            string sql = "SELECT * FROM Filiere WHERE code=@code";
            var parameters = new Dictionary<string, object>
            {
                { "@code", id }
            };
            Filiere f = null;
            using (IDataReader reader = select(sql, parameters))
            {
                if (reader.Read())
                {
                    f = new Filiere
                    {
                        Id = reader.GetInt32("id"),
                        Code = reader.GetString("code"),
                        Designation = reader.GetString("designation")
                    };
                }
                reader.Close();
            }
            Dispose();
            return f;
        }

        public override List<Filiere> findAll()
        {
            Connect();
            string sql = "SELECT * FROM Filiere";
            List<Filiere> list = new List<Filiere>();
            using (IDataReader reader = select(sql))
            {
                while (reader.Read())
                {
                    Filiere f = new Filiere
                    {
                        Id = reader.GetInt32("id"),
                        Code = reader.GetString("code"),
                        Designation = reader.GetString("designation")
                    };
                    list.Add(f);
                }
                reader.Close();
            }
            Dispose();
            return list;
        }

        public override List<Filiere> find(Filiere criteria)
        {
            Connect();
            string sql = "SELECT * FROM Filiere WHERE 1=1";
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

            List<Filiere> list = new List<Filiere>();
            using (IDataReader reader = select(sql, parameters))
            {
                while (reader.Read())
                {
                    Filiere f = new Filiere
                    {
                        Id = reader.GetInt32("id"),
                        Code = reader.GetString("code"),
                        Designation = reader.GetString("designation")
                    };
                    list.Add(f);
                }
                reader.Close();
            }
            Dispose();
            return list;
        }
    }
}
