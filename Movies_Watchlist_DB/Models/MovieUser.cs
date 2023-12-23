using Microsoft.AspNetCore.Identity;

namespace Movies_Watchlist_DB.Models
{
    public class MovieUser : IdentityUser
    {
        public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
    }
}
