namespace Movies_Watchlist_API.Models
{
    public class MovieDto
    {
        public int Id { get; set; } 
        public string Name { get; set; } = "";
        public string csfdUrl { get; set; } = "";
        public string posterUrl { get; set; } = "";
    }
}
