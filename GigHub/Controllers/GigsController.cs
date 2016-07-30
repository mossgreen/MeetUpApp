using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GigsController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _context.Gigs
                .Where(g =>
                    g.ArtistId == userId &&
                    g.DateTime > DateTime.Now &&
                    !g.IsCanceled)
                .Include(g => g.Genre)
                .ToList();

            return View(gigs);
        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();

            var viewModel = new GigsViewModel()
            {
                UpcomingGigs = GetGigsUserAttending(userId),
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gigs I'm Attending",
                Attendances = GetFutureAttendances(userId).ToLookup(a => a.GigId),
            };

            return View("Gigs", viewModel);
        }

        private List<Attendance> GetFutureAttendances(string userId)
        {
            return _context.Attendances
               .Where(a => a.AttendeeId == userId && a.Gig.DateTime > DateTime.Now)
               .ToList();
        }
        private List<Gig> GetGigsUserAttending(String userId)
        {
            return _context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();
        }

        [HttpPost]
        public ActionResult Search(GigsViewModel viewModel)
        {
            return RedirectToAction("Index", "Home", new { query = viewModel.SearchTerm });
        }

        /*this is the httpGet for Add a Gig page,
        it will get genres list as data provided for dropdown list
        and heading for this page: Add a gig. reason for heading:
        this page can also be used as Updating a gig page.*/
        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel
            {
                Genres = _context.Genres.ToList(),
                Heading = "Add a Gig"
            };

            return View("GigForm", viewModel);
        }

        /*this method retrieved data from database
        firstly, it gets userId from current user,
        and then, it gets the gig from gigs table from database,
        at last, build a viewModel and send it to GigForm,
        hence GigForm becomes an Editing form.*/
        [Authorize]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _context.Gigs.Single(g => g.Id == id && g.ArtistId == userId);

            var viewModel = new GigFormViewModel
            {
                Heading = "Edit a Gig",
                Id = gig.Id,
                Genres = _context.Genres.ToList(),
                Date = gig.DateTime.ToString("d MMM yyyy"),
                Time = gig.DateTime.ToString("HH:mm"),
                Genre = gig.GenreId,
                Venue = gig.Venue
            };

            return View("GigForm", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            /*if the form didn't pass the validation
            the the dropdown list still can get genres data
            but won't post data*/
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("GigForm", viewModel);
            }
            /*if the form passed the validation
            construct a new gig using the filed data
            add this gig to gigs table and save the database
            redirect this page to Views/Gigs/Mine page*/
            var gig = new Gig
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue
            };

            _context.Gigs.Add(gig);
            _context.SaveChanges();

            return RedirectToAction("Mine", "Gigs");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("GigForm", viewModel);
            }

            /*if this post method passed validation,
            it firstly will get the userId, which is the id of current artist's id
            and then it will get this old gig from Gigs table from database, 
            with Attendances infomation with it.
            At last, making modifications to this gig with data from viewModel.
            this Modify method is from Models/Gig, and 
            will iterate each attendee to notify them the modification of this gig.*/
            var userId = User.Identity.GetUserId();
            var gig = _context.Gigs
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .Single(g => g.Id == viewModel.Id && g.ArtistId == userId);

            gig.Modify(viewModel.GetDateTime(), viewModel.Venue, viewModel.Genre);

            _context.SaveChanges();

            return RedirectToAction("Mine", "Gigs");
        }

        public ActionResult Details(int id)
        {
            var gig = _context.Gigs
                    .Include(g => g.Artist)
                    .Include(g => g.Genre)
                    .SingleOrDefault(g => g.Id == id);

            if (gig == null)
                return HttpNotFound();

            var viewModel = new GigDetailsViewModel { Gig = gig };

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();

                viewModel.IsAttending = _context.Attendances
                    .Any(a => a.GigId == gig.Id && a.AttendeeId == userId);

                viewModel.IsFollowing = _context.Followings
                    .Any(f => f.FolloweeId == gig.ArtistId && f.FollowerId == userId);
            }

            return View("Details", viewModel);
        }
    }
}