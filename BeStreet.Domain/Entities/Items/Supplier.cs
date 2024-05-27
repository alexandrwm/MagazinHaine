using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeStreet.Domain.Entities.Items
{
    public partial class Supplier
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SupId { get; set; }

        [Required]
        [Display(Name = "Suppliers Name")]
        [StringLength(30)]
        public string SupName { get; set; }

        [Required]
        [Display(Name = "Tel")]
        [StringLength(15)]
        public string SupTel { get; set; }

        [Required]
        [Display(Name = "E-mail")]
        [StringLength(30)]
        public string SupEmail { get; set; }

        [Required]
        [Display(Name = "Suppliers Address")]
        public string SupAdd { get; set; }

        [Display(Name = "Remark")]
        public string SupRemark { get; set; }
    }
}
