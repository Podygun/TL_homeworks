namespace Domain.Repositories;


public interface IDomainRepository<T>
{
    Task AddAsync( T entity );

    Task<T?> GetByIdAsync( int id );

    Task<List<T>> GetAllAsync();

    Task UpdateAsync( T entity );

    Task DeleteByIdAsync( int id );
}
