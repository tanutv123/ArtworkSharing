using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOs
{
	public class CommissionResendDTO
	{
        [Required]
        public int Id { get; set; }
        [Required]
        public string RequestDescription { get; set; }
        [Required]
        public decimal MinPrice { get; set; }
        [Required]
        public decimal MaxPrice { get; set; }
        [Required]
        public DateTime DueDate{ get; set; }
    }
}
