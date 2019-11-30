using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Sporty.WebAPI.Models
{
    public class ListFieldBindingModel
    {
        [Required]
        public string PlaceId { get; set; }
    }

    public class DetailFieldBindingModel
    {
        [Required]
        public string Id { get; set; }
    }
}