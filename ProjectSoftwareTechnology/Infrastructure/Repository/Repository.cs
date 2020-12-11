using Domain.Entities.Common;
using Domain.Repositories;
using Infrastructure.EFContext;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : class, IAggregateRoot
    {
        private readonly WebAppDBContext context;
        public Repository(WebAppDBContext context)
        {
            this.context = context;
        }
        public void Add(T entity)
        {
            context.Set<T>().Add(entity);
            context.SaveChanges();
        }

        public void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
            context.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return context.Set<T>().ToList();
        }

        public T GetBy(int id)
        {
            return context.Set<T>().Find(id);
        }

        public void RemoveAll(IEnumerable<T> entity)
        {
            context.Set<T>().RemoveRange(entity);
            context.SaveChanges();
        }

        public void Update(T entity)
        {
            context.Set<T>().Update(entity);
            context.SaveChanges();
        }
    }
}
