using BusinessObject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOs
{
    public class AddCommentDTO
    {
        public int Artworkid{ get; set; }
        public int AppUserId{ get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
