using RestApi.API.Model;

namespace RestApi.API.Services
{
    public interface IPersonServices
    {
        Person Create(Person person);
        Person GetById(int id);
        Person GetByName(string name);
        List<Person> GetAll();
        Person Update (Person person);
        void Delete(int id);

    }
}
