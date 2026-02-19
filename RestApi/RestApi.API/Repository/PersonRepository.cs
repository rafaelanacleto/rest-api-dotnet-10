using Microsoft.EntityFrameworkCore;
using RestApi.API.Model;
using RestApi.API.Repository;

public class PersonRepository : IPersonRepository
{    

    private readonly ApplicationDbContext _context;

    public PersonRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Person>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.People.ToListAsync(cancellationToken); // Changed to use ToListAsync
    }

    public async Task<Person?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.People.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<Person> AddAsync(Person person, CancellationToken cancellationToken = default)
    {
        _context.People.Add(person);
        await _context.SaveChangesAsync(cancellationToken);
        return person;
    }

    public async Task<bool> UpdateAsync(Person person, CancellationToken cancellationToken = default)
    {
        var existingPerson = await _context.People.FirstOrDefaultAsync(p => p.Id == person.Id, cancellationToken);
        if (existingPerson == null)
            return false;

        existingPerson.Name = person.Name;
        existingPerson.Age = person.Age;
        existingPerson.Email = person.Email;
        existingPerson.Address = person.Address;

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var person = await _context.People.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        if (person == null)
            return false;

        _context.People.Remove(person);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}