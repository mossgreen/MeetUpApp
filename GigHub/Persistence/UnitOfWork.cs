using GigHub.Models;
using GigHub.Repositories;

namespace GigHub.Persistence
{
    public class UnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IGigRepository Gigs { get; private set; }
        public AttendanceRepository Attendances { get; private set; }
        public FollowingRepository Followings { get; private set; }
        public GenreRepository Genres { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Gigs = new GigRepository(context);
            Attendances = new AttendanceRepository(context);
            Followings = new FollowingRepository(context);
            Genres = new GenreRepository(context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}