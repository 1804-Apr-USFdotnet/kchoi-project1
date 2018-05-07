using PagedList;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using LibraryProject;

namespace RestaurantReviewWebsite.Models
{
    public class RestaurantPageViewModel
    {
        public IPagedList<Restaurant> List { get; set; }
        public string SearchString { get; set; }
        public string CurrentSort { get; set; }
        public string NameSortParm { get; set; }
        public string RatingSortParm { get; set; }
    }
}