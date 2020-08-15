using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SUBDCORE.Repository
{
    interface ICrudable<T>
    {
        IEnumerable<T> GetList();
        void Create(T _object);
        void Update(T _object);
        void Delete(int id);
    }
}
