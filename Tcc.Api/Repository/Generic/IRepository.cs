using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tcc.Api.Model;
using Tcc.Api.Model.Base;

namespace Tcc.Api.Repository
{
    public interface IRepository<T> where T: BaseEntity
    {
        T Create(T item);
        T FindByID(long id);
        List<T> FindAll();
        T Update(T item);
        void Delete(long id);
        bool Exists(long id);
    }
}
