using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeStreet.Domain.Entities.Items
{
    public partial class ProductType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PdtId { get; set; }

        [Required]
        [StringLength(20)]
        public string PdtName { get; set; }
    }
}
