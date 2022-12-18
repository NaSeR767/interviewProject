using InterviewBackApp.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InterviewBackApp.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected BackendContext RepositoryContext { get; set; }

        public RepositoryBase(BackendContext repositoryContext)
        {
            this.RepositoryContext = repositoryContext;
        }

        public IQueryable<T> GetByQuery()
        {
            return this.RepositoryContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> GetByQueryWithTracking()
        {
            return this.RepositoryContext.Set<T>();
        }

        public async Task<IList<T>> FindAllAsync()
        {
            return await this.RepositoryContext.Set<T>().ToListAsync();
        }

        public IList<T> FindAll()
        {
            return this.RepositoryContext.Set<T>().ToList();
        }

        public IQueryable<T> FindAllByViewOrder(Expression<Func<T, int>> expression)
        {
            return this.RepositoryContext.Set<T>().OrderBy(expression).AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.RepositoryContext.Set<T>().Where(expression).AsNoTracking();
        }

        public T GetById(Guid id)
        {
            return this.RepositoryContext.Set<T>().Find(id);
        }

        public async System.Threading.Tasks.Task<T> GetByIdAsync(Guid id)
        {
            return await this.RepositoryContext.Set<T>().FindAsync(id);
        }

        public void Create(T entity)
        {
            if (entity == null)
            {
                throw new System.ArgumentNullException(paramName: nameof(entity));
            }
            this.RepositoryContext.Set<T>().Add(entity);
        }

        public async Task CreateAsync(T entity)
        {
            if (entity == null)
            {
                throw new System.ArgumentNullException(paramName: nameof(entity));
            }
            await this.RepositoryContext.Set<T>().AddAsync(entity);

        }



        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new System.ArgumentNullException(paramName: nameof(entity));
            }
            this.RepositoryContext.Set<T>().Update(entity);
        }


        public async Task UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new System.ArgumentNullException(paramName: nameof(entity));
            }

            await Task.Run(() =>
            {
                this.RepositoryContext.Set<T>().Update(entity);
            });

        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new System.ArgumentNullException(paramName: nameof(entity));
            }

            this.RepositoryContext.Set<T>().Remove(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            if (entity == null)
            {
                throw new System.ArgumentNullException(paramName: nameof(entity));
            }

            await Task.Run(() =>
            {
                this.RepositoryContext.Set<T>().Remove(entity);
            });
        }

        public void DeleteRange(IEnumerable<T> entity)
        {
            if (entity == null)
            {
                throw new System.ArgumentNullException(paramName: nameof(entity));
            }

            this.RepositoryContext.Set<T>().RemoveRange(entity);
        }

        public async Task DeleteRangeAsync(IEnumerable<T> entity)
        {
            if (entity == null)
            {
                throw new System.ArgumentNullException(paramName: nameof(entity));
            }

            await Task.Run(() =>
            {
                this.RepositoryContext.Set<T>().RemoveRange(entity);
            });
        }


        public async Task SaveAsync()
        {
            await this.RepositoryContext.SaveChangesAsync();
        }

        public void Save()
        {
            this.RepositoryContext.SaveChanges();
        }

    }
}
