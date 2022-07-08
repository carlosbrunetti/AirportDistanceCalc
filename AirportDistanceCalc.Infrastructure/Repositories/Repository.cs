using AirportDistanceCalc.Domain.Repositories.Interfaces;
using AirportDistanceCalc.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace AirportDistanceCalc.Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly MemoryContext _context;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(MemoryContext context)
        {
            _context = context;
            DbSet = _context.Set<TEntity>();
        }

        public virtual async Task Add(TEntity obj)
        {
            await DbSet.AddAsync(obj);
        }
        public virtual async Task<List<TEntity>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task<TEntity?> GetById(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

    }
}
