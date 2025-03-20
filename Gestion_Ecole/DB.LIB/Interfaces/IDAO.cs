using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.LIB.Interfaces
{
    public interface IDAO<T>
    {
        int insert(T o);
        int update(T o);
        int delete(object id);
        T findById(object id);
        List<T> findAll();
        List<T> find(T criteria);
    }
}
