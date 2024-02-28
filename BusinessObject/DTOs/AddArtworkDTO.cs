using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOs
{
    public class AddArtworkDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int GenreId { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
        [Required]
        public int AppUserId { get; set; }

    }
}
