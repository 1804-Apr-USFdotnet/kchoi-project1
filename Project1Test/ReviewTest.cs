using Newtonsoft.Json;

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using LibraryProject;

namespace Project0Test
{
    [TestClass]
    public class ReviewTest
    {
        Review testReview;
        const string testReviewJSON = @"{'ReviewerName':'Perry Van Bruggen','Rating':3,'Description':'This is a fake review.'}";

        [TestMethod]
        public void TestReviewRating()
        {
            testReview = JsonConvert.DeserializeObject<Review>(testReviewJSON);
            Assert.AreEqual(testReview.Rating, 3);
        }

        [TestMethod]
        public void TestReviewDescription()
        {
            testReview = JsonConvert.DeserializeObject<Review>(testReviewJSON);
            Assert.AreEqual(testReview.Description, "This is a fake review.");
        }

        [TestMethod]
        public void TestReviewReviewerID()
        {
            testReview = JsonConvert.DeserializeObject<Review>(testReviewJSON);
            Assert.AreEqual(testReview.ReviewerID, "Perry Van Bruggen");
        }
    }
}
