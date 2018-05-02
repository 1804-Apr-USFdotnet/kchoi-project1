using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.Interfaces
{
    interface IReviewable
    {
        bool AddReview(Review NewReview);
        float AvgRating { get; }
    }
}
