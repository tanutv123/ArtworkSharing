using BusinessObject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOs
{
    public class AddTransationDTO
    {
        public decimal Money { get; set; }
        public DateTime CreatedDate { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }

    }
}
