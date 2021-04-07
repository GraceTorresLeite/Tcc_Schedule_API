using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tcc.Api.Model;
using Tcc.Api.Repository;

namespace Tcc.Api.Business.Implementations
{
    public class TypeServiceBusinessImplementation : ITypeServiceBusiness
    {

        private readonly ITypeServiceRepository _typeServiceRepository;

        public TypeServiceBusinessImplementation(ITypeServiceRepository typeServiceRepository)
        {
            _typeServiceRepository = typeServiceRepository;
        }

        public TypeService Create(TypeService typeService)
        {
            return _typeServiceRepository.Create(typeService);
        }

        public void Delete(long id)
        {
            _typeServiceRepository.Delete(id);
        }

        public List<TypeService> FindAll()
        {
            return _typeServiceRepository.FindAll();
        }

        public TypeService FindByID(long id)
        {
            return _typeServiceRepository.FindByID(id);
        }

        public TypeService Update(TypeService typeService)
        {
            return _typeServiceRepository.Update(typeService);
        }
    }
}
