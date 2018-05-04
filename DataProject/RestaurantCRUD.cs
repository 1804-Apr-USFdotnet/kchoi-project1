using NLog;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace DataProject
{
    public static class RestaurantCRUD
    {
        private static RestaurantReviewsEntities db;

        public static void CreateRestaurant(Restaurant restaurant)
        {
            using (db = new RestaurantReviewsEntities())
            {
                Logger log = LogManager.GetCurrentClassLogger();
                StringBuilder msg = new StringBuilder();

                db.Restaurants.Add(restaurant);

                try
                {
                    db.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException e)
                {
                    msg.Append(restaurant.Name)
                        .Append("\n--\n");

                    foreach (var eve in e.EntityValidationErrors)
                    {
                        msg.Append("Entity of type \"")
                            .Append(eve.Entry.Entity.GetType().Name)
                            .Append("\" in state \"")
                            .Append(eve.Entry.State)
                            .Append("\" has the following validation errors:\n");

                        foreach (var ve in eve.ValidationErrors)
                        {
                            msg.Append("- Property: \"")
                                .Append(ve.PropertyName)
                                .Append("\", Error: \"")
                                .Append(ve.ErrorMessage)
                                .Append("\"\n");
                        }
                    }

                    FinishExceptionHandling(log, e, msg.ToString());
                }
                catch (Exception ex)
                {
                    FinishExceptionHandling(log, ex, ex.StackTrace);
                }
            }
        }

        public static IEnumerable<Restaurant> ReadRestaurants()
        {
            using (db = new RestaurantReviewsEntities())
            {
                return db.Restaurants.ToList();
            }
        }

        public static void UpdateRestaurant(Restaurant newRestaurant)
        {
            using (db = new RestaurantReviewsEntities())
            {
                Logger log = LogManager.GetCurrentClassLogger();
                StringBuilder msg = new StringBuilder();

                Restaurant oldRestaurant = db.Restaurants.Find(newRestaurant.ID);

                oldRestaurant.Address = newRestaurant.Address;
                oldRestaurant.AvgRating = newRestaurant.AvgRating;
                oldRestaurant.City = newRestaurant.City;
                oldRestaurant.ID = newRestaurant.ID;
                oldRestaurant.Name = newRestaurant.Name;
                oldRestaurant.PhoneNum = newRestaurant.PhoneNum;
                oldRestaurant.State = newRestaurant.State;
                oldRestaurant.ZIP = newRestaurant.ZIP;

                Review tmpReview = null;
                Review oldReview;
                int i = 0;
                while (i <  oldRestaurant.Reviews.Count)
                {
                    oldReview = oldRestaurant.Reviews.ElementAt(i);
                    tmpReview = newRestaurant.Reviews.Where(x => x.ID == oldReview.ID).FirstOrDefault();
                    if (tmpReview != null)
                    {
                        oldReview.Description = tmpReview.Description;
                        oldReview.Rating = tmpReview.Rating;
                        oldReview.ReviewerID = tmpReview.ReviewerID;
                        i++;
                    }
                    else
                    {
                        oldRestaurant.Reviews.Remove(oldReview);
                    }
                }

                foreach (Review newReview in newRestaurant.Reviews)
                {
                    tmpReview = oldRestaurant.Reviews.Where(x => x.ID == newReview.ID).FirstOrDefault();
                    if(tmpReview == null)
                    {
                        oldRestaurant.Reviews.Add(newReview);
                    }
                }

                try
                {
                    db.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException e)
                {
                    msg.Append(newRestaurant.Name)
                        .Append("\n--\n");

                    foreach (var eve in e.EntityValidationErrors)
                    {
                        msg.Append("Entity of type \"")
                            .Append(eve.Entry.Entity.GetType().Name)
                            .Append("\" in state \"")
                            .Append(eve.Entry.State)
                            .Append("\" has the following validation errors:\n");

                        foreach (var ve in eve.ValidationErrors)
                        {
                            msg.Append("- Property: \"")
                                .Append(ve.PropertyName)
                                .Append("\", Error: \"")
                                .Append(ve.ErrorMessage)
                                .Append("\"\n");
                        }
                    }

                    FinishExceptionHandling(log, e, msg.ToString());
                }
                catch (Exception ex)
                {
                    FinishExceptionHandling(log, ex, ex.StackTrace);
                }
            }
        }

        public static IEnumerable<Review> ReadReviews()
        {
            using (db = new RestaurantReviewsEntities())
            {
                return db.Reviews.ToList();
            }
        }

        public static void DeleteRestaurant(Restaurant restaurant)
        {
            using (db = new RestaurantReviewsEntities())
            {
                Logger log = LogManager.GetCurrentClassLogger();
                StringBuilder msg = new StringBuilder();

                db.Restaurants.Remove(restaurant);
                foreach(Review rev in restaurant.Reviews)
                {
                    db.Reviews.Remove(rev);
                }

                try
                {
                    db.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException e)
                {
                    msg.Append(restaurant.Name)
                        .Append("\n--\n");

                    foreach (var eve in e.EntityValidationErrors)
                    {
                        msg.Append("Entity of type \"")
                            .Append(eve.Entry.Entity.GetType().Name)
                            .Append("\" in state \"")
                            .Append(eve.Entry.State)
                            .Append("\" has the following validation errors:\n");

                        foreach (var ve in eve.ValidationErrors)
                        {
                            msg.Append("- Property: \"")
                                .Append(ve.PropertyName)
                                .Append("\", Error: \"")
                                .Append(ve.ErrorMessage)
                                .Append("\"\n");
                        }
                    }

                    FinishExceptionHandling(log, e, msg.ToString());
                }
                catch(Exception ex)
                {
                    FinishExceptionHandling(log, ex, ex.StackTrace);
                }
            }
        }

        private static void FinishExceptionHandling(Logger log, Exception e, string msg)
        {
            log.Error(e, msg.ToString());
        }
    }
}
