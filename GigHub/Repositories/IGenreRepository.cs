using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Repositories
{
    public interface IGenreRepository
    {
        IEnumerable<Genre> GetGenres();
    }
}