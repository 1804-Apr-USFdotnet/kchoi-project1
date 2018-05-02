using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using DataProject;
using LibraryProject;

namespace Project0Test
{
    [TestClass]
    public class MapperTest
    {
        private static DataProject.Restaurant dbRest;
        private static LibraryProject.Restaurant blRest;
        private const int restId = 1;
        private const string name = "Taco King";
        private const string address = "1 Main St";
        private const string city = "SF";
        private const string state = "CA";
        private const string phoneNum = "555-555-5555";
        private const string zip = "10001";
        private static DataProject.Review dbRev;
        private static LibraryProject.Review blRev;
        private const int revId = 1;
        private const int rating = 5;
        private const string desc = "totally fake review";

        [TestMethod]
        public void TestConvertRestaurantFromDB() {
            dbRest = new DataProject.Restaurant
            {
                Name = name,
                ID = restId,
                Address = address,
                City = city,
                AvgRating = 5f,
                PhoneNum = phoneNum,
                Reviews = new List<DataProject.Review>(),
                State = state,
                ZIP = zip
            };
            dbRev = new DataProject.Review
            {
                ID = revId,
                Rating = rating,
                Description = desc,
                RestaurantID = restId
            };
            dbRest.Reviews.Add(dbRev);

            blRest = Mapper.ConvertRestaurantFromDB(dbRest);
            blRev = ((List<LibraryProject.Review>)blRest.Reviews)[0];

            Assert.AreEqual(dbRest.Address, blRest.Address, "Address: \""+dbRest.Address+"\" \"" + blRest.Address + '"');
            Assert.AreEqual(dbRest.AvgRating, blRest.AvgRating, "AvgRating: \"" + dbRest.AvgRating + "\" \"" + blRest.AvgRating + '"');
            Assert.AreEqual(dbRest.City, blRest.City, "City: \"" + dbRest.City + "\" \"" + blRest.City + '"');
            Assert.AreEqual(dbRest.ID, blRest.ID, "ID: \"" + dbRest.ID + "\" \"" + blRest.ID + '"');
            Assert.AreEqual(dbRest.Name, blRest.Name, "Name: \"" + dbRest.Name + "\" \"" + blRest.Name + '"');
            Assert.AreEqual(dbRest.PhoneNum, blRest.PhoneNum, "PhoneNum: \"" + dbRest.PhoneNum + "\" \"" + blRest.PhoneNum + '"');
            Assert.AreEqual(dbRest.State, blRest.State, "State: \"" + dbRest.State + "\" \"" + blRest.Address + '"');
            Assert.AreEqual(dbRest.ZIP, blRest.ZIP, "ZIP: \"" + dbRest.ZIP + "\" \"" + blRest.ZIP + '"');
            Assert.AreEqual(dbRev.Description, blRev.Description, "Description: \"" + dbRev.Description + "\" \"" + blRev.Description + '"');
            Assert.AreEqual(dbRev.Rating, blRev.Rating, "Rating: \"" + dbRev.Rating + "\" \"" + blRev.Rating + '"');
            Assert.AreEqual(dbRev.RestaurantID, blRev.RestaurantID, "RestaurantID: \"" + dbRev.RestaurantID + "\" \"" + blRev.RestaurantID + '"');
            Assert.AreEqual(dbRev.ID, blRev.ID, "ID: \"" + dbRev.ID + "\" \"" + blRev.ID + '"');
        }

