using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tcc.Api.Model;

namespace Tcc.Api.Business
{
    public interface IScheduleBusiness
    {
        Schedule Create(Schedule schedule);
        Schedule FindByID(long id);
        List<Schedule> FindAll();
        Schedule Update(Schedule schedule);
        void Delete(long id);
    }
}
