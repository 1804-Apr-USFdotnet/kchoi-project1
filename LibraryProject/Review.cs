using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject
{
    public class Review
    {
        private Nullable<int> _reviewerID;
        private int _rating;
        private string _description;
        private int _id;
        private int _restaurantID;

        public int ID { get => _id; set => _id = value; }
        public Nullable<int> ReviewerID { get => _reviewerID; set => _reviewerID = value; }
        [Required]
        public int RestaurantID { get => _restaurantID; set => _restaurantID = value; }
        [Required]
        [Range(1,5)]
        public int Rating { get => _rating; set => _rating = value; }
        [RegularExpression("^[A-Za-z 0-9]{0,200}$")]
        public string Description { get => _description; set => _description = value; }

        public override bool Equals(object obj)
        {
            return GetHashCode() == obj.GetHashCode();
        }

        public override int GetHashCode()
        {
            var hashCode = 795980842;
            hashCode = hashCode * -1521134295 + ID.GetHashCode();
            hashCode = hashCode * -1521134295 + ReviewerID.GetHashCode();
            hashCode = hashCode * -1521134295 + Rating.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Description);
            return hashCode;
        }
    }
}
