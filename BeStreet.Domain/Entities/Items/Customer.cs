using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeStreet.Domain.Entities.Items
{

    public partial class Customer
    {
        public string CusId { get; set; } = null;

        [Display(Name = "Customer Name")]
        public string CusName { get; set; } = null;

        [Display(Name = "Username ID")]
        public string CusLogin { get; set; } = null;

        [Display(Name = "Passwoed")]
        public string CusPass { get; set; } = null;

        [Display(Name = "E-mail")]
        public string CusEmail { get; set; } = null;

        [Display(Name = "Address ")]
        public string CusAdd { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? LastLogin { get; set; }
    }
}