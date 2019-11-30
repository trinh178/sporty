using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sporty.WebAPI.Models
{
    public class PreviewPlaceViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        [JsonProperty("images_array")]
        public string[] Images { get; set; }
        [JsonProperty("rating_avg_number")]
        public float RatingAvgNumber { get; set; }
        [JsonProperty("address_info")]
        public string AddressInfo { get; set; }
        [JsonProperty("address_district")]
        public string AddressDistrict { get; set; }
        [JsonProperty("address_province_city")]
        public string AddressProvinceCity { get; set; }
        [JsonProperty("created_date")]
        public DateTime CreatedDate { get; set; }
        [JsonProperty("owner_id")]
        public string OwnerId { get; set; }
    }

    public class PlaceViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [JsonProperty("contact_phone_number_1")]
        public string ContactPhoneNumber1 { get; set; }
        [JsonProperty("contact_phone_number_2")]
        public string ContactPhoneNumber2 { get; set; }
        [JsonProperty("images_array")]
        public string[] Images { get; set; }
        [JsonProperty("open_time")]
        public TimeSpan OpenTime { get; set; }
        [JsonProperty("close_time")]
        public TimeSpan CloseTime { get; set; }
        [JsonProperty("rating_avg_number")]
        public float RatingAvgNumber { get; set; }
        [JsonProperty("address_info")]
        public string AddressInfo { get; set; }
        [JsonProperty("address_district")]
        public string AddressDistrict { get; set; }
        [JsonProperty("address_province_city")]
        public string AddressProvinceCity { get; set; }
        [JsonProperty("created_date")]
        public DateTime CreatedDate { get; set; }
        [JsonProperty("owner_id")]
        public string OwnerId { get; set; }
    }

    public class PlacePriceListViewModel
    {
        public PlacePriceListViewModel()
        {
            Monday = new List<FieldTypePriceViewModel>();
            Tuesday = new List<FieldTypePriceViewModel>();
            Wednesday = new List<FieldTypePriceViewModel>();
            Thursday = new List<FieldTypePriceViewModel>();
            Friday = new List<FieldTypePriceViewModel>();
            Saturday = new List<FieldTypePriceViewModel>();
            Sunday = new List<FieldTypePriceViewModel>();
        }

        public List<FieldTypePriceViewModel> Monday { get; set; }
        public List<FieldTypePriceViewModel> Tuesday { get; set; }
        public List<FieldTypePriceViewModel> Wednesday { get; set; }
        public List<FieldTypePriceViewModel> Thursday { get; set; }
        public List<FieldTypePriceViewModel> Friday { get; set; }
        public List<FieldTypePriceViewModel> Saturday { get; set; }
        public List<FieldTypePriceViewModel> Sunday { get; set; }
    }

    public class FieldTypePriceViewModel
    {
        public string FieldTypeName { get; set; }
        public string DayPrice { get; set; }
    }
}