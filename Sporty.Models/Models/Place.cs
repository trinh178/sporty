using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sporty.Models
{
    [Table("Places")]
    public class Place
    {
        [Key]
        [MaxLength(50)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ContactPhoneNumber1 { get; set; }
        public int ContactPhoneNumber2 { get; set; }
        public string ImagesArray { get; set; }
        public TimeSpan OpenTime { get; set; }
        public TimeSpan CloseTime { get; set; }
        public int RatingAvgNumber { get; set; }
        public string AddressInfo { get; set; }
        public string AddressDistrict { get; set; }
        public string AddressProvinceCity { get; set; }

        // OnwerId
    }
}
