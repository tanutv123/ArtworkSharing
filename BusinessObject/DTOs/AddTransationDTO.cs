using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOs
{
    public class AddTransationDTO
    {
        public int AppUserId{ get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime Created { get; set; }

    }
}
