using Sporty.DAL.Constants;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sporty.DAL.Models
{
    [Table("ScheduleOrders")]
    public class ScheduleOrder
    {
        [Key]
        [MaxLength(50)]
        public string Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public ScheduleOrderStatus Status { get; set; }
        public DateTime StartDate { get; set; }
        public float Duration { get; set; }

        // FieldId
        public string FieldId { get; set; }
        [ForeignKey("FieldId")]
        public Field Field { get; set; }
        // CustomerId
        //[MaxLength(50)]
        public string CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public ApplicationUser Customer { get; set; }
        // GuestId
        [MaxLength(50)]
        public string GuestId { get; set; }
        [ForeignKey("GuestId")]
        public Guest Guest { get; set; }
    }
}
