using Newtonsoft.Json;
using Sporty.DAL.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace Sporty.Web.Models
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

    public class ScheduleOrderViewModel
    {
        public DateTime CreatedDate { get; set; }
        public DateTime StartDate { get; set; }
        public float Duration { get; set; } 
        public string PlaceId { get; set; }
        public float Price { get; set; }
        public ScheduleOrderStatus Status { get; set; }
    }
}