using DataProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCFServiceProvider
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class RestaurantService : IRestaurantService
    {
        public Restaurant GetRestaurantByID(int id)
        {
            return Mapper.ConvertRestaurantFromDB(RestaurantCRUD.ReadRestaurants().Where(x => x.ID == id).FirstOrDefault());
        }

        public IEnumerable<Restaurant> GetRestaurants()
        {
            List<Restaurant> result = new List<Restaurant>();

            foreach(var i in RestaurantCRUD.ReadRestaurants())
            {
                result.Add(Mapper.ConvertRestaurantFromDB(i));
            }

            return result;
        }

        public IEnumerable<Review> GetReviewsByRestaurantID(int id)
        {
            List<Review> result = new List<Review>();

            foreach(var i in RestaurantCRUD.ReadReviews().Where(x => x.RestaurantID == id))
            {
                result.Add(Mapper.ConvertReviewFromDB(i));
            }

            return result;
        }
    }

    static class Mapper
    {
        public static Restaurant ConvertRestaurantFromDB(DataProject.Restaurant rest)
        {
            Restaurant result;

            if(rest != null)
            {
                result = new Restaurant
                {
                    Address = rest.Address,
                    AvgRating = rest.AvgRating,
                    City = rest.City,
                    ID = rest.ID,
                    Name = rest.Name,
                    PhoneNum = rest.PhoneNum,
                    State = rest.State,
                    ZIP = rest.ZIP
                };
            } else
            {
                result = null;
            }

            return result;
        }

        public static Review ConvertReviewFromDB(DataProject.Review rev)
        {
            Review result;

            if(rev != null)
            {
                result = new Review
                {
                    Description = rev.Description,
                    ID = rev.ID,
                    Rating = rev.Rating,
                    RestaurantID = rev.RestaurantID,
                    ReviewerID = rev.ReviewerID
                };
            } else
            {
                result = null;
            }

            return result;
        }
    }
}
