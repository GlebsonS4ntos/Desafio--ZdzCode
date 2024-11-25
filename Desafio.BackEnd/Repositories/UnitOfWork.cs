using Desafio.BackEnd.Context;
using Desafio.BackEnd.Interfaces;

namespace Desafio.BackEnd.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        private IRepositoryEvent? repositoryEvent;
        private IRepositoryPanelist? repositoryPanelist;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public IRepositoryEvent RepositoryEvent { 
            get 
            { 
                return repositoryEvent = repositoryEvent ?? new RepositoryEvent(_context);
            } 
        }

        public IRepositoryPanelist RepositoryPanelist
        {
            get
            {
                return repositoryPanelist = repositoryPanelist ?? new RepositoryPanelist(_context);
            }

        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
