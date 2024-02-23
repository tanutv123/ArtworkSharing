using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOs
{
    public class AddFollowDTO
    {
        [Required]
        public int TargetUserId{ get; set; }
        [Required]
        public int AppUserId { get; set; }
    }
}
