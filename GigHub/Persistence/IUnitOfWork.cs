using GigHub.Repositories;

namespace GigHub.Persistence
{
    public interface IUnitOfWork
    {
        IGigRepository Gigs { get; }
        IAttendanceRepository Attendances { get; }
        IFollowingRepository Followings { get; }
        IGenreRepository Genres { get; }
        void Complete();
    }
}