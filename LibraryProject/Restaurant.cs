using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LibraryProject.Interfaces;

namespace LibraryProject
{
    public class Restaurant : IReviewable
    {
        private int _id;
        private string _address;
        private string _phone;
        private string _city;
        private string _state;
        private string _zip;
        private string _name;

        private List<Review> _reviews = new List<Review>();

        private float _avgRating = 0f;

        public int ID { get => _id; set => _id = value; }
        [Required]
        [RegularExpression("^[A-Za-z 0-9]{0,50}$")]
        public string Name { get => _name; set => _name = value; }
        [JsonIgnore]
        [Display(Name = "Average Rating")]
        public float AvgRating { get => _avgRating; set => _avgRating = value; }
        [RegularExpression("^[A-Za-z 0-9]{0,50}$")]
        public string Address { get => _address; set => _address = value; }
        [RegularExpression("^[A-Za-z 0-9]{0,50}$")]
        public string City { get => _city; set => _city = value; }
        [RegularExpression("[A-Z]{2}")]
        public string State { get => _state; set => _state = value; }
        [DataType(DataType.PostalCode)]
        public string ZIP { get => _zip; set => _zip = value; }
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string PhoneNum { get => _phone; set => _phone = value; }

        public ICollection<Review> Reviews { get => _reviews; set => _reviews = (List<Review>)value; }

        public override bool Equals(object obj)
        {
            bool result = GetHashCode() == obj.GetHashCode();

            if (result)
            {
                result = Reviews.Count == ((Restaurant)obj).Reviews.Count;

                if (result)
                {
                    for (int i = 0; i < Reviews.Count; i++)
                    {
                        if (!(result = Reviews.ElementAt(i).Equals(((Restaurant)obj).Reviews.ElementAt(i))))
                        {
                            break;
                        }
                    }
                }
            }

            return result;
        }

        public override int GetHashCode()
        {
            var hashCode = 1381076286;
            hashCode = hashCode * -1521134295 + ID.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Address);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PhoneNum);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(City);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(State);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ZIP);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + AvgRating.GetHashCode();
            return hashCode;
        }
    }
}
