using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using LibraryProject;

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

        // GET: Review/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Review/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: test
                Review rev = new Review
                {
                    RestaurantID = int.Parse(collection["RestaurantID"]),
                    Rating = int.Parse(collection["Rating"]),
                    ReviewerID = int.Parse(collection["ReviewerID"]),
                    Description = collection["Description"]
                };

                Mapper.CreateReview(rev);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Review/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Search");
            }

            return View(Mapper.FindReviewByID((int)id));
        }

        // POST: Review/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: test
                Review rev = new Review
                {
                    ID = id,
                    RestaurantID = int.Parse(collection["RestaurantID"]),
                    Rating = int.Parse(collection["Rating"]),
                    ReviewerID = int.Parse(collection["ReviewerID"]),
                    Description = collection["Description"]
                };

                Mapper.UpdateReview(rev);

                return RedirectToAction("Index");
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
                // TODO: test
                Mapper.DeleteReviewByID(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Review/List/5
        public ActionResult List(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Search");
            }

            ViewBag.ReviewerName = null;

            return View(Mapper.FindReviewsByRestaurantID((int)id));
        }
    }
}
