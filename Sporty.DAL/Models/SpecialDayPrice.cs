using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sporty.DAL.Models
{
    [Table("SpecialDayPrices")]
    public class SpecialDayPrice
    {
        [Key]
        [MaxLength(50)]
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string Data { get; set; }

        // FieldTypeId
        [MaxLength(50)]
        public string FieldTypeId { get; set; }
        [ForeignKey("FieldTypeId")]
        public virtual FieldType FieldType { get; set; }
    }
}
