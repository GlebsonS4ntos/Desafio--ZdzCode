namespace Desafio.BackEnd.Interfaces
{
    public interface IRepository<T> where T : class
    {
        public Task<List<T>> GetAllAsync();
        public Task<T> GetByIdAsync(int id);
        public Task DeleteAsync(int id);
        public void Update(T entity);
        public Task<T> CreateAsync(T entity);
    }
}
