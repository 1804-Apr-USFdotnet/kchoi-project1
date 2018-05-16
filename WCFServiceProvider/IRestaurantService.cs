using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCFServiceProvider
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IRestaurantService
    {
        [OperationContract]
        Restaurant GetRestaurantByID(int id);

        [OperationContract]
        IEnumerable<Restaurant> GetRestaurants();
        
        [OperationContract]
        IEnumerable<Review> GetReviewsByRestaurantID(int id);
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class Restaurant
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string State { get; set; }
        [DataMember]
        public string ZIP { get; set; }
        [DataMember]
        public string PhoneNum { get; set; }
        [DataMember]
        public Nullable<double> AvgRating { get; set; }
    }

    [DataContract]
    public class Review
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int RestaurantID { get; set; }
        [DataMember]
        public Nullable<int> ReviewerID { get; set; }
        [DataMember]
        public int Rating { get; set; }
        [DataMember]
        public string Description { get; set; }
    }
}
