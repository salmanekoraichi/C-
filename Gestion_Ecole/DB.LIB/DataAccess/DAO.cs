using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DB.LIB.DataAccess;
using DB.LIB.Interfaces;
using System.Collections.Generic;

namespace DB.LIB.DataAccess
{
    public class DAO<T> : Connexion, IDAO<T>
    {
        // Ce DAO générique peut être spécialisé ou hérité 
        // par des classes plus concrètes (DAOEleve, DAONotes, etc.)

        public virtual int insert(T o)
        {
            // A implémenter selon vos besoins
            return 0;
        }

        public virtual int update(T o)
        {
            return 0;
        }

        public virtual int delete(object id)
        {
            return 0;
        }

        public virtual T findById(object id)
        {
            return default(T);
        }

        public virtual List<T> findAll()
        {
            return new List<T>();
        }

        public virtual List<T> find(T criteria)
        {
            return new List<T>();
        }
    }
}
