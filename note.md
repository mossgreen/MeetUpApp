###Create a detail page

1. Create a link at the gig page

  ```
              <div class="details">
                  <span class="artist">
                      <a href="@Url.Action("Details", "Gigs", new {id = gig.Id})">
                          @gig.Artist.Name
                      </a>
  ```
  >in the code, we can know: to redirect to another page, we need to use `@Url.Action()`, in this case, we call the `Details` method in the `Gigs` controller, give it a parameter as id.

2. Add Details method in GigsController

  ```
    public ActionResult Details(int id)
          {
              var gig = _context.Gigs
                  .Include(g => g.Artist)
                  .Include(g => g.Genre)
                  .SingleOrDefault(g => g.Id == id);

              if (gig == null)
                  return HttpNotFound();

              var viewModel = new GigDetailsViewModel {Gig = gig};

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
  ```
 
3. Add ViewModel, since data will show on the page come from multipul sources
 
   ```
   using GigHub.Models;

  namespace GigHub.Controllers
  {
      public class GigDetailsViewModel
      {
          public Gig Gig { get; set; }
          public bool IsAttending { get; set; }
          public bool IsFollowing { get; set; }

      }
  }
  ```
4. Create Details page in views/gigs/details.cshtml
 
   ```
   @using System.Web.UI.WebControls
  @model GigHub.Controllers.GigDetailsViewModel

  @{
      ViewBag.Title = "title";
      Layout = "~/Views/Shared/_Layout.cshtml";
  }

  <h2>
      @Model.Gig.Artist.Name
      @if (User.Identity.IsAuthenticated)
      {
          if (Model.IsFollowing)
          {
              <button class="btn btn-info">Following</button>
          }
          else
          {
              <button class="btn btn-default">Follow</button>
          }
      }
  </h2>
  <p>
      Performing at @Model.Gig.Venue on @Model.Gig.DateTime.ToString("d MMM") at @Model.Gig.DateTime.ToString("HH:mm")
  </p>

  @if (User.Identity.IsAuthenticated && Model.IsAttending)
  {
       <p>You are going to this event.</p>
  }
  ```
  >in default, the layout should be `_layout.cshtml`, here we modify it as `Layout = "~/Views/Shared/_Layout.cshtml";`
  
