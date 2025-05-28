using Domain.Entities;

namespace Domain.Repositories;

public interface IPropertiesRepository : IDomainRepository<Property>
{
    Task<List<Property>> GetPropertiesByFiltersAsync(
        string city,
        DateTime arrivalDate,
        DateTime departureDate,
        int guests,
        decimal? maxPrice
        );
}