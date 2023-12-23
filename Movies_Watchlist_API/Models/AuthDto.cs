using System.ComponentModel.DataAnnotations;

namespace Movies_Watchlist_API.Models
{
    public class AuthDto
    {

        [EmailAddress]
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";

    }
}
