using LibraryProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestaurantReviewWebsite.Controllers
{
    public class RestaurantController : Controller
    {
        // GET: Edit
        public ActionResult Index()
        {
            return RedirectToAction("Create");
        }
        
        // GET: Edit/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Edit/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
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

                Mapper.CreateRestaurant(rest);
                
                return RedirectToAction("Index", "Search");
            }
            catch
            {
                return View();
            }
        }

        // GET: Edit/Edit/5
        public ActionResult Update(int? id)
        {
            if(id == null)
            {
                return RedirectToAction("Index", "Search");
            }

            return View(Mapper.FindRestaurantByID((int)id));
        }

        // POST: Edit/Edit/5
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

                return RedirectToAction("Index", "Search");
            }
            catch
            {
                return View();
            }
        }

        // GET: Edit/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Search");
            }

            return View(Mapper.FindRestaurantByID((int)id));
        }

        // POST: Edit/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Mapper.DeleteRestaurantByID(id);

                return RedirectToAction("Index", "Search");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Search");
            }

            return View(Mapper.FindRestaurantByID((int)id));
        }
    }
}
