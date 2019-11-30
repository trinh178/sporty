using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sporty.DAL.Models
{
    [Table("Guests")]
    public class Guest
    {
        [Key]
        [MaxLength(50)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string ContactPhoneNumber { get; set; }
        public string Note { get; set; }

        public List<ScheduleOrder> Schedules { get; set; }
    }
}
