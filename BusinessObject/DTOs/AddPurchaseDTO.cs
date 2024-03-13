using BusinessObject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOs
{
    public class AddPurchaseDTO
    {
        public int AppUserId { get; set; }
        public int ArtworkId { get; set; }
        public DateTime BuyDate { get; set; } = DateTime.UtcNow;
        public decimal BuyPrice { get; set; }
    }
}
