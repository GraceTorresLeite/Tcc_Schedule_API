using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tcc.Api.Model;
using Tcc.Api.Model.Context;

namespace Tcc.Api.Repository.Implementations
{
    public class TypeServiceRepositoryImplementation : ITypeServiceRepository
    {

        private MySQLContext _context;

        public TypeServiceRepositoryImplementation(MySQLContext context)
        {
            _context = context;
        }

        public TypeService Create(TypeService typeService)
        {
            try
            {
                _context.Add(typeService);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return typeService;
        }

        public void Delete(long id)
        {
            var result = _context.TypesServices.SingleOrDefault(p => p.Id.Equals(id));

            if (result != null)
            {
                try
                {
                    _context.TypesServices.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public bool Exists(long id)
        {
            return _context.TypesServices.Any(p => p.Id.Equals(id));
        }

        public List<TypeService> FindAll()
        {
            return _context.TypesServices.ToList();
        }

        public TypeService FindByID(long id)
        {
            return _context.TypesServices.SingleOrDefault(p => p.Id.Equals(id));
        }

        public TypeService Update(TypeService typeService)
        {
            if (!Exists(typeService.Id)) return null;

            var result = _context.TypesServices.SingleOrDefault(p => p.Id.Equals(typeService.Id));

            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(typeService);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return typeService;
        }
    }
}
