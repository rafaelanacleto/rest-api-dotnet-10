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

        public Person Create(Person person)
        {
            _personRepository.AddAsync(person);
            return person;
        }

        public void Delete(int id)
        {
            _personRepository.DeleteAsync(id);            
        }

        public List<Person> GetAll()
        {
            return _personRepository.GetAllAsync().Result.ToList();
        }

        public Person GetById(int id)
        {
            return _personRepository.GetByIdAsync(id).Result;
        }

        public Person GetByName(string name)
        {
            return _personRepository.GetAllAsync().Result.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public Person Update(Person person)
        {
            return _personRepository.UpdateAsync(person).Result ? person : null;
        }
    }
}
