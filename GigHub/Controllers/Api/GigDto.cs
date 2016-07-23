using System;

namespace GigHub.Controllers.Api
{
    public class GigDto
    {
        public int Id { get; set; }

        public bool IsCanceled { get; private set; }

        public UserDto Artist { get; set; }

        public DateTime DateTime { get; set; }

        public string Venue { get; set; }

        public GenreDto Genre { get; set; }
    }
}