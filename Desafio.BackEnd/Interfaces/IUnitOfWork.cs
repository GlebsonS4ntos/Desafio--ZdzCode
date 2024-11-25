namespace Desafio.BackEnd.Interfaces
{
    public interface IUnitOfWork
    {
        IRepositoryEvent RepositoryEvent { get; }
        IRepositoryPanelist RepositoryPanelist { get; }

        Task CommitAsync();
    }
}
