using System.Linq.Expressions;

namespace SOSE_API.Interface
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(params Expression<Func<T, object>>[] includes);
        T GetById(int id, params Expression<Func<T, object>>[] includes);
        T GetByIdStr(string id, params Expression<Func<T, object>>[] includes);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        void Insert(T entity);
        void Update(T entity);
        void Delete(int id);
        void DeleteStr(string id);
        void Save();
    }
}
