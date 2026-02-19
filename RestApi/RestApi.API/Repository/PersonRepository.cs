
using RestApi.API.Model;
using RestApi.API.Repository;

public class PersonRepository : IPersonRepository
{
    private readonly List<Person> _people = new();

    public Task<IEnumerable<Person>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_people.AsEnumerable());
    }

    public Task<Person?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var person = _people.FirstOrDefault(p => p.Id == id);
        return Task.FromResult(person);
    }

    public Task<Person> AddAsync(Person person, CancellationToken cancellationToken = default)
    {
        person.Id = _people.Count > 0 ? _people.Max(p => p.Id) + 1 : 1;
        _people.Add(person);
        return Task.FromResult(person);
    }

    public Task<bool> UpdateAsync(Person person, CancellationToken cancellationToken = default)
    {
        var existingPerson = _people.FirstOrDefault(p => p.Id == person.Id);
        if (existingPerson == null)
            return Task.FromResult(false);

        existingPerson.Name = person.Name;
        existingPerson.Age = person.Age;
        existingPerson.Email = person.Email;
        existingPerson.Address = person.Address;

        return Task.FromResult(true);
    }

    public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var person = _people.FirstOrDefault(p => p.Id == id);
        if (person == null)
            return Task.FromResult(false);

        _people.Remove(person);
        return Task.FromResult(true);
    }
}