using Desafio.BackEnd.Context;
using Desafio.BackEnd.Entities;
using Desafio.BackEnd.Interfaces;

namespace Desafio.BackEnd.Repositories
{
    public class RepositoryPanelist : Repository<Panelist>, IRepositoryPanelist
    {
        public RepositoryPanelist(DataContext context) : base(context)
        {
        }
    }
}
