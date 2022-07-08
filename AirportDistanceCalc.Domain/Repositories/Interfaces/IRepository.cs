namespace AirportDistanceCalc.Domain.Repositories.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        Task Add(TEntity obj);
        Task<TEntity?> GetById(Guid id);
        Task<List<TEntity>> GetAll();
        Task<int> SaveChanges();
    }
}
