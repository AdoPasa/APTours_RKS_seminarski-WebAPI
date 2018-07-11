using System.Collections.Generic;

namespace DataAccess.Repositories
{
    public interface IRepository<TEntity, TPk> where TEntity : class
    {
        bool Add(TEntity item);
        bool Remove(TEntity item);
        bool Edit(TEntity item);
        TEntity FindByID(TPk id);
        IEnumerable<TEntity> FindAll();
    }
}
