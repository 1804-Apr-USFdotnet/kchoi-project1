using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

using LibraryProject;
using RestaurantReviewWebsite.Controllers;
using System.Web.UI.WebControls;

namespace RestaurantReviewWebsite.Tests.Controllers
{
    /// <summary>
    /// Summary description for RestaurantControllerTest
    /// </summary>
    [TestClass]
    public class RestaurantControllerTest
    {
        [TestMethod]
        public void RestaurantControllerTestDeleteModelBinding()
        {
            RestaurantController controller = new RestaurantController();
            
            ViewResult view = (ViewResult)controller.Delete(1);
            var model = view.Model;

            Assert.IsNotNull(model);
        }

        [TestMethod]
        public void RestaurantControllerTestDetailsNull()
        {
            RestaurantController controller = new RestaurantController();

            ActionResult result = controller.Details(10);

            Assert.IsTrue(result is RedirectToRouteResult);
        }

        [TestMethod]
        public void RestaurantControllerTestDetailsData()
        {
            RestaurantController controller = new RestaurantController();

            ViewResult result = (ViewResult)controller.Details(1);
            var model = (Restaurant)result.Model;

            Assert.AreEqual(1, model.ID);
        }

        [TestMethod]
        public void RestaurantControllerTestSearch()
        {
            RestaurantController controller = new RestaurantController();

            ViewResult view = controller.Search("burger", null, null) as ViewResult;
            var model = view.Model;

            Assert.IsNotNull(model is Models.RestaurantPageViewModel);
        }

        [TestMethod]
        public void RestaurantControllerTestUpdateModelBinding()
        {
            RestaurantController controller = new RestaurantController();

            ViewResult view = controller.Update(1) as ViewResult;
            var model = view.Model;

            Assert.IsNotNull(model);
        }
    }
}
