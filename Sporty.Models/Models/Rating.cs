using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sporty.Models
{
    [Table("Ratings")]
    public class Rating
    {
        // PlaceId
        // CustomerId
        public int RatingNumber { get; set; }
    }
}
