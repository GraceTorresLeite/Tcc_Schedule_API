using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tcc.Api.Model.Base;

namespace Tcc.Api.Model
{
    [Table("schedule_form")]
    public class ScheduleForm : BaseEntity
    {
  
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

        public ScheduleForm()
        {
                
        }
        public TypeServices GetService(int service)
        {
           return (TypeServices)ServiceTypeWrapper.ServiceTypeById(service);
        }

        public void SetService(TypeServices types)
        {
            this.Service = types.GetHashCode();
        }
    }
}
