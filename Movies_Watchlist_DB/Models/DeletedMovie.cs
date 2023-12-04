namespace Movies_Watchlist_DB.Models
{
    public class DeletedMovie : BaseEntity
    {
        public DateTime DateWatched { get; set; } = DateTime.Now;
    }
}
