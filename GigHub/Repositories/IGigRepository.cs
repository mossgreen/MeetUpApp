using GigHub.Models;
using System.Collections.Generic;

namespace GigHub.Repositories
{
    public interface IGigRepository
    {
        Gig GetGig(int gigId);
        IEnumerable<Gig> GetUpcomingGigsByArtist(string artistid);
        Gig GetGigWithAttendees(int gigId);
        IEnumerable<Gig> GetGigsUserAttending(string userid);
        void Add(Gig gig);
    }
}
