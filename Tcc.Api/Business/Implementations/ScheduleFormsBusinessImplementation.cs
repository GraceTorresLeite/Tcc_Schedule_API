using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tcc.Api.Model;
using Tcc.Api.Repository;

namespace Tcc.Api.Business.Implementations
{
    public class ScheduleFormsBusinessImplementation : IScheduleFormsBusiness
    {
        private readonly IScheduleFormRepository _scheduleFormRepository;
       public ScheduleFormsBusinessImplementation(IScheduleFormRepository scheduleFormRepository)
        {
            _scheduleFormRepository = scheduleFormRepository;
        }

        public ScheduleForm Create(ScheduleForm scheduleForm)
        {
            return _scheduleFormRepository.Create(scheduleForm);
        }

        public void Delete(long id)
        {
            _scheduleFormRepository.Delete(id);
        }

        public List<ScheduleForm> FindAll()
        {
            return _scheduleFormRepository.FindAll();
        }

        public ScheduleForm FindByID(long id)
        {
            return _scheduleFormRepository.FindByID(id);
        }

        public ScheduleForm Update(ScheduleForm scheduleForm)
        {
            return _scheduleFormRepository.Update(scheduleForm);
        }
    }
}
