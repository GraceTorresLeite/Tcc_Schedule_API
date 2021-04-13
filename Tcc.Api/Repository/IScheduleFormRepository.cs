using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tcc.Api.Model;

namespace Tcc.Api.Repository
{
    public interface IScheduleFormRepository
    {
        ScheduleForm Create(ScheduleForm scheduleForm);
        ScheduleForm FindByID(long id);
        List<ScheduleForm> FindAll();
        ScheduleForm Update(ScheduleForm scheduleForm);
        void Delete(long id);
        bool Exists(long id);
    }
}
