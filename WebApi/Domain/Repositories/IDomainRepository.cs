namespace Domain.Repositories;


public interface IDomainRepository<T>
{
    Task AddAsync( T entity );

    Task<T?> GetByIdAsync( Guid id );

    Task<List<T>> GetAllAsync();

    Task UpdateAsync( T entity );

    Task DeleteByIdAsync( Guid id );
}
