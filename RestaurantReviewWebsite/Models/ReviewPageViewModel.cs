using PagedList;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using LibraryProject;

namespace RestaurantReviewWebsite.Models
{
    public class ReviewPageViewModel
    {
        public Review Review { get; set; }
        public IPagedList<Review> List { get; set; }
        public int RestaurantID { get; set; }
        public string ReviewerName { get; set; }
    }
}