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

        public static void UpdateReview(Review review)
        {
            using(db = new RestaurantReviewsEntities())
            {
                Logger log = LogManager.GetCurrentClassLogger();
                StringBuilder msg = new StringBuilder();

                Review rev = db.Reviews.Find(review.ID);

                rev.Rating = review.Rating;
                rev.RestaurantID = review.RestaurantID;
                rev.ReviewerID = review.ReviewerID;
                rev.Description = review.Description;

                try
                {
                    db.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException e)
                {
                    msg.Append(rev.ID + " " + rev.RestaurantID + " " + rev.Rating)
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
                oldRestaurant.City = newRestaurant.City;
                oldRestaurant.ID = newRestaurant.ID;
                oldRestaurant.Name = newRestaurant.Name;
                oldRestaurant.PhoneNum = newRestaurant.PhoneNum;
                oldRestaurant.State = newRestaurant.State;
                oldRestaurant.ZIP = newRestaurant.ZIP;

                ICollection<Review> reviews = db.Reviews.Where(x => x.RestaurantID == newRestaurant.ID).ToList();
                if (reviews.Count > 0)
                { 
                    oldRestaurant.AvgRating = (float)reviews.Average(x => x.Rating);
                } else
                {
                    oldRestaurant.AvgRating = 0f;
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

        public static void CreateReview(Review rev)
        {
            using (db = new RestaurantReviewsEntities())
            {
                Logger log = LogManager.GetCurrentClassLogger();
                StringBuilder msg = new StringBuilder();

                db.Reviews.Add(rev);

                try
                {
                    db.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException e)
                {
                    msg.Append(rev.ID + " " + rev.RestaurantID + " " + rev.Rating)
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

        public static void DeleteReviewByID(int id)
        {
            using (db = new RestaurantReviewsEntities())
            {
                Logger log = LogManager.GetCurrentClassLogger();
                StringBuilder msg = new StringBuilder();

                Review rev = db.Reviews.Find(id);

                db.Reviews.Remove(rev);
                
                try
                {
                    db.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException e)
                {
                    msg.Append(rev.ID + " " + rev.RestaurantID + " " + rev.Rating)
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

        public static void DeleteRestaurantByID(int id)
        {
            using (db = new RestaurantReviewsEntities())
            {
                Logger log = LogManager.GetCurrentClassLogger();
                StringBuilder msg = new StringBuilder();

                Restaurant rest = db.Restaurants.Find(id);

                db.Restaurants.Remove(rest);

                ICollection<Review> reviews = db.Reviews.Where(x => x.RestaurantID == rest.ID).ToList();
                foreach (Review rev in reviews)
                {
                    db.Reviews.Remove(rev);
                }

                try
                {
                    db.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException e)
                {
                    msg.Append(rest.Name)
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
