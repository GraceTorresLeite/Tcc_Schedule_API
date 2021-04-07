﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Tcc.Api.Model
{
    [Table("person")]
    public class Person
    {
        [Column("id")]
        public long IdPerson { get; set; }

        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("last_name")]
        public string LastName { get; set; }

        [Column("address")]
        public string Address { get; set; }

        [Column("phone")]
        public string Phone { get; set; }

        [Column("schedule_id")]
        public List<Schedule> Schedules { get; set; }


    }
}