using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeStreet.Domain.Entities.Items
{
    public partial class CartDtl
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemId { get; set; }

        [Required]
        public int PdId { get; set; }

        [Required]
        public int CartId { get; set; }

        public int CdtlQty { get; set; }

        public double CdtlPrice { get; set; }

        public double CdtlMoney { get; set; }
    }
}

