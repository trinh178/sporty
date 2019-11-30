using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sporty.DAL.Models
{
    [Table("Fields")]
    public class Field
    {
        [Key]
        [MaxLength(50)]
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }

        // FieldTypeId
        [MaxLength(50)]
        public string FieldTypeId { get; set; }
        [ForeignKey("FieldTypeId")]
        public virtual FieldType FieldType { get; set; }
        // PlaceId
        [MaxLength(50)]
        public string PlaceId { get; set; }
        [ForeignKey("PlaceId")]
        public virtual Place Place { get; set; }

        public virtual List<ScheduleOrder> Schedules { get; set; }
    }
}
