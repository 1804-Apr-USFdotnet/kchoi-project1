using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using PagedList;

using LibraryProject;
using RestaurantReviewWebsite.Models;

namespace RestaurantReviewWebsite.Controllers
{
    public class ReviewController : Controller
    {
        // GET: Review
        public ActionResult Index()
        {
            return View();
        }

        // GET: Review/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Search");
            }

            return View(Mapper.FindReviewByID((int)id));
        }

        // GET: Review/Create/3
        public ActionResult Create(int? id)
        {
            if(id == null)
            {
                return RedirectToAction("Index", "Search");
            }

            return View(new ReviewPageViewModel
            {
                RestaurantID = (int)id,
                ReviewerName = ""
            });
        }

        // POST: Review/Create/3
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                Review rev = new Review
                {
                    RestaurantID = int.Parse(collection["RestaurantID"]),
                    Rating = int.Parse(collection["Rating"]),
                    Description = collection["Description"]
                };

                if (string.IsNullOrEmpty(collection["ReviewerID"]))
                {
                    rev.ReviewerID = null;
                } else
                {
                    rev.ReviewerID = int.Parse(collection["ReviewerID"]);
                }

                Mapper.CreateReview(rev);

                return RedirectToAction("List", new { id = rev.RestaurantID });
            }
            catch
            {
                return View();
            }
        }

        // GET: Review/Update/5
        public ActionResult Update(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Search");
            }

            return View(Mapper.FindReviewByID((int)id));
        }

        // POST: Review/Update/5
        [HttpPost]
        public ActionResult Update(int id, FormCollection collection)
        {
            try
            {
                Review rev = new Review
                {
                    ID = id,
                    RestaurantID = int.Parse(collection["RestaurantID"]),
                    Rating = int.Parse(collection["Rating"]),
                    ReviewerID = int.Parse(collection["ReviewerID"]),
                    Description = collection["Description"]
                };

                Mapper.UpdateReview(rev);

                return RedirectToAction("List", new { id = rev.RestaurantID });
            }
            catch
            {
                return View();
            }
        }

        // GET: Review/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Search");
            }

            return View(Mapper.FindReviewByID((int)id));
        }

        // POST: Review/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Review rev = Mapper.FindReviewByID(id);
                Mapper.DeleteReviewByID(id);

                return RedirectToAction("List", new { id = rev.RestaurantID });
            }
            catch
            {
                return View();
            }
        }

        // GET: Review/List/5
        public ActionResult List(int? id, int? page)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Search");
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(new ReviewPageViewModel
            {
                RestaurantID = (int)id,
                ReviewerName = "",
                List = Mapper.FindReviewsByRestaurantID((int)id).ToPagedList(pageNumber, pageSize)
            });
        }
    }
}
