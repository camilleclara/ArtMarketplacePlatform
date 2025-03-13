namespace backend_app.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> Insert(TEntity entity);
        Task<TEntity> Update(int id, TEntity entity);
        Task<IEnumerable<TEntity>> GetAll();

        Task<TEntity> GetById(int id);

        Task<int> DeleteById(int id);
        void Delete(TEntity entity);

    }
}
