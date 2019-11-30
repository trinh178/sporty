using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sporty.WebAPI.Models
{
    public class PossibleScheduleOrderTimeViewModel
    {
        [JsonProperty("begin_hour")]
        public float BeginHour { get; set; }
        [JsonProperty("min_duration")]
        public float MinDuration { get; set; }
        [JsonProperty("max_duration")]
        public float MaxDuration { get; set; }
    }
}