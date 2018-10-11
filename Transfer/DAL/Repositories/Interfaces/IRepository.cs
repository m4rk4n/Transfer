using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transfer.DAL.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        T GetById(object id);

        IEnumerable<T> GetAll();

        void Add(T entity);

        void Delete(T entityToDelete);

        T Update(T entity, object id);
    }
}
