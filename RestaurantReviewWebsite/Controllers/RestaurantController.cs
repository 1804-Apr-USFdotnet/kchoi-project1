using NLog;
using PagedList;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using LibraryProject;

namespace RestaurantReviewWebsite.Controllers
{
    public class RestaurantController : Controller
    {
        Logger logger = LogManager.GetCurrentClassLogger();

        // GET: Restaurant
        public ActionResult Index()
        {
            return RedirectToAction("Search");
        }

        // GET: Restaurant/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Restaurant/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            Restaurant rest = new Restaurant
            {
                Name = collection["Name"],
                Address = collection["Address"],
                City = collection["City"],
                State = collection["State"],
                PhoneNum = collection["PhoneNum"],
                ZIP = collection["ZIP"]
            };

            try
            {
                Mapper.CreateRestaurant(rest);

                return RedirectToAction("Search");
            }
            catch (Exception e)
            {
                logger.Error(e.StackTrace);
                return View(rest);
            }
        }

        // GET: Restaurant/Search?searchString=burger
        public ActionResult Search(string orderBy, string searchString, int? page)
        {
            Models.RestaurantPageViewModel result = new Models.RestaurantPageViewModel
            {
                CurrentSort = orderBy,
                NameSortParm = String.IsNullOrEmpty(orderBy) ? "name_desc" : "",
                RatingSortParm = orderBy == "rating" ? "rating_asc" : "rating"
            };

            ICollection<Restaurant> tmp;
            if (!String.IsNullOrEmpty(searchString))
            {
                tmp = Mapper.FindRestaurantsByName(searchString);
                page = 1;
            }
            else
            {
                tmp = Mapper.GetRestaurants();
            }

            result.SearchString = searchString;

            switch (orderBy)
            {
                case "rating":
                    tmp = tmp.OrderByDescending(s => s.AvgRating).ToList();
                    break;
                case "rating_asc":
                    tmp = tmp.OrderBy(s => s.AvgRating).ToList();
                    break;
                case "name_desc":
                    tmp = tmp.OrderByDescending(s => s.Name).ToList();
                    break;
                default:
                    tmp = tmp.OrderBy(s => s.Name).ToList();
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            result.List = tmp.ToPagedList(pageNumber, pageSize);

            return View(result);
        }

        // GET: Restaurant/Update/5
        public ActionResult Update(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Search");
            }

            Restaurant rest = Mapper.FindRestaurantByID((int)id);

            if (rest != null)
            {
                return View(rest);
            }
            else
            {
                return RedirectToAction("Search");
            }
        }

        // POST: Restaurant/Update/5
        [HttpPost]
        public ActionResult Update(int id, FormCollection collection)
        {
            try
            {
                Restaurant rest = new Restaurant
                {   
                    ID = id,
                    Name = collection["Name"],
                    Address = collection["Address"],
                    City = collection["City"],
                    State = collection["State"],
                    PhoneNum = collection["PhoneNum"],
                    ZIP = collection["ZIP"]
                };

                Mapper.UpdateRestaurant(rest);

                return RedirectToAction("Details", new { id });
            }
            catch (Exception e)
            {
                logger.Error(e.StackTrace);
                return RedirectToAction("Update", new { id });
            }
        }

        // GET: Restaurant/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Search");
            }

            Restaurant rest = Mapper.FindRestaurantByID((int)id);

            if (rest != null)
            {
                return View(rest);
            }
            else
            {
                return RedirectToAction("Search");
            }
        }

        // POST: Restaurant/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Mapper.DeleteRestaurantByID(id);

                return RedirectToAction("Search");
            }
            catch (Exception e)
            {
                logger.Error(e.StackTrace);
                return View();
            }
        }

        // GET: Restaurant/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Search");
            }

            Restaurant rest = Mapper.FindRestaurantByID((int)id);

            if(rest != null) {
                return View(rest);
            }
            else
            {
                return RedirectToAction("Search");
            }
        }
    }
}
