using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tcc.Api.Model;

namespace Tcc.Api.Repository
{
   public  interface ITypeServiceRepository
    {
        TypeService Create(TypeService typeService);
        TypeService FindByID(long id);
        List<TypeService> FindAll();
        TypeService Update(TypeService typeService);
        void Delete(long id);
        bool Exists(long id);
    }
}
