using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tcc.Api.Model.Context
{
    public class MySQLContext : DbContext
    {

        public MySQLContext()
        {

        }

        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) { }

        public DbSet<Person> People { get; set; } 

        public DbSet<TypeService> TypesServices { get; set; }

        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<ScheduleForm> SchedulesForms { get; set; }
    }
}
