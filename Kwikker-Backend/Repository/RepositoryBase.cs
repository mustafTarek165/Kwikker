using Microsoft.EntityFrameworkCore;
using Repository.Contracts.Contracts;
using System.Linq.Expressions;


namespace Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected RepositoryContext RepositoryContext;
        public RepositoryBase(RepositoryContext repositoryContext)
        => RepositoryContext = repositoryContext;

        public void Create(T entity) => RepositoryContext.Set<T>().Add(entity);
        public void Delete(T entity) => RepositoryContext.Set<T>().Remove(entity);
        public void Update(T entity) => RepositoryContext.Set<T>().Update(entity);
        public IQueryable<T> FindAll(bool trackChanges)
           => !trackChanges ? RepositoryContext.Set<T>().AsNoTracking() : RepositoryContext.Set<T>();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> condition, bool trackChanges)
        => !trackChanges ? RepositoryContext.Set<T>().AsNoTracking().Where(condition): RepositoryContext.Set<T>().Where(condition);

        public async Task<int> GetCount() => await RepositoryContext.Set<T>().CountAsync();
        public RepositoryContext GetRepository() => RepositoryContext;

        
    }
    
    
}
