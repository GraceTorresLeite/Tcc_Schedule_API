using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tcc.Api.Model;
using Tcc.Api.Model.Base;

namespace Tcc.Api.Data.VO
{
    public class ScheduleFormVO 
    {
  
        public long Id { get; set; }
        public string FirstName { get; set; }     
        public string Email { get; set; }    
        public string Address { get; set; }      
        public string Phone { get; set; }     
        public int Service { get; set; }
        public DateTime Date { get; set; }

        public ScheduleFormVO()
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
