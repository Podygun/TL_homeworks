namespace Domain.Repositories;


public interface IDomainRepository<T>
{
    void Add( T entity );

    T GetById( Guid id );

    List<T> GetAll();

    void Update( T entity );

    void DeleteById( Guid id );
}
