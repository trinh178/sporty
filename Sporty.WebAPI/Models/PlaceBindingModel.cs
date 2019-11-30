using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Sporty.Services;

namespace Sporty.Web.Models
{
    public class ListPlaceBindingModel
    {
        public ListPlaceBindingModel()
        {
            PageSize = 10;
            PageNumb = 0;
            SortBy = SortBy.Recently;
        }
        public string Keyword { get; set; }
        public string District { get; set; }
        public string ProvinceCity { get; set; }
        public string FieldType { get; set; }
        public int PageSize { get; set; }
        public int PageNumb { get; set; }
        public SortBy SortBy { get; set; }
    }

    public class DetailPlaceBindingModel
    {
        [Required]
        public string Id { get; set; }
    }
}