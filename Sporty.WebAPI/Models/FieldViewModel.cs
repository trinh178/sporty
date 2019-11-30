using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sporty.WebAPI.Models
{
    public class FieldViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        [JsonProperty("created_date")]
        public DateTime CreatedDate { get; set; }
        [JsonProperty("field_type_id")]
        public string FieldTypeId { get; set; }
        [JsonProperty("field_type_name")]
        public string FieldTypeName { get; set; }
        [JsonProperty("place_id")]
        public string PlaceId { get; set; }
    }

    public class WeekPriceViewModel
    {
        public string Monday { get; set; }
        public string Tuesday { get; set; }
        public string Wednesday { get; set; }
        public string Thursday { get; set; }
        public string Friday { get; set; }
        public string Saturday { get; set; }
        public string Sunday { get; set; }
    }
}