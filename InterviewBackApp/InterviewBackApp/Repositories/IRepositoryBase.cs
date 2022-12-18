using System.Linq.Expressions;

namespace InterviewBackApp.Repositories
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> GetByQuery();
        IQueryable<T> GetByQueryWithTracking();
        IList<T> FindAll();
        Task<IList<T>> FindAllAsync();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        IQueryable<T> FindAllByViewOrder(Expression<Func<T, int>> expression);
        T GetById(Guid id);
        Task<T> GetByIdAsync(Guid id);
        void Create(T entity);
        Task CreateAsync(T entity);
        void Update(T entity);
        Task UpdateAsync(T entity);
        void Delete(T entity);
        Task DeleteAsync(T entity);
        void DeleteRange(IEnumerable<T> entity);
        Task DeleteRangeAsync(IEnumerable<T> entity);
        Task SaveAsync();
        void Save();

    }
}