        [TestMethod]
        public void TestConvertRestaurantToDB()
        {
            blRest = new LibraryProject.Restaurant
            {
                Name = name,
                ID = restId,
                Address = address,
                City = city,
                PhoneNum = phoneNum,
                Reviews = new List<LibraryProject.Review>(),
                State = state,
                ZIP = zip
            };
            blRev = new LibraryProject.Review
            {
                ID = revId,
                Rating = rating,
                Description = desc,
                RestaurantID = restId
            };
            blRest.Reviews.Add(blRev);

            dbRest = Mapper.ConvertRestaurantToDB(blRest);
            dbRev = ((List<DataProject.Review>)dbRest.Reviews)[0];

            Assert.AreEqual(dbRest.Address, blRest.Address, "Address: \"" + dbRest.Address + "\" \"" + blRest.Address + '"');
            Assert.AreEqual(dbRest.AvgRating, blRest.AvgRating, "AvgRating: \"" + dbRest.AvgRating + "\" \"" + blRest.AvgRating + '"');
            Assert.AreEqual(dbRest.City, blRest.City, "City: \"" + dbRest.City + "\" \"" + blRest.City + '"');
            Assert.AreEqual(dbRest.ID, blRest.ID, "ID: \"" + dbRest.ID + "\" \"" + blRest.ID + '"');
            Assert.AreEqual(dbRest.Name, blRest.Name, "Name: \"" + dbRest.Name + "\" \"" + blRest.Name + '"');
            Assert.AreEqual(dbRest.PhoneNum, blRest.PhoneNum, "PhoneNum: \"" + dbRest.PhoneNum + "\" \"" + blRest.PhoneNum + '"');
            Assert.AreEqual(dbRest.State, blRest.State, "State: \"" + dbRest.State + "\" \"" + blRest.Address + '"');
            Assert.AreEqual(dbRest.ZIP, blRest.ZIP, "ZIP: \"" + dbRest.ZIP + "\" \"" + blRest.ZIP + '"');
            Assert.AreEqual(dbRev.Description, blRev.Description, "Description: \"" + dbRev.Description + "\" \"" + blRev.Description + '"');
            Assert.AreEqual(dbRev.Rating, blRev.Rating, "Rating: \"" + dbRev.Rating + "\" \"" + blRev.Rating + '"');
            Assert.AreEqual(dbRev.RestaurantID, blRev.RestaurantID, "RestaurantID: \"" + dbRev.RestaurantID + "\" \"" + blRev.RestaurantID + '"');
            Assert.AreEqual(dbRev.ID, blRev.ID, "ID: \"" + dbRev.ID + "\" \"" + blRev.ID + '"');
        }

        [TestMethod]
        public void TestConvertRestaurantListToDB()
        {
            ICollection<LibraryProject.Restaurant> blList = new List<LibraryProject.Restaurant>();
            blRest = new LibraryProject.Restaurant
            {
                Name = name,
                ID = restId,
                Address = address,
                City = city,
                PhoneNum = phoneNum,
                Reviews = new List<LibraryProject.Review>(),
                State = state,
                ZIP = zip
            };
            blRev = new LibraryProject.Review
            {
                ID = revId,
                Rating = rating,
                Description = desc,
                RestaurantID = restId
            };
            blRest.Reviews.Add(blRev);
            blList.Add(blRest);

            ICollection<DataProject.Restaurant> dbList = Mapper.ConvertRestaurantListToDB(blList);

            if(dbList.Count != blList.Count)
            {
                Assert.Fail("lists not equal");
            } else
            {
                for(int i = 0; i < dbList.Count; i++)
                {
                    dbRest = ((List<DataProject.Restaurant>)dbList)[i];
                    blRest = ((List<LibraryProject.Restaurant>)blList)[i];

                    for (int y = 0; y < dbRest.Reviews.Count; y++)
                    {
                        dbRev = ((List<DataProject.Review>)dbRest.Reviews)[0];
                        blRev = ((List<LibraryProject.Review>)blRest.Reviews)[0];
                        Assert.AreEqual(dbRest.Address, blRest.Address, "Address: \"" + dbRest.Address + "\" \"" + blRest.Address + '"');
                        Assert.AreEqual(dbRest.AvgRating, blRest.AvgRating, "AvgRating: \"" + dbRest.AvgRating + "\" \"" + blRest.AvgRating + '"');
                        Assert.AreEqual(dbRest.City, blRest.City, "City: \"" + dbRest.City + "\" \"" + blRest.City + '"');
                        Assert.AreEqual(dbRest.ID, blRest.ID, "ID: \"" + dbRest.ID + "\" \"" + blRest.ID + '"');
                        Assert.AreEqual(dbRest.Name, blRest.Name, "Name: \"" + dbRest.Name + "\" \"" + blRest.Name + '"');
                        Assert.AreEqual(dbRest.PhoneNum, blRest.PhoneNum, "PhoneNum: \"" + dbRest.PhoneNum + "\" \"" + blRest.PhoneNum + '"');
                        Assert.AreEqual(dbRest.State, blRest.State, "State: \"" + dbRest.State + "\" \"" + blRest.Address + '"');
                        Assert.AreEqual(dbRest.ZIP, blRest.ZIP, "ZIP: \"" + dbRest.ZIP + "\" \"" + blRest.ZIP + '"');
                        Assert.AreEqual(dbRev.Description, blRev.Description, "Description: \"" + dbRev.Description + "\" \"" + blRev.Description + '"');
                        Assert.AreEqual(dbRev.Rating, blRev.Rating, "Rating: \"" + dbRev.Rating + "\" \"" + blRev.Rating + '"');
                        Assert.AreEqual(dbRev.RestaurantID, blRev.RestaurantID, "RestaurantID: \"" + dbRev.RestaurantID + "\" \"" + blRev.RestaurantID + '"');
                        Assert.AreEqual(dbRev.ID, blRev.ID, "ID: \"" + dbRev.ID + "\" \"" + blRev.ID + '"');
                    }
                }
            }
        }

