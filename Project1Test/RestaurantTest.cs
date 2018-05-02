using Newtonsoft.Json;

using System;
using System.Configuration;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using LibraryProject;

namespace Project0Test
{
    [TestClass]
    public class RestaurantTest
    {
        const string testRestaurantJSON = @"{
            'ID':34,
            'Address':'6 Mandrake Hill', 
            'Phone':'952-462-6704', 
            'City':'Young America',
            'State':'MN',
            'ZIP':'55557',
            'Name':'Senger, Rodriguez and Cole'
        }";
        const string testReviewJSON = @"{
            'RestaurantName':'Senger, Rodriguez and Cole',
            'Rating':15,
            'Description':'This is a fake review.'
        }";
        Restaurant testRestaurant;
        Review testReview;

        [TestMethod]
        public void TestRestaurantID()
        {
            testRestaurant = JsonConvert.DeserializeObject<Restaurant>(testRestaurantJSON);
            Assert.AreEqual(testRestaurant.ID, 34);
        }

        [TestMethod]
        public void TestRestaurantAddress()
        {
            testRestaurant = JsonConvert.DeserializeObject<Restaurant>(testRestaurantJSON);
            Assert.AreEqual(testRestaurant.Address, "6 Mandrake Hill");
        }

        [TestMethod]
        public void TestRestaurantCity()
        {
            testRestaurant = JsonConvert.DeserializeObject<Restaurant>(testRestaurantJSON);
            Assert.AreEqual(testRestaurant.City, "Young America");
        }

        [TestMethod]
        public void TestRestaurantState()
        {
            testRestaurant = JsonConvert.DeserializeObject<Restaurant>(testRestaurantJSON);
            Assert.AreEqual(testRestaurant.State, "MN");
        }

        [TestMethod]
        public void TestRestaurantZIP()
        {
            testRestaurant = JsonConvert.DeserializeObject<Restaurant>(testRestaurantJSON);
            Assert.AreEqual(testRestaurant.ZIP, "55557");
        }

        [TestMethod]
        public void TestRestaurantReviews()
        {
            testRestaurant = JsonConvert.DeserializeObject<Restaurant>(testRestaurantJSON);
            testReview = JsonConvert.DeserializeObject<Review>(testReviewJSON);

            testRestaurant.Reviews.Add(testReview);

            Assert.AreEqual(testReview, ((List<Restaurant>)testRestaurant.Reviews)[0]);
        }

        [TestMethod]
        public void TestRestaurantPhoneNum()
        {
            testRestaurant = JsonConvert.DeserializeObject<Restaurant>(testRestaurantJSON);
            Assert.AreEqual(testRestaurant.PhoneNum, "952-462-6704");
        }

        [TestMethod]
        public void TestRestaurantAddReview()
        {
            testRestaurant = JsonConvert.DeserializeObject<Restaurant>(testRestaurantJSON);
            testReview = JsonConvert.DeserializeObject<Review>(testReviewJSON);
            if (!testRestaurant.AddReview(testReview))
            {
                Assert.Fail("failed to add review");
            } else
            {
                Assert.AreEqual(testReview, ((List<Restaurant>)testRestaurant.Reviews)[0]);
            }
        }

        [TestMethod]
        public void TestRestaurantAvgReview()
        {
            using (System.IO.StreamReader reader = new System.IO.StreamReader(ConfigurationManager.AppSettings["DataDirectory"] + ConfigurationManager.AppSettings["RestaurantsFile"]))
            {
                testRestaurant = JsonConvert.DeserializeObject<Restaurant>(reader.ReadLine());
            }
            testRestaurant.RecalculateAvgRating();
            float testAvg = testRestaurant.AvgRating;
            float expectedAvg = 0f;
            foreach(Review i in testRestaurant.Reviews)
            {
                expectedAvg += i.Rating;
            }
            expectedAvg /= testRestaurant.Reviews.Count;
            Assert.AreEqual(expectedAvg, testRestaurant.AvgRating);
        }
    }
}
