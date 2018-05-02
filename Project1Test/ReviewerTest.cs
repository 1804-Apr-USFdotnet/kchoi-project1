using Newtonsoft.Json;

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using LibraryProject;

namespace Project0Test
{
    [TestClass]
    public class ReviewerTest
    {
        Reviewer reviewer;
        const string reviewerJSON = "{\"Name\":\"John Doe\",\"ID\":0}";

        [TestMethod]
        public void TestReviewerName()
        {
            reviewer = JsonConvert.DeserializeObject<Reviewer>(reviewerJSON);
            Assert.AreEqual(reviewer.Name, "John Doe");
        }

        [TestMethod]
        public void TestReviewerID()
        {
            reviewer = JsonConvert.DeserializeObject<Reviewer>(reviewerJSON);
            Assert.AreEqual(reviewer.ID, 0);
        }
    }
}
