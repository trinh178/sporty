using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sporty.DAL.Models
{
    [Table("FieldTypes")]
    public class FieldType
    {
        [Key]
        [MaxLength(50)]
        public string Id { get; set; }
        public string Name { get; set; }

        public virtual List<Field> Fields { get; set; }
        public virtual List<WeekPrice> WeekPrices { get; set; }
        public virtual List<SpecialDayPrice> SpecialDayPrices { get; set; }
    }
}
