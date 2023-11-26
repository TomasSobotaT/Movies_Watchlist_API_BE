using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movies_Watchlist_DB.Models
{
    public class BaseEntity
    {
     
        [Key]
        public int Id { get; set; }
    }
}
