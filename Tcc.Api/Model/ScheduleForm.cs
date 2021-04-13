using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Tcc.Api.Model
{
    [Table("schedule_form")]
    public class ScheduleForm
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("first_name")]
        [Required]
        public string FirstName { get; set; }

        [Column("email")]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Column("address")]
        [Required]
        public string Address { get; set; }

        [Column("phone")]
        [Required]
        public string Phone { get; set; }

        [Column("service")]
        [Required]
        public int Service { get; set; }

        [Column("date")]
        [Required]
        public DateTime Date { get; set; }

        public ServiceTypes GetService(int service)
        {
           return (ServiceTypes)ServiceTypeWrapper.ServiceTypeById(service);
        }

        public void SetService(ServiceTypes types)
        {
            this.Service = types.GetHashCode();
        }
        
    }
}