        [TestMethod]
        public void TestConvertRestaurantListFromDB()
        {
            ICollection<DataProject.Restaurant> dbList = new List<DataProject.Restaurant>();
            dbRest = new DataProject.Restaurant
            {
                Name = name,
                ID = restId,
                Address = address,
                City = city,
                AvgRating = 5f,
                PhoneNum = phoneNum,
                Reviews = new List<DataProject.Review>(),
                State = state,
                ZIP = zip
            };
            dbRev = new DataProject.Review
            {
                ID = revId,
                Rating = rating,
                Description = desc,
                RestaurantID = restId
            };
            dbRest.Reviews.Add(dbRev);
            dbList.Add(dbRest);

            ICollection<LibraryProject.Restaurant> blList = Mapper.ConvertRestaurantListFromDB(dbList);

            if (dbList.Count != blList.Count)
            {
                Assert.Fail("lists not equal");
            }
            else
            {
                for (int i = 0; i < dbList.Count; i++)
                {
                    dbRest = ((List<DataProject.Restaurant>)dbList)[i];
                    blRest = ((List<LibraryProject.Restaurant>)blList)[i];

                    for (int y = 0; y < dbRest.Reviews.Count; y++)
                    {
                        dbRev = ((List<DataProject.Review>)dbRest.Reviews)[0];
                        blRev = ((List<LibraryProject.Review>)blRest.Reviews)[0];
                        Assert.AreEqual(dbRest.Address, blRest.Address, "Address: \"" + dbRest.Address + "\" \"" + blRest.Address + '"');
                        Assert.AreEqual(dbRest.AvgRating, blRest.AvgRating, "AvgRating: \"" + dbRest.AvgRating + "\" \"" + blRest.AvgRating + '"');
                        Assert.AreEqual(dbRest.City, blRest.City, "City: \"" + dbRest.City + "\" \"" + blRest.City + '"');
                        Assert.AreEqual(dbRest.ID, blRest.ID, "ID: \"" + dbRest.ID + "\" \"" + blRest.ID + '"');
                        Assert.AreEqual(dbRest.Name, blRest.Name, "Name: \"" + dbRest.Name + "\" \"" + blRest.Name + '"');
                        Assert.AreEqual(dbRest.PhoneNum, blRest.PhoneNum, "PhoneNum: \"" + dbRest.PhoneNum + "\" \"" + blRest.PhoneNum + '"');
                        Assert.AreEqual(dbRest.State, blRest.State, "State: \"" + dbRest.State + "\" \"" + blRest.Address + '"');
                        Assert.AreEqual(dbRest.ZIP, blRest.ZIP, "ZIP: \"" + dbRest.ZIP + "\" \"" + blRest.ZIP + '"');
                        Assert.AreEqual(dbRev.Description, blRev.Description, "Description: \"" + dbRev.Description + "\" \"" + blRev.Description + '"');
                        Assert.AreEqual(dbRev.Rating, blRev.Rating, "Rating: \"" + dbRev.Rating + "\" \"" + blRev.Rating + '"');
                        Assert.AreEqual(dbRev.RestaurantID, blRev.RestaurantID, "RestaurantID: \"" + dbRev.RestaurantID + "\" \"" + blRev.RestaurantID + '"');
                        Assert.AreEqual(dbRev.ID, blRev.ID, "ID: \"" + dbRev.ID + "\" \"" + blRev.ID + '"');
                    }
                }
            }
        }
    }
}
