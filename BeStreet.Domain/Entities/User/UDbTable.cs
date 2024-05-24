using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeStreet.Domain.Entities.User
{
    public class UDbTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        [StringLength(30, MinimumLength = 5)]
        public string Username { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 8)]
        public string Password { get; set; }
    }
}
