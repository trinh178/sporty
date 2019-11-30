using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sporty.DAL.Models
{
    [Table("WeekPrices")]
    public class WeekPrice
    {
        [Key]
        [MaxLength(50)]
        public string Id { get; set; }
        public string Monday { get; set; }
        public string Tuesday { get; set; }
        public string Wednesday { get; set; }
        public string Thursday { get; set; }
        public string Friday { get; set; }
        public string Saturday { get; set; }
        public string Sunday { get; set; }

        // FieldTypeId
        [MaxLength(50)]
        public string FieldTypeId { get; set; }
        [ForeignKey("FieldTypeId")]
        public virtual FieldType FieldType { get; set; }
    }
}
