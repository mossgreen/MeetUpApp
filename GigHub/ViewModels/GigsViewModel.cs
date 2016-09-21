using System.Collections.Generic;
using System.Linq;
using GigHub.Core.Models;

namespace GigHub.ViewModels
{
    public class GigsViewModel
    {
        public IEnumerable<Gig> UpcomingGigs { get; set; }
        public bool ShowActions { get; set; }
        public string Heading { get; set; }
        public string SearchTerm { get; set; }

        /*data type is like dictionary, int is the key, attendance is value*/
        public ILookup<int, Attendance> Attendances { get; set; }
    }
}