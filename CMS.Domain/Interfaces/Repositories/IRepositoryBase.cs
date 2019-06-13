using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Domain.Interfaces.Repositories
{
    public interface IRepositoryBase<TEntity>
    {
        void Add(TEntity obj);
        TEntity GetById(object id);
        Task<TEntity> GetByIdAsync(object id);
        IEnumerable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync();
        IQueryable<TEntity> GetAllPaging();
        void Update(TEntity obj);
        void Remove(object id, bool IsAutoSaveChange = true);
        void SaveChanges();
        void Dispose();
    }
}
