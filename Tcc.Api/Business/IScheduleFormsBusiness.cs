using System.Collections.Generic;
using Tcc.Api.Data.VO;

namespace Tcc.Api.Business
{
    public interface IScheduleFormsBusiness
    {
        ScheduleFormVO Create(ScheduleFormVO scheduleFormVO);
        ScheduleFormVO FindByID(long id);
        List<ScheduleFormVO> FindAll();
        ScheduleFormVO Update(ScheduleFormVO scheduleFormVO);
        void Delete(long id);
    }
}
