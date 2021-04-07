using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tcc.Api.Model;
using Tcc.Api.Repository;

namespace Tcc.Api.Business.Implementations
{
    public class PersonBusinessImplementation : IPersonBusiness
    {
        private readonly IPersonRepository _personRepository;

        public PersonBusinessImplementation(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public Person Create(Person person)
        {
            return _personRepository.Create(person);
        }

        public void Delete(long id)
        {
            _personRepository.Delete(id);
        }
        public List<Person> FindAll()
        {
            return _personRepository.FindAll();
        }

        public Person FindByID(long id)
        {
            return _personRepository.FindByID(id);
        }

        public Person Update(Person person)
        {
            return _personRepository.Update(person);
        }
    }
}