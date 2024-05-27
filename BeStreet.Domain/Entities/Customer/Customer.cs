using BeStreet.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeStreet.Domain.Entities.User
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Sorry, First name too long.")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Username")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Must be between 5 and 30 characters.")]
        public string Login { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Must be between 8 and 50 characters.")]
        public string Pass { get; set; }

        [Required]
        [Display(Name = "E-mail")]
        [StringLength(30)]
        public string Email { get; set; }

        [Display(Name = "Address ")]
        public string Add { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime LastLogin { get; set; }

        [Required]
        public URole Role { get; set; }
    }
}
