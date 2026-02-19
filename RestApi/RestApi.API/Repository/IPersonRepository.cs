using RestApi.API.Model;

namespace RestApi.API.Repository;

public interface IPersonRepository
{
    Task<IEnumerable<Person>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Person?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Person> AddAsync(Person person, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(Person person, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}