using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tcc.Api.Model;
using Tcc.Api.Repository;

namespace Tcc.Api.Business.Implementations
{
    public class ScheduleBusinessImplementation : IScheduleBusiness
    {
        private readonly IScheduleRepository _scheduleRepository;

        public ScheduleBusinessImplementation(IScheduleRepository scheduleRepository)
        {
            _scheduleRepository = scheduleRepository;
        }

        public Schedule Create(Schedule schedule)
        {
            return _scheduleRepository.Create(schedule);
        }

        public void Delete(long id)
        {
            _scheduleRepository.Delete(id);
        }

        public List<Schedule> FindAll()
        {
            return _scheduleRepository.FindAll();
        }

        public Schedule FindByID(long id)
        {
            return _scheduleRepository.FindByID(id);
        }

        public Schedule Update(Schedule schedule)
        {
            return _scheduleRepository.Update(schedule);
        }
    }
}
