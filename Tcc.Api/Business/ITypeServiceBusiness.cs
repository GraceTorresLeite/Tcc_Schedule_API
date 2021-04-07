
using System.Collections.Generic;
using Tcc.Api.Model;

namespace Tcc.Api.Business
{
   public  interface ITypeServiceBusiness
    {
        TypeService Create(TypeService typeService);
        TypeService FindByID(long id);
        List<TypeService> FindAll();
        TypeService Update(TypeService typeService);
        void Delete(long id);
    }
}
