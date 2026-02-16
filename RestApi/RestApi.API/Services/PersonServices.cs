using RestApi.API.Model;

namespace RestApi.API.Services
{
    public class PersonServices : IPersonServices
    {
        public Person Create(Person person)
        {
            return person;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Person> GetAll()
        {
            return new List<Person>
            {
                new Person { Id = 1, Name = "John Doe", Age = 30, Address = "123 Main St" , Email = "john.doe@example.com" },
                new Person { Id = 2, Name = "Jane Smith", Age = 25, Address = "456 Elm St", Email = "jane.smith@example.com" }
            };
        }

        public Person GetById(int id)
        {
            return new Person
            {
                Id = id,
                Name = "John Doe",
                Age = 30,
                Address = "123 Main St",
                Email = "john.doe@example.com"
            };
        }

        public Person GetByName(string name)
        {
            return new Person
            {
                Id = 1,
                Name = name,
                Age = 30,
                Address = "123 Main St",
                Email = "john.doe@example.com"
            };
        }

        public Person Update(Person person)
        {
            return person;
        }
    }
}
