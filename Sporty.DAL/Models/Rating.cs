using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sporty.DAL.Models
{
    [Table("Ratings")]
    public class Rating
    {
        public int RatingNumber { get; set; }

        // PlaceId
        [Key]
        [Column(Order = 0)]
        [MaxLength(50)]
        public string PlaceId { get; set; }
        [ForeignKey("PlaceId")]
        public virtual Place Place { get; set; }
        // CustomerId
        [Key]
        [Column(Order = 1)]
        //[MaxLength(50)]
        public string CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual ApplicationUser Customer { get; set; }
    }
}
