using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOs
{
	public class UpdateArtworkImageDTO
	{
		public int Id {  get; set; }
		public string ArtworkId { get; set; }
		[Required]
		public string Url { get; set; }
		public bool isMain { get; set; }
		[Required]
		public string PublicId { get; set; }
	}
}
