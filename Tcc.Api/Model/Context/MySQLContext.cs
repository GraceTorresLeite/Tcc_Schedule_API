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

        public DbSet<Person> People { get; set; } // dBsET É TIPO UMA LISTA - DEPOIS DISTO vamos configurar nosso appsetings.Json

        public DbSet<TypeService> TypesServices { get; set; }

        public DbSet<Schedule> Schedules { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    //modelBuilder.Entity<Schedule>()
        //    //    .HasMany(b => b.TypesServices)
        //    //    .WithOne();

        //    modelBuilder.Entity<Schedule>()
        //   .Property<int>("PersonForeignKey");

        //    modelBuilder.Entity<Schedule>()
        //    .HasOne(p => p.Person)
        //    .WithMany(b => b.Schedules)
        //   // .HasForeignKey(p => p.PersonForeignKey);
        //    .HasForeignKey("PersonForeignKey");

        //    //modelBuilder.Entity<Schedule>()
        //    //    .Navigation(b => b.TypesServices)
        //    //    .UsePropertyAccessMode(PropertyAccessMode.Property);

        //}
    }
}
