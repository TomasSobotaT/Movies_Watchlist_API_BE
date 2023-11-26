namespace Movies_Watchlist_DB.Models
{
    public class Movie : BaseEntity
    {
        public string Name { get; set; } = "";
        public string CsfdUrl { get; set; } = "";
        public string PosterUrl { get; set; } = "";
    }
}
