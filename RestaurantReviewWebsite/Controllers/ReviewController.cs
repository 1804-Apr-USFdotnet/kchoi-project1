using NLog;
using PagedList;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using LibraryProject;
using RestaurantReviewWebsite.Models;

namespace RestaurantReviewWebsite.Controllers
{
    public class ReviewController : Controller
    {
        Logger logger = LogManager.GetCurrentClassLogger();

        // GET: Review
        public ActionResult Index()
        {
            return RedirectToAction("Search", "Restaurant");
        }

        // GET: Review/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Search", "Restaurant");
            }

            Review rev = Mapper.FindReviewByID((int)id);
            if (rev != null)
            {
                ReviewPageViewModel result = new ReviewPageViewModel
                {
                    Review = rev,
                    RestaurantID = rev.RestaurantID,
                    ReviewerName = ""
                };

                return View(result);
            }
            else
            {
                return RedirectToAction("Search", "Restaurant");
            }
        }

        // GET: Review/Create/3
        public ActionResult Create(int? id)
        {
            if(id == null)
            {
                return RedirectToAction("Search", "Restaurant");
            }

            Restaurant rest = Mapper.FindRestaurantByID((int)id);
            if (rest != null)
            {
                ReviewPageViewModel result = new ReviewPageViewModel
                {
                    RestaurantID = rest.ID,
                    ReviewerName = ""
                };

                return View(result);
            }
            else
            {
                return RedirectToAction("Search", "Restaurant");
            }
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
                    Rating = int.Parse(collection["Review.Rating"]),
                    Description = collection["Review.Description"]
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
            catch (Exception e)
            {
                logger.Error(e.StackTrace);
                return RedirectToAction("Create", new { id = int.Parse(collection["RestaurantID"])});
            }
        }

        // GET: Review/Update/5
        public ActionResult Update(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Search", "Restaurant");
            }
            
            Review rev = Mapper.FindReviewByID((int)id);
            if (rev != null)
            {
                return View(rev);
            }
            else
            {
                return RedirectToAction("Search", "Restaurant");
            }
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
            catch (Exception e)
            {
                logger.Error(e.StackTrace);
                return RedirectToAction("Update", new { id = int.Parse(collection["RestaurantID"]) });
            }
        }

        // GET: Review/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Search", "Restaurant");
            }

            Review rev = Mapper.FindReviewByID((int)id);
            if (rev != null)
            {
                return View(rev);
            }
            else
            {
                return RedirectToAction("Search", "Restaurant");
            }
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
            catch (Exception e)
            {
                logger.Error(e.StackTrace);
                return View();
            }
        }

        // GET: Review/List/5
        public ActionResult List(int? id, int? page)
        {
            if (id == null)
            {
                return RedirectToAction("Search", "Restaurant");
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
