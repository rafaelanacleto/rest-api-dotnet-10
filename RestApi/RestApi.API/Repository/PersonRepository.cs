
using RestApi.API.Model;
using RestApi.API.Repository;

public class PersonRepository : IPersonRepository
{    

    private readonly ApplicationDbContext _context;

    public PersonRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<IEnumerable<Person>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_context.People.AsEnumerable());
    }

    public Task<Person?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var person = _context.People.FirstOrDefault(p => p.Id == id);
        return Task.FromResult(person);
    }

    public Task<Person> AddAsync(Person person, CancellationToken cancellationToken = default)
    {
        _context.People.Add(person);
        _context.SaveChanges();
        return Task.FromResult(person);
    }

    public Task<bool> UpdateAsync(Person person, CancellationToken cancellationToken = default)
    {
        var existingPerson = _context.People.FirstOrDefault(p => p.Id == person.Id);
        if (existingPerson == null)
            return Task.FromResult(false);

        existingPerson.Name = person.Name;
        existingPerson.Age = person.Age;
        existingPerson.Email = person.Email;
        existingPerson.Address = person.Address;

        _context.SaveChanges();
        return Task.FromResult(true);
    }

    public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var person = _context.People.FirstOrDefault(p => p.Id == id);
        if (person == null)
            return Task.FromResult(false);

        _context.People.Remove(person);
        _context.SaveChanges();
        return Task.FromResult(true);
    }
}