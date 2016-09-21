using System.Collections.Generic;
using GigHub.Models;

namespace GigHub.Repositories
{
    public interface IGenreRepository
    {
        IEnumerable<Genre> GetGenres();
    }
}