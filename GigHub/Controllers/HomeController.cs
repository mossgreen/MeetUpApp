using GigHub.Models;
using GigHub.ViewModels;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        /*go to the index page, by defalt the query is null,
        find gigs from database using linq as:
        each upcomingGigs with data of artist, genre
        in the condition of that
        the gig's time > now and it's not been canceled.*/
        public ActionResult Index(string query = null)
        {
            var upcomingGigs = _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .Where(g => g.DateTime > DateTime.Now && !g.IsCanceled);

            /*if queryString is not null, that means this page should be 
            rendered as a queried page with a list of result.
            we do it with linq, it will select the Gigs with key words 
            in artist name, genre name and venue.*/
            if (!String.IsNullOrWhiteSpace(query))
            {
                upcomingGigs = upcomingGigs
                    .Where(g =>
                            g.Artist.Name.Contains(query) ||
                            g.Genre.Name.Contains(query) ||
                            g.Venue.Contains(query));
            }

            /*controller will pass data from model to view, however,
            here we should use ViewModel in order to simplify models.
            Hence, in the end, this method will send this viewModel 
            to Gigs View.*/
            var viewModel = new GigsViewModel
            {
                UpcomingGigs = upcomingGigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Upcoming Gigs",
                SearchTerm = query,
            };

            /*thid Gigs view is in the Shared folder,as Gigs.cshtml*/
            return View("Gigs", viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}