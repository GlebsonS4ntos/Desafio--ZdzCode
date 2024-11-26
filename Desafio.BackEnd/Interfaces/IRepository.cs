namespace Desafio.BackEnd.Interfaces
{
    public interface IRepository<T> where T : class
    {
        public Task<List<T>> GetAllAsync();
        public Task<T> GetByIdAsync(long id);
        public Task DeleteAsync(long id);
        public void Update(T entity);
        public Task<T> CreateAsync(T entity);
    }
}
