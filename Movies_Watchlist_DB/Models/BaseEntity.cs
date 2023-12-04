using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movies_Watchlist_DB.Models
{
    public abstract class BaseEntity
    {
     
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string CsfdUrl { get; set; } = "";
        public string PosterUrl { get; set; } = "";

        public DateTime DateAdded { get; set; }  = DateTime.Now;
    }
}
