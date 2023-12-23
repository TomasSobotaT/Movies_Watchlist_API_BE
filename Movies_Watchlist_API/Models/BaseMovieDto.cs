using Movies_Watchlist_DB.Models;

namespace Movies_Watchlist_API.Models
{
    public class BaseMovieDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = "";
        public string posterUrl { get; set; } = "";

        public DateTime? DateAdded { get; set; } = DateTime.Now;
        public DateTime? DateWatched { get; set; } = DateTime.Now;


    }
}
