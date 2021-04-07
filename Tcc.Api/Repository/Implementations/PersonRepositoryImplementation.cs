using System;
using System.Collections.Generic;
using System.Linq;
using Tcc.Api.Model;
using Tcc.Api.Model.Context;

namespace Tcc.Api.Repository.Implementations
{
    public class PersonRepositoryImplementation : IPersonRepository
    {
        private MySQLContext _context;

        public PersonRepositoryImplementation(MySQLContext context)
        {
            _context = context;
        }

        public Person Create(Person person)
        {
            try
            {
                _context.Add(person);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return person;
        }

        public void Delete(long id)
        {
            var result = _context.People.SingleOrDefault(p => p.IdPerson.Equals(id));

            if (result != null)
            {
                try
                {
                    _context.People.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public List<Person> FindAll()
        {
            return _context.People.ToList();
        }
        public Person FindByID(long id)
        {
            return _context.People.SingleOrDefault(p => p.IdPerson.Equals(id));
        }
        public Person Update(Person person)
        {
            if (!Exists(person.IdPerson)) return null;

            var result = _context.People.SingleOrDefault(p => p.IdPerson.Equals(person.IdPerson));

            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(person);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return person;
        }

        public bool Exists(long id)
        {
            return _context.People.Any(p => p.IdPerson.Equals(id));
        }
    }
}