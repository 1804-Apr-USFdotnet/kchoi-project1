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
        public ActionResult Index(string searchString)
        {
            return RedirectToAction("Search", new { id = searchString });
        }

        // GET: Search/Search/burger?orderBy=rating
        public ActionResult Search(string id, string orderBy)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(orderBy) ? "name_desc" : "";
            ViewBag.RatingSortParm = orderBy == "rating" ? "rating_asc" : "rating";

            ICollection<Restaurant> result;
            if (!String.IsNullOrEmpty(id))
            {
                result = Mapper.FindRestaurantsByName(id);
            }
            else {
                result = Mapper.GetRestaurants();
            }

            switch (orderBy)
            {
                case "rating":
                    result = result.OrderByDescending(s => s.AvgRating).ToList();
                    break;
                case "rating_asc":
                    result = result.OrderBy(s => s.AvgRating).ToList();
                    break;
                case "name_desc":
                    result = result.OrderByDescending(s => s.Name).ToList();
                    break;
                default:
                    result = result.OrderBy(s => s.Name).ToList();
                    break;
            }
            return View(result);
        }
    }
}