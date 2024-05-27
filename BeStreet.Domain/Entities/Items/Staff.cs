using System;
using System.ComponentModel.DataAnnotations;

namespace BeStreet.Domain.Entities.Items
{

    public partial class Staff
    {
        [Display(Name = "Employee ID")]
        public string StfId { get; set; } = null;


        [Display(Name = "Employee Name")]
        public string StfName { get; set; } = null;

        [Display(Name = "Passwoed")]
        public string StfPass { get; set; } = null;

        [Display(Name = "Duty")]
        public string DutyId { get; set; } = null;

        public DateTime? StartDate { get; set; }

        public DateTime? QuitDate { get; set; }
    }
}