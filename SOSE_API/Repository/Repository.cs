using Microsoft.EntityFrameworkCore;
using SOSE_API.Data;
using SOSE_API.Interface;
using System.Linq.Expressions;

namespace SOSE_API.Repository
{
    public class Repository<T>: IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _entities;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _entities = _context.Set<T>();
        }

        public IEnumerable<T> GetAll(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _entities;

            // Apply eager loading for the related entities
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.ToList();
        }

        // Modified GetById method to support Include
        public T GetById(int id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _entities;

            // Apply eager loading for the related entities
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.FirstOrDefault(e => EF.Property<int>(e, "Id") == id);
        }


        public T GetByIdStr(string id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _entities;

            // Apply eager loading for the related entities
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.FirstOrDefault(e => EF.Property<string>(e, "Id") == id);
        }

        //public IEnumerable<T> GetAll()
        //{
        //    return _entities.ToList();
        //}

        //public T GetById(int id)
        //{
        //    return _entities.Find(id);
        //}

        public void Insert(T entity)
        {
            _entities.Add(entity);
        }

        public void Update(T entity)
        {
            _entities.Update(entity);
        }

        public void Delete(int id)
        {
            var entity = _entities.Find(id);
            if (entity != null)
            {
                _entities.Remove(entity);
            }
        }

        public void DeleteStr(string id)
        {
            var entity = _entities.Find(id);
            if (entity != null)
            {
                _entities.Remove(entity);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public IEnumerable< T > Find(Expression<Func<T, bool>> predicate)
        {
            return _entities.Where(predicate).ToList(); // Allows flexible querying
        }
    }
}
