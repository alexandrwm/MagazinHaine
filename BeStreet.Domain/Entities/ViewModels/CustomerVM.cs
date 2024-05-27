using BeStreet.Domain.Enums;
using System;

namespace BeStreet.Domain.Entities.ViewModels
{
    public class CustomerVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Login { get; set; }

        public string Pass { get; set; }

        public string Email { get; set; }

        public string Add { get; set; }
        public URole Role { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime LastLogin { get; set; }
    }
}
