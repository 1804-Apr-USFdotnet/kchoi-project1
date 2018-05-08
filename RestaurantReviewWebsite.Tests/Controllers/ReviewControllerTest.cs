using System;
using System.Web.Mvc;
using LibraryProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantReviewWebsite.Controllers;

namespace ReviewReviewWebsite.Tests.Controllers
{
    [TestClass]
    public class ReviewControllerTest
    {
        [TestMethod]
        public void ReviewControllerTestDeleteModelBinding()
        {
            ReviewController controller = new ReviewController();

            ViewResult view = (ViewResult)controller.Delete(1);
            var model = view.Model;

            Assert.IsNotNull(model);
        }

        [TestMethod]
        public void ReviewControllerTestDetailsNull()
        {
            ReviewController controller = new ReviewController();

            ActionResult result = controller.Details(100);

            Assert.IsTrue(result is RedirectToRouteResult);
        }

        [TestMethod]
        public void ReviewControllerTestDetailsData()
        {
            ReviewController controller = new ReviewController();

            ViewResult result = (ViewResult)controller.Details(1);
            var model = (RestaurantReviewWebsite.Models.ReviewPageViewModel)result.Model;

            Assert.AreEqual(1, model.Review.ID);
        }

        [TestMethod]
        public void ReviewControllerTestSearch()
        {
            ReviewController controller = new ReviewController();

            ViewResult view = controller.List(1, null) as ViewResult;
            var model = view.Model;

            Assert.IsNotNull(model is RestaurantReviewWebsite.Models.ReviewPageViewModel);
        }

        [TestMethod]
        public void ReviewControllerTestUpdateModelBinding()
        {
            ReviewController controller = new ReviewController();

            ViewResult view = controller.Update(1) as ViewResult;
            var model = view.Model;

            Assert.IsNotNull(model);
        }
    }
}
