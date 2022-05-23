namespace FarmlyCore.Infrastructure.Queries
{
    public interface IQueryHandler<U, T>
    {
        Task<T> HandleAsync(U queryModel, CancellationToken cancellationToken = default);        
    }

    public interface IQueryHandler<T>
    {
        Task<T> HandleAsync(CancellationToken cancellationToken = default);
    }
}
