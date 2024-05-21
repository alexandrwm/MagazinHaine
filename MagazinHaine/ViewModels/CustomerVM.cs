using System;

namespace BeStreet.ViewModels
{
    public class CustomerVM
    {
        public string CusId { get; set; } = null;

        public string CusName { get; set; } = null;

        public string CusLogin { get; set; } = null;

        public string CusPass { get; set; } = null;

        public string CusEmail { get; set; } = null;

        public string CusAdd { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime LastLogin { get; set; }
    }
}
