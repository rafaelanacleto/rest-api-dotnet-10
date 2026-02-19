using RestApi.API.Model;

namespace RestApi.API.Services
{
    public interface IPersonServices
    {
        Task<Person> Create(Person person);
        Task<Person?> GetById(int id);
        Task<Person?> GetByName(string name);
        Task<IEnumerable<Person>> GetAll();
        Task<Person?> Update(Person person);
        Task Delete(int id);
    }
}
