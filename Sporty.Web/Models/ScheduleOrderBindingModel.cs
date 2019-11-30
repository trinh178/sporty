using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sporty.Web.Models
{
    public class GetAllPossibleScheduleOrderTimeBingdingModel
    {
        [Required]
        public string FieldId { get; set; }
        [Required]
        public string DateString { get; set; }
    }

    public class GetPriceBindingModel
    {
        [Required]
        public string FieldId { get; set; }
        [Required]
        public string DateString { get; set; }
        [Required]
        public float BeginHour { get; set; }
        [Required]
        public float Duration { get; set; }
    }
}