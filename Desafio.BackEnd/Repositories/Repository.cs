using Desafio.BackEnd.Context;
using Desafio.BackEnd.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Desafio.BackEnd.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DataContext _context;

        public Repository(DataContext context) 
        { 
            _context = context;
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);

            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var t = await _context.Set<T>().FindAsync(id);
            _context.Set<T>().Remove(t);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
