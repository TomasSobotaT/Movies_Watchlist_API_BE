using System.Text.Json.Serialization;

namespace Movies_Watchlist_API.Models
{
    public class UserDto
    {
        public string UserId { get; set; } = "";
        public string Email { get; set; } = "";
    }
}
