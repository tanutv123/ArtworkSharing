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
		[Required(ErrorMessage = "Title is required.")]
		[StringLength(100, ErrorMessage = "Title must be less than 100 characters.")]
		public string Title { get; set; }
		[Required(ErrorMessage = "Description is required.")]
		[StringLength(500, ErrorMessage = "Description must be less than 500 characters.")]
		public string Description { get; set; }
        public int GenreId { get; set; }
		[Required(ErrorMessage = "Price is required.")]
		[Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
		public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
        [Required]
        public int AppUserId { get; set; }

    }
}
