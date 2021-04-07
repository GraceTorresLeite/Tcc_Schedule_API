using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tcc.Api.Model;
using Tcc.Api.Model.Context;

namespace Tcc.Api.Repository.Implementations
{
    public class ScheduleRepositoryImplementation : IScheduleRepository
    {
        private MySQLContext _context;

        public ScheduleRepositoryImplementation(MySQLContext context)
        {
            _context = context;
        }

        public Schedule Create(Schedule schedule)
        {
            try
            {
                _context.Add(schedule);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return schedule;
        }

        public void Delete(long id)
        {
            var result = _context.Schedules.SingleOrDefault(p => p.Id.Equals(id));

            if (result != null)
            {
                try
                {
                    _context.Schedules.Remove(result);
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
            return _context.Schedules.Any(p => p.Id.Equals(id));
        }

        public List<Schedule> FindAll()
        {
            return _context.Schedules.ToList();
        }

        public Schedule FindByID(long id)
        {
            return _context.Schedules.SingleOrDefault(p => p.Id.Equals(id));
        }

        public Schedule Update(Schedule schedule)
        {
            if (!Exists(schedule.Id)) return null;

            var result = _context.Schedules.SingleOrDefault(p => p.Id.Equals(schedule.Id));

            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(schedule);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return schedule;
        }
    }
}
