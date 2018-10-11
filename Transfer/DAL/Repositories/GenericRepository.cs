using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transfer.DAL.Repositories.Interfaces;

namespace Transfer.DAL.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        protected TransferContext Context;
        internal readonly DbSet<T> DbSet;

        public GenericRepository(TransferContext context)
        {
            Context = context ?? throw new ApplicationException("DbContext cannot be null.");
            this.DbSet = Context.Set<T>();
        }

        public T GetById(object id)
        {
            return DbSet.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return this.DbSet.ToList();
        }

        public void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public void Delete(T entityToDelete)
        {
            DbSet.Remove(entityToDelete);
        }

        public T Update(T entity, object id)
        {
            if (entity == null)
                return null;
            T exist = DbSet.Find(id);
            if (exist != null)
            {
                Context.Entry(exist).CurrentValues.SetValues(entity);
                // Context.SaveChanges(); // it should be done  in  UoW, but it works this way
            }
            return exist;
        }
    }
}
