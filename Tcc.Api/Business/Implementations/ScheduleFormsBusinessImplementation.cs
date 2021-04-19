using System.Collections.Generic;
using Tcc.Api.Data.Converter.Implementations;
using Tcc.Api.Data.VO;
using Tcc.Api.Model;
using Tcc.Api.Repository;

namespace Tcc.Api.Business.Implementations
{
    public class ScheduleFormsBusinessImplementation : IScheduleFormsBusiness
    {
        private readonly IRepository<ScheduleForm> _repository;
        private readonly ScheduleFormConverter _converter;
       public ScheduleFormsBusinessImplementation(IRepository<ScheduleForm> repository)
        {
            _repository = repository;
            _converter = new ScheduleFormConverter();
        }

        public ScheduleFormVO Create(ScheduleFormVO ScheduleFormVO)
        {
            var scheduleFormEntity = _converter.Parse(ScheduleFormVO);
            scheduleFormEntity = _repository.Create(scheduleFormEntity);
            return _converter.Parse(scheduleFormEntity);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public List<ScheduleFormVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }

        public ScheduleFormVO FindByID(long id)
        {
            return _converter.Parse(_repository.FindByID(id));
        }

        public ScheduleFormVO Update(ScheduleFormVO ScheduleFormVO)
        {
            var scheduleFormEntity = _converter.Parse(ScheduleFormVO);
            scheduleFormEntity = _repository.Update(scheduleFormEntity);
            return _converter.Parse(scheduleFormEntity);
        }
    }
}
