using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tcc.Api.Model;
using Tcc.Api.Model.Context;

namespace Tcc.Api.Repository.Implementations
{
    public class ScheduleFormRepositoryImplementation:IScheduleFormRepository
    {
        private MySQLContext _context;

        public ScheduleFormRepositoryImplementation(MySQLContext context)
        {
            _context = context;
        }

        public ScheduleForm Create(ScheduleForm scheduleForm)
        {
            try
            {
                _context.Add(scheduleForm);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return scheduleForm;
        }

        public void Delete(long id)
        {
            var result = _context.SchedulesForms.SingleOrDefault(p => p.Id.Equals(id));

            if (result != null)
            {
                try
                {
                    _context.SchedulesForms.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public bool Exists(long id)
        {
            return _context.SchedulesForms.Any(p => p.Id.Equals(id));
        }

        public List<ScheduleForm> FindAll()
        {
            return _context.SchedulesForms.ToList();
        }

        public ScheduleForm FindByID(long id)
        {
            return _context.SchedulesForms.SingleOrDefault(p => p.Id.Equals(id));
        }

        public ScheduleForm Update(ScheduleForm scheduleForm)
        {
            if (!Exists(scheduleForm.Id)) return null;

            var result = _context.SchedulesForms.SingleOrDefault(p => p.Id.Equals(scheduleForm.Id));

            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(scheduleForm);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return scheduleForm;
        }
    }
}
