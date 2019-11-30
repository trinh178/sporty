using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sporty.DAL.Models
{
    [Table("Places")]
    public class Place
    {
        [Key]
        [MaxLength(50)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ContactPhoneNumber1 { get; set; }
        public string ContactPhoneNumber2 { get; set; }
        public string ImagesArray { get; set; }
        public float OpenHour { get; set; }
        public float CloseHour { get; set; }
        public float MinDuration { get; set; }
        public float MaxDuration { get; set; }
        public float RatingAvgNumber { get; set; }
        public string AddressInfo { get; set; }
        public string AddressDistrict { get; set; }
        public string AddressProvinceCity { get; set; }
        public DateTime CreatedDate { get; set; }

        // OwnerId
        //[MaxLength(50)]
        public string OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public virtual ApplicationUser Owner { get; set; }
        
        public virtual List<Rating> Ratings { get; set; }
        public virtual List<Field> Fields { get; set; }
    }
}
