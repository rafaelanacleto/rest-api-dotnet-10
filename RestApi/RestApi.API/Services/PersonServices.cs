using RestApi.API.Model;
using RestApi.API.Repository;

namespace RestApi.API.Services
{
    public class PersonServices : IPersonServices
    {

        private readonly IPersonRepository _personRepository;

        public PersonServices(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<Person> Create(Person person)
        {
            await _personRepository.AddAsync(person);
            return person;
        }

        public async Task Delete(int id)
        {
            await _personRepository.DeleteAsync(id);            
        }

        public async Task<IEnumerable<Person>> GetAll()
        {
            return await _personRepository.GetAllAsync();
        }

        public async Task<Person?> GetById(int id)
        {
            return await _personRepository.GetByIdAsync(id);
        }

        public async Task<Person?> GetByName(string name)
        {
            var people = await _personRepository.GetAllAsync();
            return people.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<Person?> Update(Person person)
        {
            var success = await _personRepository.UpdateAsync(person);
            return success ? person : null;
        }
    }
}
