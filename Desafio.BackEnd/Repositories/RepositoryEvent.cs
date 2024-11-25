using Desafio.BackEnd.Context;
using Desafio.BackEnd.Entities;
using Desafio.BackEnd.Interfaces;

namespace Desafio.BackEnd.Repositories
{
    public class RepositoryEvent : Repository<Event>, IRepositoryEvent
    {
        public RepositoryEvent(DataContext context) : base(context)
        {
        }
    }
}
