using System.Linq.Expressions;

namespace RegistrationTask.Core.Application.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<List<T>> GetAll();
        Task<T> GetById(int id);
        void Add(T entity);
        void Remove(T entity);
        void AddRange(IEnumerable<T> entities); 
        Task AddRangeAsync(IEnumerable<T> entities);
        void RemoveRange(IEnumerable<T> entities);
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);

        //void Load(T entity, Expression<Func<T, object>> propertyExpression);
        //void LoadAsync(T entity, Expression<Func<T, object>> propertyExpression);
        //void LoadCollection(T entity, Expression<Func<T, IEnumerable<object>>> propertyExpression);
        //void LoadCollectionAsync(T entity, Expression<Func<T, IEnumerable<object>>> propertyExpression);

        //void Load(T entity, string propertyExpression);
    }
}
