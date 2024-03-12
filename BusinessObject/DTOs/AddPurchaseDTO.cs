using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOs
{
    public class AddPurchaseDTO
    {
        public int ArtworkId { get; set; }
        public int AppUserId { get; set; }
        public decimal Price { get; set; }
    }
}
