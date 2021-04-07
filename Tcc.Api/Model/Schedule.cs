using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Tcc.Api.Model
{
    [Table("schedule")]
    public class Schedule
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("schedule_date")]
        public DateTime ScheduleDate { get; set; }

        public int PersonForeignKey { get; set; }

        [Column("person_id")]
        public Person Person { get; set; }

        [Column("type_service_id")]
        public List<TypeService> TypesServices { get; set; }
    }
}
