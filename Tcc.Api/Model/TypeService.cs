using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Tcc.Api.Model
{
    [Table("type_service")]
    public class TypeService
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        [Required]
        public string Name { get; set; }

        [Column("price")]
        [Range(0.01, 999.99)]
        public decimal Price { get; set; }

        //public int ScheduleId { get; set; }

      //  [Column("schedule_id")]
     //   public Schedule Schedule { get; set; }
    }
}
