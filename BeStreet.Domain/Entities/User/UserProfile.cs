using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeStreet.Domain.Entities.User
{
    public class UserProfile
    {
        [Required]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Sorry, Name too long.")]
        public string CusName { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Must be between 5 and 30 characters.")]
        public string CusLogin { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Must be between 8 and 50 characters.")]
        public string CusPass { get; set; }
        
        [Required]
        [StringLength(30)]
        public string CusEmail { get; set; }

        public string CusAdd { get; set; }
    }
}
