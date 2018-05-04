using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using LibraryProject;

namespace RestaurantReviewWebsite.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
  /*      public ActionResult Index()
        {
            return View();
        }
        */
        public ActionResult Index(string searchKey)
        {
            return View(Mapper.FindRestaurantsByName(searchKey));
        }

        public ActionResult Details(int id)
        {
            return View(Mapper.FindRestaurantByID(id));
        }
    }
}