using System.Collections.Generic;
using Tcc.Api.Model;
using Tcc.Api.Repository;

namespace Tcc.Api.Business.Implementations
{
    public class ScheduleFormsBusinessImplementation : IScheduleFormsBusiness
    {
        private readonly IRepository<ScheduleForm> _repository;
       public ScheduleFormsBusinessImplementation(IRepository<ScheduleForm> repository)
        {
            _repository = repository;
        }

        public ScheduleForm Create(ScheduleForm scheduleForm)
        {
            return _repository.Create(scheduleForm);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public List<ScheduleForm> FindAll()
        {
            return _repository.FindAll();
        }

        public ScheduleForm FindByID(long id)
        {
            return _repository.FindByID(id);
        }

        public ScheduleForm Update(ScheduleForm scheduleForm)
        {
            return _repository.Update(scheduleForm);
        }
    }
}
