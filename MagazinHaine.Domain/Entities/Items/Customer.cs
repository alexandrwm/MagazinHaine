using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeStreet.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CusId { get; set; }

        [Required]
        [Display(Name = "Customer Name")]
        [StringLength(50)]
        public string CusName { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 5)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 8)]
        public string Password { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime LastLogin { get; set; }
    }
}
