using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeStreet.Domain.Entities.Items
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CusId { get; set; }

        [Required]
        [Display(Name = "Customer Name")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Sorry, First name too long.")]
        public string CusName { get; set; }

        [Required]
        [Display(Name = "Username ID")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Must be between 5 and 30 characters.")]
        public string CusLogin { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Must be between 8 and 50 characters.")]
        public string CusPass { get; set; }

        [Required]
        [Display(Name = "E-mail")]
        [StringLength(30)]
        public string CusEmail { get; set; }

        [Display(Name = "Address ")]
        public string CusAdd { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime LastLogin { get; set; }
    }
}
