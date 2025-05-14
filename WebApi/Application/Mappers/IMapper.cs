namespace Application.Mappers;

public interface IMapper<Domain, Dto>
{
    List<Dto> Convert( List<Domain> domainList );

    List<Domain> Convert( List<Dto> dtoList );

    Dto Convert( Domain domain );

    Domain Convert( Dto dto );
}
