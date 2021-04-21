using Microsoft.EntityFrameworkCore;

namespace Tcc.Api.Model.Context
{
    public class MySQLContext : DbContext
    {

        public MySQLContext()
        {

        }

        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) { }
        public DbSet<ScheduleForm> SchedulesForms { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
