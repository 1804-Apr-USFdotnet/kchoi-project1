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
                AvgRating = (float)dbRestaurant.AvgRating
            };

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

        public static void CreateReview(Review rev)
        {
            RestaurantCRUD.CreateReview(ConvertReviewToDB(rev));
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
                AvgRating = restaurant.AvgRating
            };
            return result;
        }

        public static Review FindReviewByID(int id)
        {
            return ConvertReviewFromDB(RestaurantCRUD.ReadReviews().Where(x => x.ID == id).FirstOrDefault());
        }

        public static void UpdateReview(Review rev)
        {
            RestaurantCRUD.UpdateReview(ConvertReviewToDB(rev));
        }

        public static void DeleteReviewByID(int id)
        {
            RestaurantCRUD.DeleteReviewByID(id);
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
            return ConvertRestaurantFromDB(RestaurantCRUD.ReadRestaurants().Where(x => x.ID == id).FirstOrDefault());
        }

        public static ICollection<Restaurant> FindRestaurantsByName(string key)
        {
            return ConvertRestaurantListFromDB(RestaurantCRUD.ReadRestaurants().Where(x => x.Name.ToLower().Contains(key.ToLower())).ToList());
        }

        public static ICollection<Restaurant> GetRestaurantsSortByRating()
        {
            return ConvertRestaurantListFromDB(RestaurantCRUD.ReadRestaurants().OrderByDescending(x => x.AvgRating).ToList());
        }

        public static ICollection<Restaurant> GetRestaurantsSortByRating(int count)
        {
            return ConvertRestaurantListFromDB(RestaurantCRUD.ReadRestaurants().OrderByDescending(x => x.AvgRating).Take(count).ToList());
        }

        public static ICollection<Restaurant> GetRestaurantsSortByName()
        {
            return ConvertRestaurantListFromDB(RestaurantCRUD.ReadRestaurants().OrderBy(x => x.Name).ToList());
        }

        public static ICollection<Restaurant> GetRestaurants()
        {
            return ConvertRestaurantListFromDB(RestaurantCRUD.ReadRestaurants().ToList());
        }

        public static void CreateRestaurant(Restaurant rest)
        {
            RestaurantCRUD.CreateRestaurant(ConvertRestaurantToDB(rest));
        }

        public static void UpdateRestaurant(Restaurant rest)
        {
            RestaurantCRUD.UpdateRestaurant(ConvertRestaurantToDB(rest));
        }

        public static void DeleteRestaurantByID(int id)
        {
            RestaurantCRUD.DeleteRestaurantByID(id);
        }
    }
}
