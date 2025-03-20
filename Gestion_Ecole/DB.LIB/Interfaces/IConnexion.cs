using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.LIB.Interfaces
{
    public interface IConnexion : IDisposable
    {
        void Connect();  // Ouvre la connexion
        int iud(string sql, Dictionary<string, object> parameters = null);
        IDataReader select(string sql, Dictionary<string, object> parameters = null);
    }


}
