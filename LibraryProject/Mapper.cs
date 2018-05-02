using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataProject;

namespace LibraryProject
{
    public static class Mapper
    {
        public static Restaurant ConvertRestaurantFromDB(DataProject.Restaurant dbRestaurant)
        {
            Restaurant result = new Restaurant
            {
                ID = dbRestaurant.ID,
                Address = dbRestaurant.Address,
                PhoneNum = dbRestaurant.PhoneNum,
                City = dbRestaurant.City,
                State = dbRestaurant.State,
                ZIP = dbRestaurant.ZIP,
                Name = dbRestaurant.Name,
                Reviews = new List<Review>()
            };
            ICollection<DataProject.Review> reviews = dbRestaurant.Reviews;
            foreach (DataProject.Review rev in reviews)
            {
                result.AddReview(ConvertReviewFromDB(rev));
            }

            return result;
        }

        public static ICollection<Restaurant> ConvertRestaurantListFromDB(ICollection<DataProject.Restaurant> dbList)
        {
            ICollection<Restaurant> result = new List<Restaurant>();

            foreach(DataProject.Restaurant res in dbList)
            {
                result.Add(ConvertRestaurantFromDB(res));
            }

            return result;
        }

        public static ICollection<DataProject.Restaurant> ConvertRestaurantListToDB(ICollection<Restaurant> list)
        {
            ICollection<DataProject.Restaurant> result = new List<DataProject.Restaurant>();

            foreach (Restaurant res in list)
            {
                result.Add(ConvertRestaurantToDB(res));
            }

            return result;
        }

        public static DataProject.Restaurant ConvertRestaurantToDB(Restaurant restaurant)
        {
            DataProject.Restaurant result = new DataProject.Restaurant
            {
                ID = restaurant.ID,
                Address = restaurant.Address,
                PhoneNum = restaurant.PhoneNum,
                City = restaurant.City,
                State = restaurant.State,
                ZIP = restaurant.ZIP,
                Name = restaurant.Name,
                AvgRating = restaurant.AvgRating,
                Reviews = new List<DataProject.Review>()
            };
            foreach(Review rev in restaurant.Reviews)
            {
                result.Reviews.Add(ConvertReviewToDB(rev));
            }

            return result;
        }

        private static Review ConvertReviewFromDB(DataProject.Review dbReview)
        {
            Review result = new Review
            {
                Description = dbReview.Description,
                Rating = (int)dbReview.Rating,
                ReviewerID = (dbReview.ReviewerID != null ? (int)dbReview.ReviewerID : -1),
                ID = dbReview.ID,
                RestaurantID = dbReview.RestaurantID
            };

            return result;
        }

        private static DataProject.Review ConvertReviewToDB(Review review)
        {
            DataProject.Review result = new DataProject.Review
            {
                Description = review.Description.Substring(0,Math.Min(200,review.Description.Length)),
                Rating = review.Rating,
                ID = review.ID,
                RestaurantID = review.RestaurantID
            };
            if(review.ReviewerID > 0)
            {
                result.ReviewerID = review.ReviewerID;
            }

            return result;
        }

        public static ICollection<Review> ConvertReviewListFromDB(ICollection<DataProject.Review> collection)
        {
            List<Review> result = new List<Review>();
            foreach(DataProject.Review rev in collection)
            {
                result.Add(ConvertReviewFromDB(rev));
            }

            return result;
        }

        public static ICollection<Review> FindReviewsByRestaurantID(int restID)
        {
            return ConvertReviewListFromDB(RestaurantCRUD.ReadReviews().Where(x => x.RestaurantID == restID).ToList());
        }

        public static Restaurant FindRestaurantByID(int id)
        {
            return ConvertRestaurantFromDB(RestaurantCRUD.ReadRestaurants().Find(id));
        }

        public static ICollection<Restaurant> FindRestaurantsByName(string key)
        {
            return ConvertRestaurantListFromDB(RestaurantCRUD.ReadRestaurants().Where(x => x.Name.Contains(key)).Include("Reviews").ToList());
        }

        public static ICollection<Restaurant> GetRestaurantsSortByRating()
        {
            return ConvertRestaurantListFromDB(RestaurantCRUD.ReadRestaurants().OrderByDescending(x => x.AvgRating).Include("Reviews").ToList());
        }

        public static ICollection<Restaurant> GetRestaurantsSortByRating(int count)
        {
            return ConvertRestaurantListFromDB(RestaurantCRUD.ReadRestaurants().OrderByDescending(x => x.AvgRating).Take(count).Include("Reviews").ToList());
        }

        public static ICollection<Restaurant> GetRestaurantsSortByName()
        {
            return ConvertRestaurantListFromDB(RestaurantCRUD.ReadRestaurants().OrderBy(x => x.Name).Include("Reviews").ToList());
        }

        public static ICollection<Restaurant> GetRestaurants()
        {
            return ConvertRestaurantListFromDB(RestaurantCRUD.ReadRestaurants().Include("Reviews").ToList());
        }
    }
}
