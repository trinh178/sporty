using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sporty.Models
{
    [Table("Schedules")]
    public class Schedule
    {
        [Key]
        [MaxLength(50)]
        public string Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; }
        public DateTime StartDate { get; set; }
        public int Duration { get; set; }

        // CustomerId
        // GuestId
    }
}
