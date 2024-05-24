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
        public string Name { get; set; }
        
        [Required]
        [Display(Name = "Username")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Must be between 5 and 30 characters.")]
        public string Login { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Must be between 8 and 50 characters.")]
        public string Pass { get; set; }

        [Required]
        [Display(Name = "E-mail")]
        [StringLength(30)]
        public string Email { get; set; }

        [Display(Name = "Address ")]
        public string Add { get; set; }
    }
}
